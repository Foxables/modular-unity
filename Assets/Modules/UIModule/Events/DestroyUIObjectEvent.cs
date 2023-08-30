using Core.EventBus;
using Modules.UIModule;

namespace Modules.UIModule.Events
{
    public class DestroyUIObjectEvent : AbstractEvent
    {
        public DestroyUIObjectEvent(object payload) : base(payload)
        {}

        // public new DestroyObjectEventPayload GetPayload()
        // {
        //     return (DestroyObjectEventPayload)base.GetPayload();
        // }

        public new System.Type GetType()
        {
            return typeof(DestroyUIObjectEvent);
        }
    }
}
