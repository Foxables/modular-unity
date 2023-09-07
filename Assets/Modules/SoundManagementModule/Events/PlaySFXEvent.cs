using Core.EventBus;
using Modules.SoundManagementModule;

namespace Modules.SoundManagementModule.Events
{
    public class PlaySFXEvent : AbstractEvent
    {
        public PlaySFXEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(PlaySFXEvent);
        }
    }
}
