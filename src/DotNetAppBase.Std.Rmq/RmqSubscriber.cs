using System;
using DotNetAppBase.Std.Rmq.Abstraction;
using DotNetAppBase.Std.Rmq.Events;
using RabbitMQ.Client.Events;

namespace DotNetAppBase.Std.Rmq
{
    public class RmqSubscriber : IRmqSubscriber
    {
        private EventingBasicConsumer _consumer;
        private RmqProxy _proxy;

        public event EventHandler<RmqReceivedEventArgs> Received;

        public void Initialize(RmqProxy proxy)
        {
            _proxy = proxy;

            Subscribe();
        }

        internal void Subscribe()
        {
            Unsubscribe();

            _consumer = _proxy.CreateConsumer();
            _consumer.Received += OnConsumerOnReceived;
        }

        internal void Unsubscribe()
        {
            if (_consumer != null)
            {
                _consumer.Received -= OnConsumerOnReceived;
            }
        }

        private void OnConsumerOnReceived(object sender, BasicDeliverEventArgs ea)
        {
            try
            {
                Received?.Invoke(this, new RmqReceivedEventArgs(ea.DeliveryTag, ea.Body));

                ((EventingBasicConsumer) sender).Model.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception)
            {
                //
            }
        }
    }
}