using System;
using UnityEngine;

namespace Modules.ObjectManagementModule {
    public class DestroyObjectEventPayload : ObjectEventPayloadInterface
    {
        public GameObject Target { get; set; }
        public Type ReturnEvent { get; set; }

        public DestroyObjectEventPayload(GameObject Target)
        {
            this.Target = Target;
        }

        public DestroyObjectEventPayload(GameObject Target, Type ReturnEvent)
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