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
        private struct state {
            public bool wActuated;
            public bool aActuated;
            public bool sActuated;
            public bool dActuated;
        }
        private state previousState;
        private bool shouldSendEvent = false;
        private PlayerMovementController player;

        public override void Update() {
            // Capture key input.
            this.GetPlayer()
                .GetInputs()
                .CompareState()
                .UpdateState()
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
            if (Input.GetKey(KeyCode.W)) {
                this.wActuated = true;
            } else {
                this.wActuated = false;
            }

            return this;
        }

        private WASDPlayerMovementModule GetAInput() {
            if (Input.GetKey(KeyCode.A)) {
                this.aActuated = true;
            } else {
                this.aActuated = false;
            }

            return this;
        }

        private WASDPlayerMovementModule GetSInput() {
            if (Input.GetKey(KeyCode.S)) {
                this.sActuated = true;
            } else {
                this.sActuated = false;
            }

            return this;
        }

        private WASDPlayerMovementModule GetDInput() {
            if (Input.GetKey(KeyCode.D)) {
                this.dActuated = true;
            } else {
                this.dActuated = false;
            }

            return this;
        }

        private WASDPlayerMovementModule CompareState() {
            if (this.wActuated != this.previousState.wActuated) {
                this.shouldSendEvent = true;
            }

            if (this.aActuated != this.previousState.aActuated) {
                this.shouldSendEvent = true;
            }

            if (this.sActuated != this.previousState.sActuated) {
                this.shouldSendEvent = true;
            }

            if (this.dActuated != this.previousState.dActuated) {
                this.shouldSendEvent = true;
            }

            return this;
        }

        private WASDPlayerMovementModule UpdateState() {
            this.previousState.wActuated = this.wActuated;
            this.previousState.aActuated = this.aActuated;
            this.previousState.sActuated = this.sActuated;
            this.previousState.dActuated = this.dActuated;

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
            Debug.Log("Sent WASD Movement Event.");
            Debug.Log("Payload: " + wasdEvent.GetPayload().ToString());
        }

        private Vector3 CalculateNewPosition() {
            Vector3 newPosition = this.player.transform.position;

            if (this.wActuated) {
                newPosition += this.player.transform.forward * this.player.MovementSpeed;
            }

            if (this.sActuated) {
                newPosition -= this.player.transform.forward * this.player.MovementSpeed;
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