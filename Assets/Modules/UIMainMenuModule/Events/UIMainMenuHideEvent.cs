using Core.EventBus;
using Modules.UIMainMenuModule;

namespace Modules.UIMainMenuModule.Events
{
    public class UIMainMenuHideEvent : AbstractEvent
    {
        public UIMainMenuHideEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(UIMainMenuHideEvent);
        }
    }
}
