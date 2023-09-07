using Core.EventBus;
using Core.Module;
using Modules.UIModule.Events;
using UnityEngine;
using System;
using Modules.SystemStateModule.Events;
using Modules.LevelModule.Events;
using Modules.ObjectManagementModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;

namespace Modules.LevelModule {
    public class LevelModule : AbstractModule, ModuleInterface
    {
        protected GameObject CurrentLevel;
        protected string FIRST_LEVEL_PATH = "Prefabs/Levels/Level1";

        public LevelModule()
        {
            EVENTS = new Type[] { typeof(LoadLevelEvent), typeof(UnloadLevelEvent), typeof(SystemLevelLoadedEvent), typeof(SystemGameStartEvent) };
        }

        public LevelModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;
            EVENTS = new Type[] { typeof(LoadLevelEvent), typeof(UnloadLevelEvent), typeof(SystemLevelLoadedEvent), typeof(SystemGameStartEvent) };
        }

        public override void Receiver(object message)
        {
            EventInterface e = (EventInterface)message;
            Type t = e.GetType();
            Debug.Log("--LevelModule: Received Level Event " + t.Name);

            if (t == typeof(LoadLevelEvent)) {
                InstantiateObjectEventPayload pl= (InstantiateObjectEventPayload)e.GetPayload();
                pl.ReturnEvent = typeof(SystemLevelLoadedEvent);
                PublishEvent<InstantiateObjectEvent>(pl);
            }

            if (t == typeof(UnloadLevelEvent)) {
                CurrentLevel = null;
                PublishEvent<DestroyObjectEvent>(e.GetPayload());
            }

            if (t == typeof(SystemLevelLoadedEvent)) {
                try {
                    CurrentLevel = ((InstantiatedObjectEventPayload)e.GetPayload()).Target;
                } catch (Exception ex) {
                    Debug.Log("--LevelModule: SystemLevelLoadedEvent payload is not a GameObject " + ex.Message);
                }
            }

            if (t == typeof(SystemGameStartEvent)) {
                var payload = new InstantiateObjectEventPayload(FIRST_LEVEL_PATH);
                PublishEvent<LoadLevelEvent>(payload);
            }
        }
    }
}
