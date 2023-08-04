using Core.EventBus;
using Core.Module;
using Modules.MovableObjectModule.Events;
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
