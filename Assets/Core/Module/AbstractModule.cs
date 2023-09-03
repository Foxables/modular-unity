using Core.EventBus;
using System;
using UnityEngine;

namespace Core.Module
{
    public class AbstractModule : ScriptableObject, ModuleInterface
    {
        public Type EVENT { get; set; }
        public Type[] EVENTS { get; set; }
        protected SubscriberInterface Subscriber;
        protected PublisherInterface Publisher;

        public AbstractModule()
        {
            EVENT = typeof(AbstractEvent);
        }

        public AbstractModule(PublisherInterface publisher, SubscriberInterface subscriber)
        {
            EVENT = typeof(AbstractEvent);
            Publisher = publisher;
            Subscriber = subscriber;
        }

        public bool Init(PublisherInterface publisher, SubscriberInterface subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;
            SetupEventListener().SetupListenersForEachEvent();
            return true;
        }

        public virtual void Update()
        {
            // Do nothing.
        }

        public virtual void Start()
        {
            // Do nothing.
        }

        public static ModuleInterface FactoryCreateAndListen(PublisherInterface publisher, SubscriberInterface subscriber, Type T)
        {
            object tmpSelf = CreateInstance(T);
            var self = (ModuleInterface)tmpSelf;

            self.Init(publisher, subscriber);
            return self;
        }

        private AbstractModule SetupEventListener()
        {
            if (EVENT != null)
            {
                SetupListenerForEvent(EVENT);
            }

            return this;
        }

        private AbstractModule SetupListenersForEachEvent()
        {
            if (EVENTS != null)
            {
                foreach (var e in EVENTS)
                {
                    SetupListenerForEvent(e);
                }
            }

            return this;
        }

        private AbstractModule SetupListenerForEvent(Type Event)
        {
            Subscriber.Subscribe(Receiver, Event);
            return this;
        }

        public virtual void Receiver(object message)
        {
            Debug.Log("Received message on abstract");
        }

        protected void PublishEvent<T> (object payload) where T : AbstractEvent
        {
            EventInterface e = CreateInstance<T>();
            e.SetPayload(payload);
            Publisher.Dispatch(e);
        }
    }
}
