using System;
using UnityEngine;
using Core.EventBus;
using Core.Module;
using Modules.SystemStateModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;
using Modules.ActorSpawnModule.Events;
using Modules.ObjectManagementModule.Events;

namespace Modules.ActorSpawnModule {
    class ActorSpawnModule: AbstractModule, ModuleInterface {
        public ActorSpawnModule()
        {
            EVENTS = new Type[] { typeof(SystemLevelLoadedEvent), typeof(SpawnActorEvent), typeof(DespawnActorEvent) };
        }

        public ActorSpawnModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;
            EVENTS = new Type[] { typeof(SystemLevelLoadedEvent), typeof(SpawnActorEvent), typeof(DespawnActorEvent) };
        }

        public override void Receiver(object message)
        {
            EventInterface e = (EventInterface)message;
            Type t = e.GetType();
            Debug.Log("--ActorSpawnModule: Received Level Event " + t.Name);

            if (t == typeof(SystemLevelLoadedEvent)) {
                var CurrentLevel = ((InstantiatedObjectEventPayload)e.GetPayload()).Target;
                var spawnPoints = CurrentLevel.GetComponentsInChildren<ActorSpawnController>();
                foreach (var spawnPoint in spawnPoints) {
                    spawnPoint.SetPublisher(Publisher);
                    spawnPoint.Init();
                }
            }

            if (t == typeof(SpawnActorEvent)) {
                PublishEvent<InstantiateObjectEvent>(e.GetPayload());
            }
        }
    }
}