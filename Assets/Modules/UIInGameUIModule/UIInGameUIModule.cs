using System;
using UnityEngine;
using Core.Module;
using Core.EventBus;

using Modules.UIModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;
using Modules.UIMainMenuModule.Events;
using Modules.UIInGameUIModule.Events;

namespace Modules.UIInGameUIModule {
    public class UIInGameUIModule : AbstractModule
    {
        protected const string PREFAB_PATH = "Modules/UIInGameUIModule/InGameUI";
        private GameObject objectInstance;

        public UIInGameUIModule()
        {
            EVENTS = new Type[] {
                typeof(UIInGameUIShowEvent),
                typeof(UIInGameUIInitialisedEvent),
                typeof(UIMainMenuStartEvent)
            };
        }

        public UIInGameUIModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;
            EVENTS = new Type[] {
                typeof(UIInGameUIShowEvent),
                typeof(UIInGameUIInitialisedEvent),
                typeof(UIMainMenuStartEvent)
            };
        }

        public override int Receiver(EventInterface message)
        {
            Debug.Log("--UIMainMenuModule: Received object event");
            Type t = message.GetType();
            if (t == typeof(UIMainMenuStartEvent) || t == typeof(UIInGameUIShowEvent)) {
                InstantiateObjectEventPayload pl = new(PREFAB_PATH)
                {
                    ReturnEvent = typeof(UIInGameUIInitialisedEvent)
                };
                eventBus.Send(new InstantiateUIObjectEvent(pl));
            } else if (t == typeof(UIInGameUIInitialisedEvent)) {
                GetInstance().objectInstance.GetComponent<UIInGameUIController>().SetEventBus(eventBus);
            }

            return 0;
        }

        private UIInGameUIModule GetInstance()
        {
            if (objectInstance == null) {
                objectInstance = FindFirstObjectByType<UIInGameUIController>().gameObject;
            }

            return this;
        }
    }
}
