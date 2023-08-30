using UnityEngine;
namespace Core.EventBus {
    public class AbstractEvent : ScriptableObject, EventInterface
    {
        private object payload;

        public AbstractEvent()
        {}

        public AbstractEvent(object payload)
        {
            this.payload = payload;
        }

        public object GetPayload()
        {
            return payload;
        }

        public void SetPayload(object payload)
        {
            this.payload = payload;
        }
    }
}
