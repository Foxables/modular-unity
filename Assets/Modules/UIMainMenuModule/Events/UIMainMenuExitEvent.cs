using Core.EventBus;
using Modules.UIMainMenuModule;

namespace Modules.UIMainMenuModule.Events
{
    public class UIMainMenuExitEvent : AbstractEvent
    {
        public UIMainMenuExitEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(UIMainMenuExitEvent);
        }
    }
}
