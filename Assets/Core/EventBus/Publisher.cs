namespace Core.EventBus {
    public class Publisher : PublisherInterface
    {
        private EventBusInterface eventBus;

        public Publisher(EventBusInterface eventBus)
        {
            this.eventBus = eventBus;
        }

        public void Dispatch(EventInterface message)
        {
            eventBus.Send(message);
        }
    }
}
