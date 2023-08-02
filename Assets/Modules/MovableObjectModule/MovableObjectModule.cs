using Core.EventBus;
using Core.Module;
using Modules.MovableObjectModule.Events;
using System;
using UnityEngine;

namespace Modules.MovableObjectModule {
    public class MovableObjectModule : AbstractModule, ModuleInterface
    {
        public MovableObjectModule()
        {
            this.EVENT = typeof(MovableObjectEvent);
        }

        public MovableObjectModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;
            this.EVENT = typeof(MovableObjectEvent);
        }

        private void DoSomething(MovableObjectEvent moveEvent)
        {
            Debug.Log("MovableObjectModule: Received movement event");
            MovableObjectEventPayload pl = moveEvent.GetPayload();
            MovableObjectInterface movable = pl.target.GetComponent<MovableObjectInterface>();
            if (movable == null)
            {
                Debug.Log("MovableObjectModule: Target does not have MovableObjectInterface");
                return;
            }

            Debug.Log("MovableObjectModule: Moving object to " + pl.newPosition.ToString());

            movable.SetMoveTo(pl.newPosition);
            movable.SetRotateTo(pl.newRotation);
        }

        public override int Receiver(object message)
        {
            Debug.Log("MovableObjectModule: Received message");
            MovableObjectEvent myEvent = (MovableObjectEvent)message;
            // Do something with the message.
            this.DoSomething(myEvent);
            return 0;
        }
    }
}
