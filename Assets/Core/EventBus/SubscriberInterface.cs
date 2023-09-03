using System;

namespace Core.EventBus {
    public interface SubscriberInterface
    {
        public int Receiver(EventInterface message);
        public void Subscribe<T>(Action<object> subscriber) where T : EventInterface;
        public void Subscribe(Action<object> subscriber, Type type);
    }
}
