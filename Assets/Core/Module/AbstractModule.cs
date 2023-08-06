using Core.EventBus;
using System;
using UnityEngine;

namespace Core.Module {
    public class AbstractModule : ScriptableObject, ModuleInterface, SubscriberInterface
    {
        public Type EVENT { get; set; }
        public Type[] EVENTS { get; set; }
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

        public bool Init(EventBusInterface eventBus)
        {
            this.eventBus = eventBus;
            this.SetupEventListener().SetupListenersForEachEvent();
            return true;
        }

        public virtual void Update()
        {
            // Do nothing.
        }

        public static ModuleInterface FactoryCreateAndListen(EventBusInterface eventBus, Type T) {
            object tmpSelf = ScriptableObject.CreateInstance(T);
            var self = (ModuleInterface)tmpSelf;

            self.Init(eventBus);
            return self;
        }

        private AbstractModule SetupEventListener()
        {
            if (this.EVENT != null) {
                this.SetupListenerForEvent(this.EVENT);
            }

            return this;
        }

        private AbstractModule SetupListenersForEachEvent()
        {
            if (this.EVENTS != null) {
                foreach (var e in this.EVENTS) {
                    this.SetupListenerForEvent(e);
                }
            }

            return this;
        }

        private AbstractModule SetupListenerForEvent(Type Event)
        {
            // Debug.Log("Setting up listener for " + Event + " on " + this.Name + ".");
            eventBus.Listen(this, Event);
            return this;
        }

        public virtual int Receiver(EventInterface message)
        {
            Debug.Log("Received message on abstract");
            // Do something with the message.
            return 0;
        }
    }
}
