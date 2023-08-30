using Core.EventBus;
using Modules.UIMainMenuModule;

namespace Modules.UIMainMenuModule.Events
{
    public class UIMainMenuShowEvent : AbstractEvent
    {
        public UIMainMenuShowEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(UIMainMenuShowEvent);
        }
    }
}
