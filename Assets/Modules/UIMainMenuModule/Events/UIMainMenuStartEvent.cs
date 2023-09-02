using Core.EventBus;
using Modules.UIMainMenuModule;

namespace Modules.UIMainMenuModule.Events
{
    public class UIMainMenuStartEvent : AbstractEvent
    {
        public UIMainMenuStartEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(UIMainMenuStartEvent);
        }
    }
}
