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
        static readonly string ignoreFile = Application.dataPath + "/.ignoremodule";
        private readonly EventBusInterface EventBus;
        private readonly PublisherInterface Publisher;
        private readonly SubscriberInterface Subscriber;
        private List<ModuleInterface> initializedModules = new List<ModuleInterface>();
        private List<Type> modules = new List<Type>();
        private List<Action> moduleUpdateFunctions = new List<Action>();
        private List<string> ignoreModules = new List<string>();

        public static ModuleContainer FactoryCreate()
        {
            return new ModuleContainer();
        }

        ModuleContainer()
        {
            EventBus = new EventBus.EventBus();
            Publisher = new Publisher(EventBus);
            Subscriber = new Subscriber(EventBus);
        }

        void Start()
        {
            Debug.Log("ModuleContainer started.");
            LoadIgnoreModulesFile().LoadModules().InitializeModules().StartModules();
        }

        void Update()
        {
            foreach (var module in moduleUpdateFunctions)
            {
                module();
            }
        }

        private ModuleContainer InitializeModules()
        {
            Debug.Log("Initializing " + modules.Count + " Modules...");
            foreach (var module in modules)
            {
                InitializeModuleIfNotIgnored(module);
            }

            Debug.Log("All Modules Initialized.");

            return this;
        }

        private void InitializeModuleIfNotIgnored(Type module)
        {
            if (!ignoreModules.Contains(module.Name))
            {
                InitializeModule(module);
            }
        }

        private ModuleContainer StartModules()
        {
            Debug.Log("Initializing " + modules.Count + " Modules...");
            foreach (var module in initializedModules)
            {
                TriggerStartModuleIfNotIgnored(module);
            }

            Debug.Log("All Modules Initialized.");

            return this;
        }

        private void TriggerStartModuleIfNotIgnored(ModuleInterface module)
        {
            if (!ignoreModules.Contains(module.GetType().Name))
            {
                StartModule(module);
            }
        }

        private void StartModule(ModuleInterface module)
        {
            Debug.Log("Starting Module: " + module.GetType().Name);
            module.Start();
        }

        private void InitializeModule(Type module)
        {
            Debug.Log("Initializing Module: " + module.Name);
            // Inject Dependency into Subscriber.
            ModuleInterface m = AbstractModule.FactoryCreateAndListen(Publisher, Subscriber, module);
            initializedModules.Add(m);
            moduleUpdateFunctions.Add(m.Update);
        }

        private ModuleContainer LoadModules()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (Type t in assembly.GetTypes())
            {
                if (t.FindInterfaces((Type type, object criteria) => {
                    return type.Name == "ModuleInterface";
                }, null).Length > 0) {
                    modules.Add(t);
                }
            }

            Debug.Log(modules.Count + " modules were loaded.");

            return this;
        }

        private ModuleContainer LoadIgnoreModulesFile()
        {
            // Filename: ../.ignoremodules
            if (System.IO.File.Exists(ignoreFile))
            {
                string[] lines = System.IO.File.ReadAllLines(ignoreFile);
                foreach (string line in lines)
                {
                    ignoreModules.Add(line);
                }
            } else {
                Debug.Log("No ignore file found. " + ignoreFile);
            }

            return this;
        }
    }
}

    //  EOF
