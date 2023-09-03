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

        public UIModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;
            EVENTS = new Type[] { typeof(InstantiateUIObjectEvent), typeof(DestroyUIObjectEvent) };
        }

        public override void Receiver(object message)
        {
            Debug.Log("--UIModule: Received UI Event");
            EventInterface e = (EventInterface)message;
            Type t = e.GetType();

            if (t == typeof(InstantiateUIObjectEvent)) {
                if (e.GetPayload() == null) {
                    InstantiateObjectEventPayload initial = new(CONTAINER_PREFAB_PATH) {
                        Name = "UIModuleCanvas"
                    };
                    PublishEvent<InstantiateObjectEvent>(initial);
                    return;
                }
                InstantiateObjectEventPayload pl= (InstantiateObjectEventPayload)e.GetPayload();
                pl.Parent = GetCanvas();

                PublishEvent<InstantiateObjectEvent>(pl);
            }

            if (t == typeof(DestroyUIObjectEvent)) {
                PublishEvent<DestroyObjectEvent>(e.GetPayload());
            }
        }

        override public void Start() {
            Debug.Log("--UIModule: Start");
            PublishEvent<InstantiateUIObjectEvent>(null);
        }

        private GameObject GetCanvas()
        {
            if (Container == null)
            {
                Container = FindObjectOfType<UIModuleController>().gameObject;
            }
            return Container;
        }
    }
}
