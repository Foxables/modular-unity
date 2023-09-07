using Core.EventBus;
using Modules.SoundManagementModule;

namespace Modules.SoundManagementModule.Events
{
    public class PlayBackgroundMusicEvent : AbstractEvent
    {
        public PlayBackgroundMusicEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(PlayBackgroundMusicEvent);
        }
    }
}
