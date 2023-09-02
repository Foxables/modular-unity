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

        public PlayerModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;
            EVENTS = new Type[] {
                typeof(SystemGameStartEvent)
            };
        }

        public override int Receiver(EventInterface message)
        {
            Debug.Log("--PlayerModule: Received object event");
            Type t = message.GetType();
            if (t == typeof(SystemGameStartEvent)) {
                InstantiateObjectEventPayload pl = new(PREFAB_PATH);
                eventBus.Send(new InstantiateObjectEvent(pl));
            }

            return 0;
        }
    }
}
