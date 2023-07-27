using System.Collections.Generic;

namespace Source.Infrastructure.Events
{
    public class SubscribersList<TSubscriber> where TSubscriber : class
    {
        private readonly List<TSubscriber> _subscribers = new List<TSubscriber>();

        private bool _needsCleanUp;
        private bool _executing;

        public IReadOnlyList<TSubscriber> Subscribers => _subscribers;

        public void StartExecuting() => _executing = true;

        public void StopExecuting() => _executing = false;

        public void Add(TSubscriber subscriber) =>
            _subscribers.Add(subscriber);

        public void Remove(TSubscriber subscriber)
        {
            if (_executing)
            {
                int subscriberIndex = _subscribers.IndexOf(subscriber);

                if (subscriberIndex >= 0)
                {
                    _needsCleanUp = true;
                    _subscribers[subscriberIndex] = null;
                }
            }
            else
            {
                _subscribers.Remove(subscriber);
            }
        }

        public void Cleanup()
        {
            if (_needsCleanUp == false)
                return;

            _subscribers.RemoveAll(s => s == null);
            _needsCleanUp = false;
        }
    }
}