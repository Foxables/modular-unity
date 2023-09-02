using System.Security;
using UnityEngine;
using UnityEngine.UI;

using Core.EventBus;
using Modules.UIMainMenuModule.Events;

namespace Modules.UIInGameUIModule {
    public class UIInGameUIController: MonoBehaviour
    {
        public Button openMainMenuButton;

        private EventBusInterface eventBus;

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
            openMainMenuButton.onClick.AddListener(() => {
                EventBus().Send(new Modules.UIMainMenuModule.Events.UIMainMenuShowEvent(null));
            });
        }
    }
}
