using Core.EventBus;
using Modules.SoundManagementModule;

namespace Modules.SoundManagementModule.Events
{
    public class SetSFXVolumeEvent : AbstractEvent
    {
        public SetSFXVolumeEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(SetSFXVolumeEvent);
        }
    }
}
