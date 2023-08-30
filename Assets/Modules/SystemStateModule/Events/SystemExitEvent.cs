using Core.EventBus;
using Modules.SystemStateModule;

namespace Modules.SystemStateModule.Events
{
    public class SystemExitEvent : AbstractEvent
    {
        public SystemExitEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(SystemExitEvent);
        }
    }
}
