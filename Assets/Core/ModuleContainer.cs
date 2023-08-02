using Core.EventBus;
using Core.Module;
using Modules.MyModule.Events;
using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

namespace Core {
    public class ModuleContainer: MonoBehaviour
    {
        private EventBusInterface eventBus = new EventBus.EventBus();
        private List<ModuleInterface> initializedModules = new List<ModuleInterface>();
        private List<Type> modules = new List<Type>();

        public static ModuleContainer FactoryCreate()
        {
            return new ModuleContainer();
        }

        ModuleContainer()
        {
            Debug.Log("ModuleContainer created.");
        }

        void Start()
        {
            Debug.Log("ModuleContainer started.");
            this.LoadModules().InitializeModules();
        }

        void Update()
        {
            try {
                this.eventBus.Send(new MyEvent("Hello World!"));
                Debug.Log("Sent Event!");
            } catch (Exception e) {
                Debug.Log("Exception: " + e);
            }
        }

        private ModuleContainer InitializeModules()
        {
            Debug.Log("Initializing " + this.modules.Count + " Modules...");
            foreach (var module in this.modules)
            {
                Debug.Log("Initializing Module: " + module.Name);
                // Inject Dependency into Subscriber.
                this.initializedModules.Add(AbstractModule.FactoryCreateAndListen(eventBus, module));
            }

            Debug.Log("All Modules Initialized.");

            return this;
        }

        private ModuleContainer LoadModules()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (Type t in assembly.GetTypes())
            {
                if (t.FindInterfaces((Type type, object criteria) => {
                    return type.Name == "ModuleInterface";
                }, null).Length > 0) {
                    this.modules.Add(t);
                }
            }

            Debug.Log(this.modules.Count + " modules were loaded.");

            return this;
        }
    }
}

    //  EOF
