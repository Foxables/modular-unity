using Core.EventBus;
using Modules.SoundManagementModule;

namespace Modules.SoundManagementModule.Events
{
    public class SoundManagementModuleInitialisedEvent : AbstractEvent
    {
        public SoundManagementModuleInitialisedEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(SoundManagementModuleInitialisedEvent);
        }
    }
}
