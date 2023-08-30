using Core.EventBus;
using Modules.UIModule;

namespace Modules.UIModule.Events
{
    public class UIInteractionEvent : AbstractEvent
    {
        public UIInteractionEvent(object payload) : base(payload)
        {}

        // public new DestroyObjectEventPayload GetPayload()
        // {
        //     return (DestroyObjectEventPayload)base.GetPayload();
        // }

        public new System.Type GetType()
        {
            return typeof(UIInteractionEvent);
        }
    }
}
