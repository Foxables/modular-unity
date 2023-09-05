using Core.EventBus;
using Modules.SystemStateModule;

namespace Modules.SystemStateModule.Events
{
    public class SystemLevelLoadedEvent : AbstractEvent
    {
        public SystemLevelLoadedEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(SystemLevelLoadedEvent);
        }
    }
}
