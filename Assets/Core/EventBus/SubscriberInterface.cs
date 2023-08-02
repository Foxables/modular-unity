namespace Core.EventBus {
    public interface SubscriberInterface
    {
        public int Receiver(object message);
    }
}
