using System;
using UnityEngine;
using Core.Module;
using Core.EventBus;

using Modules.ObjectManagementModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;


namespace Modules.PlayerModule {
    public class PlayerModule : AbstractModule
    {
        protected const string PREFAB_PATH = "Player";
        private GameObject Container;

        override public void Start()
        {
            InstantiateObjectEventPayload pl = new(PREFAB_PATH);
            eventBus.Send(new InstantiateObjectEvent(pl));
        }
    }
}
