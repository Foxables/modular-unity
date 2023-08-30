namespace Core.EventBus
{
    public interface EventInterface
    {
        public object GetPayload();
        public void SetPayload(object payload);
    }
}