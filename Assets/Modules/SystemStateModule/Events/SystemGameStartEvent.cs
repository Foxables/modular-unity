using Core.EventBus;
using Modules.SystemStateModule;

namespace Modules.SystemStateModule.Events
{
    public class SystemGameStartEvent : AbstractEvent
    {
        public SystemGameStartEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(SystemGameStartEvent);
        }
    }
}
