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

        private PublisherInterface publisher;

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

        public void InjectPublisher(PublisherInterface publisher)
        {
            this.publisher = publisher;
        }

        private PublisherInterface Publisher()
        {
            if (publisher == null) {
                Debug.LogError("PublisherInterface is null");
            }

            return publisher;
        }

        void Start()
        {
            startButton.onClick.AddListener(() => {
                EventInterface e = ScriptableObject.CreateInstance<UIMainMenuStartEvent>();
                EventInterface e1 = ScriptableObject.CreateInstance<UIMainMenuHideEvent>();
                Publisher().Dispatch(e);
                Publisher().Dispatch(e1);
            });
            closeButton.onClick.AddListener(() => {
                EventInterface e = ScriptableObject.CreateInstance<UIMainMenuHideEvent>();
                Publisher().Dispatch(e);
            });
            exitButton.onClick.AddListener(() => {
                EventInterface e = ScriptableObject.CreateInstance<UIMainMenuExitEvent>();
                Publisher().Dispatch(e);
            });
        }
    }
}
