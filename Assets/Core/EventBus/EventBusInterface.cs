using System;
using Core.Module;
namespace Core.EventBus {
    public interface EventBusInterface
    {
        bool Listen(ModuleInterface subscriber, Type type);
        public void Send(EventInterface message);
        public bool RegisterSubscriber(Action<object> callback, Type type);
    }
}
