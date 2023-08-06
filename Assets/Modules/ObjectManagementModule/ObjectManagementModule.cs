using System;
using UnityEngine;
using Core.Module;
using Core.EventBus;
using System.Collections.Generic;
using Modules.ObjectManagementModule.Events;

namespace Modules.ObjectManagementModule {
    public class ObjectManagementModule : AbstractModule
    {
        private List<InstantiatedObject> InstantiatedObjects = new List<InstantiatedObject>();

        public ObjectManagementModule()
        {
            // Can make a module listen to only 1 event, or to a list of events. Use: `this.EVENT = typeof(MovableObjectEvent);` for single event.
            this.EVENTS = new System.Type[] { typeof(InstantiateObjectEvent), typeof(DestroyObjectEvent) }; // Example of listening to a list of events.
        }

        public ObjectManagementModule(EventBusInterface eventBus) : base(eventBus)
        {
            this.eventBus = eventBus;
            // Can make a module listen to only 1 event, or to a list of events. Use: `this.EVENT = typeof(MovableObjectEvent);` for single event.
            this.EVENTS = new System.Type[] { typeof(InstantiateObjectEvent), typeof(DestroyObjectEvent) }; // Example of listening to a list of events.
        }

        public override int Receiver(EventInterface message)
        {
            Debug.Log("--ObjectManagementModule: Received object event");
            Type t = message.GetType();
            if (t == typeof(InstantiateObjectEvent)) {
                this.InstantiateObjectFromEvent((InstantiateObjectEvent)message);
            }

            if (t == typeof(DestroyObjectEvent)) {
                this.DestroyObjectFromEvent((DestroyObjectEvent)message);
            }

            return 0;
        }

        private void InstantiateObjectFromEvent(InstantiateObjectEvent instantiateObjectEvent)
        {
            InstantiateObjectEventPayload pl = instantiateObjectEvent.GetPayload();
            GameObject prefab = Resources.Load(pl.PrefabPath) as GameObject;
            if (prefab == null)
            {
                Debug.Log("--ObjectManagementModule: Prefab not found");
                return;
            }

            GameObject obj = GameObject.Instantiate(prefab);

            if (pl.Parent != null)
            {
                obj.transform.SetParent(pl.Parent.transform);
            }

            this.InstantiateGameObject(obj, pl);
        }

        private void InstantiateGameObject(GameObject gameObject, InstantiateObjectEventPayload pl)
        {
            InstantiatedObject insObj = new InstantiatedObject(gameObject);

            insObj.GetGameObject().name = insObj.GetId().ToString();

            this.InstantiatedObjects.Add(insObj);
            this.SendResponseEventIfSet(insObj.GetId().ToString(), pl);
        }

        private void DestroyObjectFromEvent(DestroyObjectEvent destroyObjectEvent)
        {
            DestroyObjectEventPayload pl = destroyObjectEvent.GetPayload();
            this.DestroyGameObject(pl.Target).SendResponseEventIfSet(true, pl);
        }

        private ObjectManagementModule DestroyGameObject(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
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
            this.eventBus.Send(responseEvent);
        }

    }

}