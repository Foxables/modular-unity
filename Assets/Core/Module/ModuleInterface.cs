using System;
using Core.EventBus;
namespace Core.Module {
    public interface ModuleInterface
    {
        public void Init(EventBusInterface eventBus);
        int Receiver(object message);
        public Type EVENT { get; set; }
    }
}
