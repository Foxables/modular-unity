using System;
using UnityEngine;

namespace Modules.ObjectManagementModule.Events.Payloads {
    interface ObjectEventPayloadInterface
    {
        public Type GetReturnEvent();
    }
}