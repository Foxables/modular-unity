using Core.EventBus;
using Modules.LevelModule;

namespace Modules.LevelModule.Events
{
    public class UnloadLevelEvent : AbstractEvent
    {
        public UnloadLevelEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(UnloadLevelEvent);
        }
    }
}
