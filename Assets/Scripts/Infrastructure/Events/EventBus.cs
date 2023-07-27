using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Infrastructure.Events
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, SubscribersList<IGlobalSubscriber>> s_subscribers;
        private static readonly Dictionary<Type, List<Type>> s_cashedSubscriberTypes;

        static EventBus()
        {
            s_subscribers = new Dictionary<Type, SubscribersList<IGlobalSubscriber>>();
            s_cashedSubscriberTypes = new Dictionary<Type, List<Type>>();
        }

        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = GetSubscriberTypes(subscriber);
            
            foreach (Type type in subscriberTypes)
            {
                if (s_subscribers.ContainsKey(type) == false)
                    s_subscribers[type] = new SubscribersList<IGlobalSubscriber>();
                
                s_subscribers[type].Add(subscriber);
            }
        }
        
        public static void Unsubscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = GetSubscriberTypes(subscriber);
            
            foreach (Type type in subscriberTypes)
            {
                if (s_subscribers.ContainsKey(type))
                    s_subscribers[type].Remove(subscriber);
            }
        }
        
        public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, IGlobalSubscriber
        {
            SubscribersList<IGlobalSubscriber> subscribersList = s_subscribers[typeof(TSubscriber)];
            
            subscribersList.StartExecuting();
            
            foreach (IGlobalSubscriber subscriber in subscribersList.Subscribers)
            {
                try
                {
                    action.Invoke(subscriber as TSubscriber);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            
            subscribersList.StopExecuting();
            subscribersList.Cleanup();
        }
        
        private static List<Type> GetSubscriberTypes(IGlobalSubscriber globalSubscriber)
        {
            Type type = globalSubscriber.GetType();
            
            if (s_cashedSubscriberTypes.ContainsKey(type))
                return s_cashedSubscriberTypes[type];
            
            List<Type> subscriberTypes = type
                .GetInterfaces()
                .Where(it =>
                    it != typeof(IGlobalSubscriber) &&
                    typeof(IGlobalSubscriber).IsAssignableFrom(it))
                .ToList();
            
            s_cashedSubscriberTypes[type] = subscriberTypes;
            
            return subscriberTypes;
        }
    }
}