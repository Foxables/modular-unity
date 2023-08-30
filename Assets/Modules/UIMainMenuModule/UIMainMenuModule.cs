using System;
using UnityEngine;
using Core.Module;
using Core.EventBus;

using Modules.UIModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;
using Modules.UIMainMenuModule.Events;
using Unity.VisualScripting;
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
                getInstance().toggle(false);
            }

            if (t == typeof(UIMainMenuShowEvent)) {
                // Show the main menu.
                getInstance().toggle(true);
            }

            if (t == typeof(UIMainMenuExitEvent)) {
                // Show the main menu.
                eventBus.Send(new SystemExitEvent(null));
            }

            if (t == typeof(UIMainMenuInitialisedEvent)) {
                // Set the event bus instance on the Main Menu Controller.
                getInstance().objectInstance.GetComponent<UIMainMenuController>().SetEventBus(eventBus);
            }

            return 0;
        }

        private UIMainMenuModule getInstance()
        {
            if (this.objectInstance == null) {
                this.objectInstance = GameObject.FindFirstObjectByType<UIMainMenuController>().gameObject;
            }

            return this;
        }

        private void toggle(bool show) {
            if (show) {
                getInstance().objectInstance.GetComponent<UIMainMenuController>().Show();
            } else {
                getInstance().objectInstance.GetComponent<UIMainMenuController>().Hide();
            }
        }

        override public void Start()
        {
            InstantiateObjectEventPayload pl = new(PREFAB_PATH);
            pl.ReturnEvent = typeof(UIMainMenuInitialisedEvent);
            eventBus.Send(new InstantiateUIObjectEvent(pl));
        }
    }
}
