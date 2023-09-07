using Core.EventBus;
using Modules.SoundManagementModule;

namespace Modules.SoundManagementModule.Events
{
    public class SoundManagementModuleInitialiseEvent : AbstractEvent
    {
        public SoundManagementModuleInitialiseEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(SoundManagementModuleInitialiseEvent);
        }
    }
}
