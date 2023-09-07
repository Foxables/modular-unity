using System;
using UnityEngine;
using Core.Module;
using Core.EventBus;
using System.Collections.Generic;
using Modules.ObjectManagementModule.Entity;
using Modules.ObjectManagementModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;
using System.Xml;

namespace Modules.ObjectManagementModule {
    public class ObjectManagementModule : AbstractModule
    {
        protected const string CONTAINER_PREFAB_PATH = "Modules/ObjectManagementModule/ObjectManagementContainer";
        private readonly List<InstantiatedObject> InstantiatedObjects = new();
        private GameObject Container;

        public ObjectManagementModule()
        {
            // Can make a module listen to only 1 event, or to a list of events. Use: `this.EVENT = typeof(MovableObjectEvent);` for single event.
            EVENTS = new Type[] { typeof(InstantiateObjectEvent), typeof(DestroyObjectEvent) }; // Example of listening to a list of events.
        }

        public ObjectManagementModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;
            // Can make a module listen to only 1 event, or to a list of events. Use: `this.EVENT = typeof(MovableObjectEvent);` for single event.
            EVENTS = new Type[] { typeof(InstantiateObjectEvent), typeof(DestroyObjectEvent) }; // Example of listening to a list of events.
        }

        public override void Receiver(object message)
        {
            Type t = message.GetType();
            Debug.Log("--ObjectManagementModule: Received " + t.Name + " event");
            if (t == typeof(InstantiateObjectEvent)) {
                InstantiateObjectFromEvent((InstantiateObjectEvent)message);
            }

            if (t == typeof(DestroyObjectEvent)) {
                DestroyObjectFromEvent((DestroyObjectEvent)message);
            }
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
            InstantiatedObject insObj = CreateInstance<InstantiatedObject>();
            insObj.SetGameObject(gameObject);

            if (string.IsNullOrEmpty(pl.Name) == false)
            {
                insObj.GetGameObject().name = pl.Name;
            }

            if (pl.Location != null)
            {
                insObj.GetGameObject().transform.position = pl.Location;
            }

            if (pl.Rotation != null)
            {
                insObj.GetGameObject().transform.rotation = pl.Rotation;
            }

            InstantiatedObjects.Add(insObj);
            var obj = new InstantiatedObjectEventPayload(insObj.GetId().ToString(), insObj.GetGameObject());
            SendResponseEventIfSet(obj, pl);
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
            Type respEvent = payload.GetReturnEvent();
            Debug.Log("--ObjectManagementModule: Sending response event " + respEvent.Name);
            if (respEvent == null)
            {
                return;
            }
            object tmpSelf = CreateInstance(respEvent);
            EventInterface responseEvent = (EventInterface)tmpSelf;
            responseEvent.SetPayload(response);
            Publisher.Dispatch(responseEvent);
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