using Core.EventBus;
using Modules.UIMainMenuModule;

namespace Modules.UIMainMenuModule.Events
{
    public class UIMainMenuInitialisedEvent : AbstractEvent
    {
        public UIMainMenuInitialisedEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(UIMainMenuInitialisedEvent);
        }
    }
}
