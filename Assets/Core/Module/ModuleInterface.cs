using System;
using Core.EventBus;
namespace Core.Module {
    public interface ModuleInterface
    {
        public bool Init(PublisherInterface publisher, SubscriberInterface subscriber);
        void Receiver(object message);
        public Type EVENT { get; set; }
        public void Update();

        public void Start();
    }
}
