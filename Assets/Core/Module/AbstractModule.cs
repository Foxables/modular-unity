using Core.EventBus;
using System;
using UnityEngine;

namespace Core.Module {
    public class AbstractModule : ScriptableObject, ModuleInterface, SubscriberInterface
    {
        public Type EVENT { get; set; }
        protected EventBusInterface eventBus;

        public AbstractModule()
        {
            this.EVENT = typeof(AbstractEvent);
        }

        public AbstractModule(EventBusInterface eventBus)
        {
            this.EVENT = typeof(AbstractEvent);
            this.eventBus = eventBus;
        }

        public void Init(EventBusInterface eventBus)
        {
            this.eventBus = eventBus;
        }

        public static ModuleInterface FactoryCreateAndListen(EventBusInterface eventBus, Type T) {
            // var self = T(eventBus);
            object tmpSelf = ScriptableObject.CreateInstance(T);
            var self = (ModuleInterface)tmpSelf;
            self.Init(eventBus);
            Debug.Log("Setting up listener for " + self.EVENT + " on " + T.Name + ".");
            bool v = eventBus.Listen(self, self.EVENT);
            Debug.Log(v);
            return self;
        }

        public int Receiver(object message)
        {
            Debug.Log("Received message on abstract");
            // Do something with the message.
            return 0;
        }
    }
}
