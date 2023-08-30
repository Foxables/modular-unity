using System;
using UnityEngine;
using Core.Module;
using Core.EventBus;
using System.Collections.Generic;
using Modules.ObjectManagementModule.Entity;
using Modules.ObjectManagementModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;

namespace Modules.ObjectManagementModule {
    public class ObjectManagementModule : AbstractModule
    {
        protected const string CONTAINER_PREFAB_PATH = "Modules/ObjectManagementModule/ObjectManagementContainer";
        private List<InstantiatedObject> InstantiatedObjects = new List<InstantiatedObject>();
        private GameObject Container;

        public ObjectManagementModule()
        {
            // Can make a module listen to only 1 event, or to a list of events. Use: `this.EVENT = typeof(MovableObjectEvent);` for single event.
            EVENTS = new Type[] { typeof(InstantiateObjectEvent), typeof(DestroyObjectEvent) }; // Example of listening to a list of events.
        }

        public ObjectManagementModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;
            // Can make a module listen to only 1 event, or to a list of events. Use: `this.EVENT = typeof(MovableObjectEvent);` for single event.
            EVENTS = new Type[] { typeof(InstantiateObjectEvent), typeof(DestroyObjectEvent) }; // Example of listening to a list of events.
        }

        public override int Receiver(EventInterface message)
        {
            Debug.Log("--ObjectManagementModule: Received object event");
            Type t = message.GetType();
            if (t == typeof(InstantiateObjectEvent)) {
                InstantiateObjectFromEvent((InstantiateObjectEvent)message);
            }

            if (t == typeof(DestroyObjectEvent)) {
                DestroyObjectFromEvent((DestroyObjectEvent)message);
            }

            return 0;
        }

        private void InstantiateObjectFromEvent(InstantiateObjectEvent instantiateObjectEvent)
        {
            InstantiateObjectEventPayload pl = instantiateObjectEvent.GetPayload();
            GameObject prefab = Resources.Load(pl.PrefabPath) as GameObject;
            if (prefab == null)
            {
                Debug.Log("--ObjectManagementModule: Prefab '" + pl.PrefabPath + "' not found");
                return;
            }

            GameObject obj = Instantiate(prefab);

            if (pl.Parent != null)
            {
                obj.transform.SetParent(pl.Parent.transform);
            } else {
                if (Container == null)
                {
                    SetupContainer();
                }

                obj.transform.SetParent(Container.transform);
            }

            InstantiateGameObject(obj, pl);
        }

        private void InstantiateGameObject(GameObject gameObject, InstantiateObjectEventPayload pl)
        {
            InstantiatedObject insObj = new(gameObject);

            insObj.GetGameObject().name = insObj.GetId().ToString();

            InstantiatedObjects.Add(insObj);
            SendResponseEventIfSet(insObj.GetId().ToString(), pl);
        }

        private void DestroyObjectFromEvent(DestroyObjectEvent destroyObjectEvent)
        {
            DestroyObjectEventPayload pl = destroyObjectEvent.GetPayload();
            DestroyGameObject(pl.Target).SendResponseEventIfSet(true, pl);
        }

        private ObjectManagementModule DestroyGameObject(GameObject gameObject)
        {
            Destroy(gameObject);
            return this;
        }

        private void SendResponseEventIfSet(object response, ObjectEventPayloadInterface payload)
        {
            if (payload.GetReturnEvent() == null)
            {
                return;
            }
            Type respEvent = payload.GetReturnEvent().GetType();
            if (respEvent == null)
            {
                return;
            }
            EventInterface responseEvent = (EventInterface)Activator.CreateInstance(respEvent, response);
            eventBus.Send(responseEvent);
        }

        private void SetupContainer()
        {
            if (Container != null) {
                return;
            }
            Debug.Log("--ObjectManagementModule: Setting up container " + CONTAINER_PREFAB_PATH);
            GameObject prefab = Resources.Load<GameObject>(CONTAINER_PREFAB_PATH);
            Container = Instantiate(prefab);
            Container.name = "ObjectManagementContainer";
        }

        override public void Start()
        {
            // Run.
            SetupContainer();
        }
    }

}