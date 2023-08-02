using Core.EventBus;

namespace Modules.MyModule.Events
{
    public class MyEvent : AbstractEvent
    {
        public MyEvent(object payload) : base(payload)
        {}

        // This allows us to get the payload cast as a specific type.
        public new string GetPayload()
        {
            return (string)base.GetPayload();
        }
    }
}
