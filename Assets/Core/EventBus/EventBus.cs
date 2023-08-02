using System.Collections.Generic;
using System;
using Core.Module;

namespace Core.EventBus {
    public class EventBus : EventBusInterface
    {
        private Dictionary<Type, List<ModuleInterface>> subscribers = new Dictionary<Type, List<ModuleInterface>>();

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
        }
    }
}
