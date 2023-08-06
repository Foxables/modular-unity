using Core.EventBus;
using Core.Module;
using Modules.MovableObjectModule.Events;
using UnityEngine;
using System;

namespace Modules.MovableObjectModule {
    public class MovableObjectModule : AbstractModule, ModuleInterface
    {
        public MovableObjectModule()
        {
            // Can make a module listen to only 1 event, or to a list of events. Use: `this.EVENT = typeof(MovableObjectEvent);` for single event.
            this.EVENTS = new Type[] { typeof(MovableObjectEvent) }; // Example of listening to a list of events.
        }

        public MovableObjectModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;
            // Can make a module listen to only 1 event, or to a list of events. Use: `this.EVENT = typeof(MovableObjectEvent);` for single event.
            this.EVENTS = new Type[] { typeof(MovableObjectEvent) }; // Example of listening to a list of events.
        }

        private void DoSomething(MovableObjectEvent moveEvent)
        {
            MovableObjectEventPayload pl = moveEvent.GetPayload();
            MovableObjectInterface movable = pl.target.GetComponent<MovableObjectInterface>();
            if (movable == null)
            {
                Debug.Log("--MovableObjectModule: Target does not have MovableObjectInterface");
                return;
            }

            movable.SetMoveTo(pl.newPosition);
            movable.SetRotateTo(pl.newRotation);
        }

        public override int Receiver(object message)
        {
            Debug.Log("--MovableObjectModule: Received movement event");
            MovableObjectEvent myEvent = (MovableObjectEvent)message;
            // Do something with the message.
            this.DoSomething(myEvent);
            return 0;
        }
    }
}
