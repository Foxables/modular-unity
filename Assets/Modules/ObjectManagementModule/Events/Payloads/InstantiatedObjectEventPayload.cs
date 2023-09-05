using System;
using UnityEngine;

namespace Modules.ObjectManagementModule.Events.Payloads {
    public class InstantiatedObjectEventPayload : ObjectEventPayloadInterface
    {
        public GameObject Target { get; set; }
        public string UUID { get; set; }
        public Type ReturnEvent { get; set; }

        public InstantiatedObjectEventPayload(GameObject Target)
        {
            this.Target = Target;
        }
        
        public InstantiatedObjectEventPayload(string UUID, GameObject Target)
        {
            this.Target = Target;
            this.UUID = UUID;
        }

        public InstantiatedObjectEventPayload(GameObject Target, Type ReturnEvent)
        {
            this.Target = Target;
            this.ReturnEvent = ReturnEvent;
        }

        public Type GetReturnEvent()
        {
            return this.ReturnEvent;
        }
    }
}