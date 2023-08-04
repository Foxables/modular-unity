using UnityEngine;
using Core.Module;
using Core.Controllers;
using Core.EventBus;
using Modules.MovableObjectModule;
using Modules.MovableObjectModule.Events;

namespace Modules.WASDPlayerMovementModule {
    class WASDPlayerMovementModule: AbstractModule
    {
        private bool wActuated = false;
        private bool aActuated = false;
        private bool sActuated = false;
        private bool dActuated = false;

        private bool shouldSendEvent = false;
        private PlayerMovementController player;

        public override void Update() {
            // Capture key input.
            this.GetPlayer()
                .GetInputs()
                .DetermineIfEventShouldSend()
                .SendEvent();
        }

        private WASDPlayerMovementModule GetPlayer() {
            if (this.player == null) {
                Debug.Log("WASDPlayerMovementModule: Finding player...");
                var player = GameObject.FindObjectsOfType<PlayerMovementController>();
                if (player.Length > 1) {
                    Debug.LogError("WASDPlayerMovementModule: More than one player found.");
                } else if (player.Length == 0) {
                    Debug.LogError("WASDPlayerMovementModule: No player found.");
                } else {
                    Debug.Log("WASDPlayerMovementModule: Found player.");
                    this.player = player[0];
                }
            }

            return this;
        }

        private WASDPlayerMovementModule GetInputs() {
            this.GetWInput()
                .GetAInput()
                .GetSInput()
                .GetDInput();

            return this;
        }

        private WASDPlayerMovementModule GetWInput() {
            this.wActuated = Input.GetKey(KeyCode.W);

            return this;
        }

        private WASDPlayerMovementModule GetAInput() {
            this.aActuated = Input.GetKey(KeyCode.A);

            return this;
        }

        private WASDPlayerMovementModule GetSInput() {
            this.sActuated = Input.GetKey(KeyCode.S);

            return this;
        }

        private WASDPlayerMovementModule GetDInput() {
            this.dActuated = Input.GetKey(KeyCode.D);

            return this;
        }

        private WASDPlayerMovementModule DetermineIfEventShouldSend() {
            if (
                this.wActuated
                || this.aActuated
                || this.sActuated
                || this.dActuated
            ) {
                this.shouldSendEvent = true;
            }

            return this;
        }

        private void SendEvent() {
            if (this.shouldSendEvent == false) {
                return;
            }

            // Send event.
            this.shouldSendEvent = false;
            EventInterface wasdEvent = new MovableObjectEvent(this.GetPayload());
            this.eventBus.Send(wasdEvent);
            this.LogEvent(wasdEvent);
        }

        private void LogEvent(EventInterface wasdEvent) {
            Debug.Log("WASDPlayerMovementModule: Sent WASD Movement Event.");
        }

        private Vector3 CalculateNewPosition() {
            Vector3 newPosition = Vector3.zero;
            if (this.wActuated) {
                newPosition = Vector3.forward;
            } else if (this.sActuated) {
                newPosition = Vector3.back;
            }

            return newPosition;
        }

        private Quaternion CalculateNewRotation() {
            Quaternion newRotation = this.player.transform.rotation;

            if (this.aActuated) {
                newRotation = Quaternion.Euler(0, -180, 0);
            } else if (this.dActuated) {
                newRotation = Quaternion.Euler(0, 180, 0);
            }

            return newRotation;
        }

        private MovableObjectEventPayload GetPayload() {
            return new MovableObjectEventPayload(
                this.CalculateNewPosition(),
                this.CalculateNewRotation(),
                this.player.transform.position,
                this.player.transform.rotation,
                this.player.gameObject
            );
        }
    }
}