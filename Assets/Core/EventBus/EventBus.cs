using System.Collections.Generic;
using System;
using Core.Module;

namespace Core.EventBus {
    public class EventBus : EventBusInterface
    {
        private Dictionary<Type, List<ModuleInterface>> subscribers = new Dictionary<Type, List<ModuleInterface>>();
        private Dictionary<Type, IList<Action<object>>> subscribersV2 = new Dictionary<Type, IList<Action<object>>>();

        public bool Listen(ModuleInterface subscriber, Type type)
        {
            if (subscribers.ContainsKey(type)) {
                subscribers[type].Add(subscriber);
                return false;
            }

            var list = new List<ModuleInterface>();
            list.Add(subscriber);
            subscribers.Add(type, list);
            return true;
        }

        public bool RegisterSubscriber(Action<object> callback, Type type)
        {
            if (subscribersV2.ContainsKey(type))
            {
                subscribersV2[type].Add(callback);
                return false;
            }

            var list = new List<Action<object>>
            {
                callback
            };
            subscribersV2.Add(type, list);
            return true;
        }

        public void Send(EventInterface message)
        {
            var type = message.GetType();
            if (subscribers.ContainsKey(type))
            {
                foreach (var subscriber in subscribers[type])
                {
                    subscriber.Receiver(message);
                }
            }

            if (subscribersV2.ContainsKey(type))
            {
                foreach (var subscriber in subscribersV2[type])
                {
                    subscriber.Invoke(message);
                }
            }
        }
    }
}
