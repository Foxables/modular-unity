using System;

namespace Core.EventBus {
    public class Subscriber: SubscriberInterface
    {
        protected EventBusInterface eventBus;

        public Subscriber(EventBusInterface eventBus)
        {
            this.eventBus = eventBus;
        }

        public void Subscribe<T>(Action<object> subscriber) where T : EventInterface
        {
            eventBus.RegisterSubscriber(subscriber, typeof(T));
        }

        public void Subscribe(Action<object> subscriber, Type type)
        {
            eventBus.RegisterSubscriber(subscriber, type);
        }

        public virtual int Receiver(EventInterface message)
        {
            // Do something with the message.
            return 0;
        }
    }
}