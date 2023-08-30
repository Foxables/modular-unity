using System;
using UnityEngine;

namespace Modules.ObjectManagementModule.Events.Payloads
{
    public class InstantiateObjectEventPayload : ObjectEventPayloadInterface
    {
        public GameObject Parent { get; set; }
        public string Name { get; set; }
        public string PrefabPath { get; set; }
        public Type ReturnEvent { get; set; }
        public Vector3 Location { get; set; }
        public Quaternion Rotation { get; set; }

        public InstantiateObjectEventPayload(string PrefabPath)
        {
            this.PrefabPath = PrefabPath;
        }

        public InstantiateObjectEventPayload(string PrefabPath, GameObject Parent)
        {
            this.PrefabPath = PrefabPath;
            this.Parent = Parent;
        }

        public InstantiateObjectEventPayload(string PrefabPath, Type ReturnEvent)
        {
            this.PrefabPath = PrefabPath;
            this.ReturnEvent = ReturnEvent;
        }

        public InstantiateObjectEventPayload(string PrefabPath, GameObject Parent, Type ReturnEvent)
        {
            this.PrefabPath = PrefabPath;
            this.ReturnEvent = ReturnEvent;
        }

        public InstantiateObjectEventPayload(string PrefabPath, Vector3 Location, Quaternion Rotation)
        {
            this.PrefabPath = PrefabPath;
            this.Location = Location;
            this.Rotation = Rotation;
        }

        public InstantiateObjectEventPayload(string PrefabPath, Vector3 Location, Quaternion Rotation, GameObject Parent)
        {
            this.PrefabPath = PrefabPath;
            this.Location = Location;
            this.Rotation = Rotation;
            this.Parent = Parent;
        }

        public InstantiateObjectEventPayload(string PrefabPath, Vector3 Location, Quaternion Rotation, Type ReturnEvent)
        {
            this.PrefabPath = PrefabPath;
            this.Location = Location;
            this.Rotation = Rotation;
            this.ReturnEvent = ReturnEvent;
        }

        public InstantiateObjectEventPayload(string PrefabPath, Vector3 Location, Quaternion Rotation, GameObject Parent, Type ReturnEvent)
        {
            this.PrefabPath = PrefabPath;
            this.Location = Location;
            this.Rotation = Rotation;
            this.Parent = Parent;
            this.ReturnEvent = ReturnEvent;
        }

        public Type GetReturnEvent()
        {
            return this.ReturnEvent;
        }
    }
}