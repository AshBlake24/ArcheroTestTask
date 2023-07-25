using System;
using System.Collections.Generic;

namespace Source.Infrastructure.Services
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
        
        private static ServiceLocator s_instance;
        public static ServiceLocator Container => s_instance ??= new ServiceLocator();

        public void RegisterSingle<TService>(TService instance) where TService : IService => 
            _services.Add(typeof(TService), instance);

        public TService Single<TService>() where TService : class, IService
        {
            if (_services.ContainsKey(typeof(TService)))
                return _services[typeof(TService)] as TService;
            else
                throw new ArgumentNullException($"Service {typeof(TService)} does not exist!");
        }
    }
}