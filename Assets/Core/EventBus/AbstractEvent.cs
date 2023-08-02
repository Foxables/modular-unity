namespace Core.EventBus {
    public class AbstractEvent : EventInterface
    {
        private object payload;

        public AbstractEvent(object payload)
        {
            this.payload = payload;
        }

        public object GetPayload()
        {
            return this.payload;
        }
    }
}
