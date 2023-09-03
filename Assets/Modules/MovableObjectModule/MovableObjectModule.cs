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

        public MovableObjectModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;
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

        public override void Receiver(object message)
        {
            MovableObjectEvent myEvent = (MovableObjectEvent)message;
            // Do something with the message.
            DoSomething(myEvent);
        }
    }
}
