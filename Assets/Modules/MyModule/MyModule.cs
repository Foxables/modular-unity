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
            this.EVENT = typeof(MyEvent);
        }

        public MyModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;
            this.EVENT = typeof(MyEvent);
        }

        private void DoSomething(MyEvent myEvent)
        {
            // Send an event of type MyEvent to the event bus.
            Debug.Log("MyModule: " + myEvent.GetPayload());
        }

        public new int Receiver(object message)
        {
            Debug.Log("MyModule: Received message");
            MyEvent myEvent = (MyEvent)message;
            // Do something with the message.
            this.DoSomething(myEvent);
            return 0;
        }
    }
}
