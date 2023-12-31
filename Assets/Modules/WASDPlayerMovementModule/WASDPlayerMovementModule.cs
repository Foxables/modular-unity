using UnityEngine;
using Core.Module;
using Core.Controllers;
using Core.EventBus;
using Modules.MovableObjectModule.Events;
using Modules.MovableObjectModule.Events.Payloads;
using Modules.SystemStateModule.Events;

namespace Modules.WASDPlayerMovementModule
{
    class WASDPlayerMovementModule : AbstractModule
    {
        private bool wActuated = false;
        private bool aActuated = false;
        private bool sActuated = false;
        private bool dActuated = false;

        private bool shouldSendEvent = false;
        private bool hasStarted = false;
        private PlayerMovementController player;

        public WASDPlayerMovementModule()
        {
            EVENTS = new System.Type[] {
                typeof(SystemGameStartEvent)
            };
        }

        public WASDPlayerMovementModule(PublisherInterface publisher, SubscriberInterface subscriber) : base(publisher, subscriber)
        {
            Publisher = publisher;
            Subscriber = subscriber;
            EVENTS = new System.Type[] {
                typeof(SystemGameStartEvent)
            };
        }

        public override void Receiver(object message)
        {
            Debug.Log("--WASDPlayerMovementModule: Received event");
            System.Type t = message.GetType();
            if (t == typeof(SystemGameStartEvent))
            {
                hasStarted = true;
            }
        }

        public override void Update()
        {
            if (hasStarted == false)
            {
                return;
            }

            // Capture key input.
            GetPlayer()
                .GetInputs()
                .DetermineIfEventShouldSend()
                .SendEvent();
        }

        private WASDPlayerMovementModule GetPlayer()
        {
            if (player == null)
            {
                Debug.Log("WASDPlayerMovementModule: Finding player...");
                var player = GameObject.FindObjectsOfType<PlayerMovementController>();
                if (player.Length > 1)
                {
                    Debug.LogError("WASDPlayerMovementModule: More than one player found.");
                }
                else if (player.Length == 0)
                {
                    Debug.LogError("WASDPlayerMovementModule: No player found.");
                }
                else
                {
                    Debug.Log("WASDPlayerMovementModule: Found player.");
                    this.player = player[0];
                }
            }

            return this;
        }

        private WASDPlayerMovementModule GetInputs()
        {
            GetWInput()
                .GetAInput()
                .GetSInput()
                .GetDInput();

            return this;
        }

        private WASDPlayerMovementModule GetWInput()
        {
            wActuated = Input.GetKey(KeyCode.W);

            return this;
        }

        private WASDPlayerMovementModule GetAInput()
        {
            aActuated = Input.GetKey(KeyCode.A);

            return this;
        }

        private WASDPlayerMovementModule GetSInput()
        {
            sActuated = Input.GetKey(KeyCode.S);

            return this;
        }

        private WASDPlayerMovementModule GetDInput()
        {
            dActuated = Input.GetKey(KeyCode.D);

            return this;
        }

        private WASDPlayerMovementModule DetermineIfEventShouldSend()
        {
            if (
                wActuated
                || aActuated
                || sActuated
                || dActuated
            )
            {
                shouldSendEvent = true;
            }

            return this;
        }

        private void SendEvent()
        {
            if (shouldSendEvent == false)
            {
                return;
            }

            // Send event.
            shouldSendEvent = false;
            PublishEvent<MovableObjectEvent>(GetPayload());
        }

        private Vector3 CalculateNewPosition()
        {
            Vector3 newPosition = Vector3.zero;
            if (wActuated)
            {
                newPosition = Vector3.forward;
            }
            else if (sActuated)
            {
                newPosition = Vector3.back;
            }

            return newPosition;
        }

        private Quaternion CalculateNewRotation()
        {
            Quaternion newRotation = player.transform.rotation;

            if (aActuated)
            {
                newRotation = Quaternion.Euler(0, -180, 0);
            }
            else if (dActuated)
            {
                newRotation = Quaternion.Euler(0, 180, 0);
            }

            return newRotation;
        }

        private MovableObjectEventPayload GetPayload()
        {
            return new MovableObjectEventPayload(
                CalculateNewPosition(),
                CalculateNewRotation(),
                player.transform.position,
                player.transform.rotation,
                player.gameObject
            );
        }
    }
}