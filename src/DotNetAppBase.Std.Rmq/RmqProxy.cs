using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using DotNetAppBase.Std.Rmq.Abstraction;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DotNetAppBase.Std.Rmq
{
    public class RmqProxy : IRmqProxy
    {
        private readonly RmqQueueEndpoint _endpoint;
        private readonly Lazy<ConnectionFactory> _lazyConnFactory;

        private readonly object _syncSubscribers = new object();
        private readonly List<RmqSubscriber> _subscribers;

        private readonly object _syncConnection = new object();
        private IModel _channel;
        private IConnection _connection;
        private IBasicProperties _defProperties;

        public RmqProxy(RmqQueueEndpoint endpoint, bool autoConnect=true)
        {
            _endpoint = endpoint;
            _subscribers = new List<RmqSubscriber>();

            _lazyConnFactory = new Lazy<ConnectionFactory>(
                () => new ConnectionFactory
                          {
                              HostName = _endpoint.HostName,
                              Port = _endpoint.Port,
                              UserName = _endpoint.UserName,
                              Password = _endpoint.UserPwd
                          });

            if (autoConnect)
            {
                Connect();
            }
        }

        private ConnectionFactory ConnFactory => _lazyConnFactory.Value;

        public bool IsConnected
        {
            get
            {
                lock (_syncConnection)
                {
                    return _channel != null;
                }
            }
        }

        public bool Add(object model, byte maxAttempts = 3)
        {
            var json = JsonSerializer.Serialize(model);
            var data = Encoding.UTF8.GetBytes(json);

            return Add(data, maxAttempts);
        }

        public bool Add(byte[] data, byte maxAttempts = 3)
        {
            var i = 0;
            while (true)
            {
                try
                {
                    if (i == maxAttempts)
                    {
                        return false;
                    }

                    _channel.BasicPublish(
                        _endpoint.Exchange ?? string.Empty,
                        _endpoint.QueueName,
                        _defProperties,
                        data);

                    return true;
                }
                catch (Exception )
                {
                    i++;

                    Disconnect();
                    Connect();
                }
            }
        }

        public bool Connect()
        {
            lock (_syncConnection)
            {
                if (IsConnected)
                {
                    return true;
                }

                try
                {
                    _connection = ConnFactory.CreateConnection();
                    _channel = _connection.CreateModel();

                    _defProperties = _channel.CreateBasicProperties();
                    _defProperties.Persistent = true;

                    _subscribers.ForEach(sbr => sbr.Subscribe());

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public void Disconnect()
        {
            lock (_syncConnection)
            {
                if (!IsConnected)
                {
                    return;
                }

                _subscribers.ForEach(sbr => sbr.Unsubscribe());

                _defProperties = null;

                _channel?.Close();
                _channel = null;

                _connection?.Close();
                _connection = null;
            }
        }

        internal EventingBasicConsumer CreateConsumer()
        {
            lock (_syncConnection)
            {
                if (!IsConnected)
                {
                    throw new ApplicationException("There isn't connection to create a consumer.");
                }

                var consumer = new EventingBasicConsumer(_channel);

                _channel.BasicConsume(queue: _endpoint.QueueName, false, consumer: consumer);

                return consumer;
            }
        }

        public void AddSubscriber(RmqSubscriber subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            lock (_syncSubscribers)
            {
                if (_subscribers.Contains(subscriber))
                {
                    return;
                }

                _subscribers.Add(subscriber);
                subscriber.Initialize(this);
            }
        }
    }
}