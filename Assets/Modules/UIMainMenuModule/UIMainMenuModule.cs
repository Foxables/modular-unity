using System;
using UnityEngine;
using Core.Module;
using Core.EventBus;

using Modules.UIModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;
using Modules.UIMainMenuModule.Events;
using Modules.SystemStateModule.Events;

namespace Modules.UIMainMenuModule {
    public class UIMainMenuModule : AbstractModule
    {
        protected const string PREFAB_PATH = "Modules/UIMainMenuModule/UIMainMenu";
        private GameObject objectInstance;

        public UIMainMenuModule()
        {
            EVENTS = new Type[] {
                typeof(UIMainMenuHideEvent),
                typeof(UIMainMenuShowEvent),
                typeof(UIMainMenuExitEvent),
                typeof(UIMainMenuStartEvent),
                typeof(UIMainMenuInitialisedEvent)
            };
        }

        public UIMainMenuModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;

            EVENTS = new Type[] {
                typeof(UIMainMenuHideEvent),
                typeof(UIMainMenuShowEvent),
                typeof(UIMainMenuExitEvent),
                typeof(UIMainMenuStartEvent),
                typeof(UIMainMenuInitialisedEvent)
            };
        }

        public override int Receiver(EventInterface message)
        {
            Debug.Log("--UIMainMenuModule: Received object event");
            Type t = message.GetType();
            if (t == typeof(UIMainMenuHideEvent)) {
                // Hide the main menu.
                GetInstance().Toggle(false);
            }

            if (t == typeof(UIMainMenuShowEvent)) {
                // Show the main menu.
                GetInstance().Toggle(true);
            }

            if (t == typeof(UIMainMenuExitEvent)) {
                // Show the main menu.
                eventBus.Send(new SystemExitEvent(null));
            }

            if (t == typeof(UIMainMenuInitialisedEvent)) {
                // Set the event bus instance on the Main Menu Controller.
                GetInstance().objectInstance.GetComponent<UIMainMenuController>().SetEventBus(eventBus);
            }

            return 0;
        }

        private UIMainMenuModule GetInstance()
        {
            if (objectInstance == null) {
                objectInstance = FindFirstObjectByType<UIMainMenuController>().gameObject;
            }

            return this;
        }

        private void Toggle(bool show) {
            if (show) {
                GetInstance().objectInstance.GetComponent<UIMainMenuController>().Show();
            } else {
                GetInstance().objectInstance.GetComponent<UIMainMenuController>().Hide();
            }
        }

        override public void Start()
        {
            InstantiateObjectEventPayload pl = new(PREFAB_PATH)
            {
                ReturnEvent = typeof(UIMainMenuInitialisedEvent)
            };
            eventBus.Send(new InstantiateUIObjectEvent(pl));
        }
    }
}
