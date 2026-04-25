using System;
using DotNetAppBase.Std.Library;
using DotNetAppBase.Std.Worker.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetAppBase.Std.Worker.Factory
{
    public static class HostedServiceFactory
    {
        public static T Create<T>(IServiceProvider provider, string name = null, string settingsSectionName = null) where T : HostedServiceBase, new()
        {
            var service = new T();

            IConfigurationSection settingsSection = null;
            if (XHelper.Strings.IsNotEmptyOrWhiteSpace(settingsSectionName))
            {
                settingsSection = provider.GetService<IConfiguration>().GetSection(settingsSectionName);
            }

            service.Initialize(name ?? service.GetType().FullName, provider.GetService<ILoggerFactory>(), settingsSection);

            return service;
        }

        public static T Initialize<T>(IServiceProvider provider, T service, string name = null, string settingsSectionName = null) where T : HostedServiceBase
        {
            IConfigurationSection settingsSection = null;
            if (XHelper.Strings.IsNotEmptyOrWhiteSpace(settingsSectionName))
            {
                settingsSection = provider.GetService<IConfiguration>().GetSection(settingsSectionName);
            }

            service.Initialize(name ?? service.GetType().FullName, provider.GetService<ILoggerFactory>(), settingsSection);

            return service;
        }
    }
}