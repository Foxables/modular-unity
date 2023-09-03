namespace Core.EventBus {
    public interface PublisherInterface
    {
        public void Dispatch(EventInterface message);
    }
}
