using UnityEngine;
using System;
using Core.EventBus;
using Core.Module;
using Modules.SoundManagementModule.Events;
using Modules.ObjectManagementModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;

namespace Modules.SoundManagementModule {
    class SoundManagementModule: AbstractModule, ModuleInterface {
        protected const string PREFAB_PATH = "Modules/SoundManagementModule/SoundManager";
        private SoundManagementControllerInterface controller;

        SoundManagementModule() {
            EVENTS = new System.Type[] {
                typeof(SoundManagementModuleInitialiseEvent),
                typeof(SoundManagementModuleInitialisedEvent),
                typeof(PlayBackgroundMusicEvent),
                typeof(PlaySFXEvent),
                typeof(StopBackgroundMusicEvent),
                typeof(SetSFXVolumeEvent),
                typeof(SetBackgroundMusicVolumeEvent),
            };
        }

        public SoundManagementModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;
            EVENTS = new System.Type[] {
                typeof(SoundManagementModuleInitialiseEvent),
                typeof(SoundManagementModuleInitialisedEvent),
                typeof(PlayBackgroundMusicEvent),
                typeof(PlaySFXEvent),
                typeof(StopBackgroundMusicEvent),
                typeof(SetSFXVolumeEvent),
                typeof(SetBackgroundMusicVolumeEvent),
            };
        }

        public override void Receiver(object message)
        {
            Debug.Log("--SoundManagementModule: Received event");
            Type t = message.GetType();
            if (t == typeof(SoundManagementModuleInitialiseEvent)) {
                var payload = new InstantiateObjectEventPayload(PREFAB_PATH);
                payload.ReturnEvent = typeof(SoundManagementModuleInitialisedEvent);
                PublishEvent<InstantiateObjectEvent>(payload);
            }
        }

        private SoundManagementControllerInterface GetContainer()
        {
            if (controller == null)
            {
                controller = FindObjectOfType<SoundManagementController>();
            }
            return controller;
        }

        override public void Start() {
            Debug.Log("--UIModule: Start");
            PublishEvent<SoundManagementModuleInitialiseEvent>(null);
        }
    }
}