using Core.EventBus;
using Modules.UIInGameUIModule;

namespace Modules.UIInGameUIModule.Events
{
    public class UIInGameUIShowEvent : AbstractEvent
    {
        public UIInGameUIShowEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(UIInGameUIShowEvent);
        }
    }
}
