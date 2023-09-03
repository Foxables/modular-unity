using Core.EventBus;
using Core.Module;
using Modules.MyModule.Events;
using System;
using UnityEngine;

namespace Modules.MyModule {
    public class MyModule : AbstractModule, ModuleInterface
    {
        public MyModule()
        {
            EVENT = typeof(MyEvent);
        }

        public MyModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;
            EVENT = typeof(MyEvent);
        }

        private void DoSomething(MyEvent myEvent)
        {
            // Send an event of type MyEvent to the event bus.
            Debug.Log("MyModule: " + myEvent.GetPayload());
        }

        public override void Receiver(object message)
        {
            Debug.Log("MyModule: Received message");
            MyEvent myEvent = (MyEvent)message;
            // Do something with the message.
            DoSomething(myEvent);
        }
    }
}
