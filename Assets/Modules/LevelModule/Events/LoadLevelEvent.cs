using Core.EventBus;
using Modules.LevelModule;

namespace Modules.LevelModule.Events
{
    public class LoadLevelEvent : AbstractEvent
    {
        public LoadLevelEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(LoadLevelEvent);
        }
    }
}
