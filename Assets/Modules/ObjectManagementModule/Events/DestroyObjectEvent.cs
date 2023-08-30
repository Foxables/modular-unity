using Core.EventBus;
using Modules.ObjectManagementModule.Events.Payloads;

namespace Modules.ObjectManagementModule.Events
{
    public class DestroyObjectEvent : AbstractEvent
    {
        public DestroyObjectEvent(object payload) : base(payload)
        {}

        public new DestroyObjectEventPayload GetPayload()
        {
            return (DestroyObjectEventPayload)base.GetPayload();
        }

        public new System.Type GetType()
        {
            return typeof(DestroyObjectEvent);
        }
    }
}
