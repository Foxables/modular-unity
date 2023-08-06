using Core.EventBus;
using Modules.ObjectManagementModule;
namespace Modules.ObjectManagementModule.Events
{
    public class InstantiateObjectEvent : AbstractEvent
    {
        public InstantiateObjectEvent(object payload) : base(payload)
        {}

        public new InstantiateObjectEventPayload GetPayload()
        {
            return (InstantiateObjectEventPayload)base.GetPayload();
        }

        public new System.Type GetType()
        {
            return typeof(InstantiateObjectEvent);
        }
    }
}
