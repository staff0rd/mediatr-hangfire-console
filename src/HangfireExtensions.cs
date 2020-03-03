using Hangfire;
using Hangfire.Common;
using MediatR;
using Newtonsoft.Json;

namespace mediatr_hangfire_console
{
    public static class HangfireExtensions
    {
        public static IGlobalConfiguration UseMediatR(this IGlobalConfiguration config, IMediator mediator)
        {
            config.UseActivator(new MediatRJobActivator(mediator));

            JobHelper.SetSerializerSettings(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            });

            return config;
        }
    }
}