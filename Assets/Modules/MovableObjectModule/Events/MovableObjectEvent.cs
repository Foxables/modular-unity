using Core.EventBus;

namespace Modules.MovableObjectModule.Events
{
    public class MovableObjectEvent : AbstractEvent
    {
        public MovableObjectEvent(object payload) : base(payload)
        {}

        // This allows us to get the payload cast as a specific type.
        public new MovableObjectEventPayload GetPayload()
        {
            return (MovableObjectEventPayload)base.GetPayload();
        }
    }
}
