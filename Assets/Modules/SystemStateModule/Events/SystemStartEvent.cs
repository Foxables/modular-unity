using Core.EventBus;
using Modules.SystemStateModule;

namespace Modules.SystemStateModule.Events
{
    public class SystemStartEvent : AbstractEvent
    {
        public SystemStartEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(SystemStartEvent);
        }
    }
}
