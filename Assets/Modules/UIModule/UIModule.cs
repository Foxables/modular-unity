using Core.EventBus;
using Core.Module;
using Modules.UIModule.Events;
using UnityEngine;
using System;
using Modules.ObjectManagementModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;

namespace Modules.UIModule {
    public class UIModule : AbstractModule, ModuleInterface
    {
        protected const string CONTAINER_PREFAB_PATH = "Modules/UIModule/UIModuleCanvas";
        public UIModule()
        {
            EVENTS = new Type[] { typeof(InstantiateUIObjectEvent), typeof(DestroyUIObjectEvent) };
        }

        public UIModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;
            EVENTS = new Type[] { typeof(InstantiateUIObjectEvent), typeof(DestroyUIObjectEvent) };
        }

        public override int Receiver(EventInterface message)
        {
            Debug.Log("--UIModule: Received UI Event");
            Type t = message.GetType();
            if (t == typeof(InstantiateUIObjectEvent)) {
                if (message.GetPayload() == null) {
                    InstantiateObjectEventPayload pl = new(CONTAINER_PREFAB_PATH);
                    eventBus.Send(new InstantiateObjectEvent(pl));
                    return 0;
                }
                eventBus.Send(new InstantiateObjectEvent(message.GetPayload()));
            }

            if (t == typeof(DestroyUIObjectEvent)) {
                eventBus.Send(new DestroyObjectEvent(message.GetPayload()));
            }

            return 0;
        }

        override public void Start() {
            Debug.Log("--UIModule: Start");
            eventBus.Send(new InstantiateUIObjectEvent(null));
        }
    }
}
