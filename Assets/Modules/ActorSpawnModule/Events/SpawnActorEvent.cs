using Core.EventBus;
using Modules.ActorSpawnModule;

namespace Modules.ActorSpawnModule.Events
{
    public class SpawnActorEvent : AbstractEvent
    {
        public SpawnActorEvent(object payload) : base(payload)
        {}

        public new System.Type GetType()
        {
            return typeof(SpawnActorEvent);
        }
    }
}
