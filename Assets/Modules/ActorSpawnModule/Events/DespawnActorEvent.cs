using Core.EventBus;
using Modules.ActorSpawnModule;

namespace Modules.ActorSpawnModule.Events
{
    public class DespawnActorEvent : AbstractEvent
    {
        public DespawnActorEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(DespawnActorEvent);
        }
    }
}
