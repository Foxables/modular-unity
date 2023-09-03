using System;
using UnityEngine;
using Core.Module;
using Core.EventBus;

using Modules.ObjectManagementModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;
using Modules.SystemStateModule.Events;


namespace Modules.PlayerModule {
    public class PlayerModule : AbstractModule
    {
        protected const string PREFAB_PATH = "Player";
        private GameObject Container;

        public PlayerModule()
        {
            EVENTS = new Type[] {
                typeof(SystemGameStartEvent)
            };
        }

        public PlayerModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;

            EVENTS = new Type[] {
                typeof(SystemGameStartEvent)
            };
        }

        public override void Receiver(object message)
        {
            Debug.Log("--PlayerModule: Received object event");
            Type t = message.GetType();
            if (t == typeof(SystemGameStartEvent)) {
                InstantiateObjectEventPayload pl = new(PREFAB_PATH);
                PublishEvent<InstantiateObjectEvent>(pl);
            }
        }
    }
}
