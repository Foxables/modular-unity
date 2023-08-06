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
        private EventBusInterface eventBus = new EventBus.EventBus();
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
            Debug.Log("ModuleContainer created.");
        }

        void Start()
        {
            Debug.Log("ModuleContainer started.");
            this.LoadIgnoreModulesFile().LoadModules().InitializeModules();
        }

        void Update()
        {
            foreach (var module in this.moduleUpdateFunctions)
            {
                module();
            }
        }

        private ModuleContainer InitializeModules()
        {
            Debug.Log("Initializing " + this.modules.Count + " Modules...");
            foreach (var module in this.modules)
            {
                this.InitializeModuleIfNotIgnored(module);
            }

            Debug.Log("All Modules Initialized.");

            return this;
        }

        private void InitializeModuleIfNotIgnored(Type module)
        {
            if (!this.ignoreModules.Contains(module.Name))
            {
                this.InitializeModule(module);
            }
        }

        private void InitializeModule(Type module)
        {
            Debug.Log("Initializing Module: " + module.Name);
            // Inject Dependency into Subscriber.
            ModuleInterface m = AbstractModule.FactoryCreateAndListen(eventBus, module);
            this.initializedModules.Add(m);
            this.moduleUpdateFunctions.Add(m.Update);
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

        private ModuleContainer LoadIgnoreModulesFile()
        {
            // Filename: ../.ignoremodules
            if (System.IO.File.Exists(ignoreFile))
            {
                string[] lines = System.IO.File.ReadAllLines(ignoreFile);
                foreach (string line in lines)
                {
                    this.ignoreModules.Add(line);
                }
            } else {
                Debug.Log("No ignore file found. " + ignoreFile);
            }

            return this;
        }
    }
}

    //  EOF
