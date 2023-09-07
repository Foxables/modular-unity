using Core.EventBus;
using Modules.SoundManagementModule;

namespace Modules.SoundManagementModule.Events
{
    public class StopBackgroundMusicEvent : AbstractEvent
    {
        public StopBackgroundMusicEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(StopBackgroundMusicEvent);
        }
    }
}
