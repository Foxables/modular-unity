using UnityEngine;
using UnityEngine.UI;

using Core.EventBus;
using Modules.UIMainMenuModule.Events;

namespace Modules.UIInGameUIModule {
    public class UIInGameUIController: MonoBehaviour
    {
        public Button openMainMenuButton;

        private PublisherInterface publisher;

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
            openMainMenuButton.onClick.AddListener(() => {
                EventInterface e = ScriptableObject.CreateInstance<UIMainMenuShowEvent>();
                Publisher().Dispatch(e);
            });
        }
    }
}
