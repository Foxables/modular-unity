using Core.EventBus;
using Modules.SoundManagementModule;

namespace Modules.SoundManagementModule.Events
{
    public class SetBackgroundMusicVolumeEvent : AbstractEvent
    {
        public SetBackgroundMusicVolumeEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(SetBackgroundMusicVolumeEvent);
        }
    }
}
