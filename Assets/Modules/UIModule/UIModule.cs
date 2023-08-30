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
        protected GameObject Container;

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
                    InstantiateObjectEventPayload initial = new(CONTAINER_PREFAB_PATH) {
                        Name = "UIModuleCanvas"
                    };

                    eventBus.Send(new InstantiateObjectEvent(initial));
                    return 0;
                }
                InstantiateObjectEventPayload pl= (InstantiateObjectEventPayload)message.GetPayload();
                pl.Parent = getCanvas();
                eventBus.Send(new InstantiateObjectEvent(pl));
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

        private GameObject getCanvas()
        {
            if (Container == null)
            {
                Container = FindObjectOfType<UIModuleController>().gameObject;
            }
            return Container;
        }
    }
}
