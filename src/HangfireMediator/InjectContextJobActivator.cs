using System;
using Hangfire;
using Hangfire.Server;
using mediatr_hangfire_console;
using Microsoft.Extensions.DependencyInjection;

public class InjectContextJobActivator : JobActivator
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    public InjectContextJobActivator(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    public override JobActivatorScope BeginScope(PerformContext context)
    {
        return new Scope(context, _scopeFactory.CreateScope());
    }

    private class Scope : JobActivatorScope, IServiceProvider
    {
        private readonly PerformContext _context;
        private readonly IServiceScope _scope;

        public Scope(PerformContext context, IServiceScope scope)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
        
        public override object Resolve(Type type)
        {
            return ActivatorUtilities.GetServiceOrCreateInstance(this, type);
        }

        object IServiceProvider.GetService(Type serviceType)
        {
            if (serviceType == typeof(PerformContext))
                return _context;
            return _scope.ServiceProvider.GetService(serviceType);
        }
    }
}
