using Core.EventBus;
using Modules.UIInGameUIModule;

namespace Modules.UIInGameUIModule.Events
{
    public class UIInGameUIInitialisedEvent : AbstractEvent
    {
        public UIInGameUIInitialisedEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(UIInGameUIInitialisedEvent);
        }
    }
}
