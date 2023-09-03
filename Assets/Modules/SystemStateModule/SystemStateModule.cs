using System;
using UnityEngine;
using Core.Module;
using Core.EventBus;

using Modules.SystemStateModule.Events;

namespace Modules.SystemStateModule {
    public class SystemStateModule : AbstractModule
    {
        public SystemStateModule()
        {
            EVENTS = new Type[] {
                typeof(SystemExitEvent),
                typeof(SystemStartEvent)
            };
        }

        public SystemStateModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;

            EVENTS = new Type[] {
                typeof(SystemExitEvent),
                typeof(SystemStartEvent)
            };
        }

        public override void Receiver(object message)
        {
            Debug.Log("--SystemStateModule: Received event");
            Type t = message.GetType();
            if (t == typeof(SystemExitEvent)) {
                // Hide the main menu.
                Application.Quit();
                if (UnityEditor.EditorApplication.isPlaying == true) {
                    Debug.Log("--SystemStateModule: Exited application in editor.");
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            }
        }
    }
}