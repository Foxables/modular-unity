using System.Security;
using UnityEngine;
using UnityEngine.UI;

using Core.EventBus;
using Modules.UIMainMenuModule.Events;

namespace Modules.UIMainMenuModule {
    public class UIMainMenuController: MonoBehaviour
    {
        public Button startButton;
        public Button exitButton;
        public Button closeButton;

        private EventBusInterface eventBus;

        public void Hide()
        {
            Debug.Log("Hiding main menu");
            gameObject.SetActive(false);
        }

        public void Show()
        {
            Debug.Log("Showing main menu");
            gameObject.SetActive(true);
        }

        public void SetEventBus(EventBusInterface eventBus)
        {
            this.eventBus = eventBus;
        }

        private EventBusInterface EventBus()
        {
            if (eventBus == null) {
                Debug.LogError("EventBusInterface is null");
            }

            return eventBus;
        }

        void Start()
        {
            startButton.onClick.AddListener(() => {
                EventBus().Send(new UIMainMenuStartEvent(null));
                EventBus().Send(new UIMainMenuHideEvent(null));
            });
            closeButton.onClick.AddListener(() => {
                EventBus().Send(new UIMainMenuHideEvent(null));
            });
            exitButton.onClick.AddListener(() => {
                EventBus().Send(new UIMainMenuExitEvent(null));
            });
        }
    }
}
