using System;
using Core.EventBus;
namespace Core.Module {
    public interface ModuleInterface
    {
        public bool Init(EventBusInterface eventBus);
        int Receiver(EventInterface message);
        public Type EVENT { get; set; }
        public void Update();

        public void Start();
    }
}
