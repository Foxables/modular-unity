using Core.EventBus;
using Core.Module;
using Modules.MovableObjectModule.Events;
using Modules.MovableObjectModule.Events.Payloads;
using System;

namespace Modules.MovableObjectModule {
    public class MovableObjectModule : AbstractModule, ModuleInterface
    {
        public MovableObjectModule()
        {
            EVENTS = new Type[] { typeof(MovableObjectEvent) };
        }

        public MovableObjectModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;
            EVENTS = new Type[] { typeof(MovableObjectEvent) };
        }

        private void DoSomething(MovableObjectEvent moveEvent)
        {
            MovableObjectEventPayload pl = moveEvent.GetPayload();
            MovableObjectInterface movable = pl.target.GetComponent<MovableObjectInterface>();
            if (movable == null)
            {
                return;
            }

            movable.SetMoveTo(pl.newPosition);
            movable.SetRotateTo(pl.newRotation);
        }

        public override int Receiver(EventInterface message)
        {
            MovableObjectEvent myEvent = (MovableObjectEvent)message;
            // Do something with the message.
            DoSomething(myEvent);
            return 0;
        }
    }
}
