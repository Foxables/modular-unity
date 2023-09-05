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

        private bool hasStarted = false;

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

        public UIMainMenuModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;

            EVENTS = new Type[] {
                typeof(UIMainMenuHideEvent),
                typeof(UIMainMenuShowEvent),
                typeof(UIMainMenuExitEvent),
                typeof(UIMainMenuStartEvent),
                typeof(UIMainMenuInitialisedEvent)
            };
        }

        public override void Receiver(object message)
        {
            Debug.Log("--UIMainMenuModule: Received object event");
            Type t = message.GetType();

            if (t == typeof(UIMainMenuStartEvent)) {
                if (hasStarted == true) {
                    return;
                }
                hasStarted = true;
                // Start the game.
                PublishEvent<SystemGameStartEvent>(null);
            }

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
                PublishEvent<SystemExitEvent>(null);
            }

            if (t == typeof(UIMainMenuInitialisedEvent)) {
                // Set the event bus instance on the Main Menu Controller.
                GetInstance().objectInstance.GetComponent<UIMainMenuController>().InjectPublisher(Publisher);
            }

            return;
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
            PublishEvent<InstantiateUIObjectEvent>(pl);
        }
    }
}
