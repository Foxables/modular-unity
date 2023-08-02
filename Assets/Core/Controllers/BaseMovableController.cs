using Modules.MovableObjectModule;
using UnityEngine;

namespace Core.Controllers {
    public class BaseMovableController: MonoBehaviour, MovableObjectInterface
    {
        public float MovementSpeed = 10.0f;
        public float RotationSpeed = 100.0f;
        public float JumpForce = 10.0f;
        public float Gravity = 9.8f;

        protected Vector3 currentPosition { get; set; }
        protected Vector3 newPosition { get; set; }
        protected Quaternion currentRotation { get; set; }
        protected Quaternion newRotation { get; set; }

        public void Move(Vector3 newPosition)
        {
            this.SetMoveTo(newPosition);
        }

        public void Rotate(Quaternion newRotation)
        {
            this.SetRotateTo(newRotation);
        }

        public void SetPosition(Vector3 newPosition)
        {
            this.SetMoveTo(newPosition);
        }

        public void SetRotation(Quaternion newRotation)
        {
            this.SetRotateTo(newRotation);
        }

        public Vector3 GetPosition()
        {
            return this.currentPosition;
        }

        public Quaternion GetRotation()
        {
            return this.currentRotation;
        }

        public void SetMoveTo(Vector3 newPosition)
        {
            this.newPosition = newPosition;
        }

        public void SetRotateTo(Quaternion newRotation)
        {
            this.newRotation = newRotation;
        }

        private Rigidbody rigidBody { get; set; }

        void Start()
        {
            this.currentPosition = this.transform.position;
            this.currentRotation = this.transform.rotation;
            this.newPosition = this.currentPosition;
            this.newRotation = this.currentRotation;
        }

        public void FixedUpdate()
        {
            // Handle Player Movement.
            this.getRigidBody()
                .updateCurrentPosition()
                .updateCurrentRotation()
                .performMovementIfRequired()
                .performRotationIfRequired();
        }

        // Get Once.
        private BaseMovableController getRigidBody()
        {
            if (this.rigidBody == null) {
                this.rigidBody = this.GetComponent<Rigidbody>();
            }

            return this;
        }

        private BaseMovableController updateCurrentPosition()
        {
            this.currentPosition = this.transform.position;
            return this;
        }

        private BaseMovableController updateCurrentRotation()
        {
            this.currentRotation = this.transform.rotation;
            return this;
        }

        private BaseMovableController performMovementIfRequired()
        {
            if (Vector3.Distance(currentPosition, newPosition) > 0.1f) {
                Vector3 direction = newPosition - currentPosition;
                direction.Normalize();

                direction *= MovementSpeed * Time.deltaTime;
                Debug.Log("Moving to " + newPosition.ToString() + " with direction " + direction.ToString());
                this.rigidBody.AddForce(direction, ForceMode.Acceleration);
                // this.rigidBody.velocity = direction;
            }
            return this;
        }

        private BaseMovableController performRotationIfRequired()
        {
            if (Mathf.Abs(currentRotation.y - newRotation.y) > 0.1f) {
                var newVal = Quaternion.RotateTowards(currentRotation, newRotation, RotationSpeed * Time.deltaTime);

                Debug.Log("Rotating to " + newRotation.y.ToString() + " with from " + currentRotation.y.ToString());
                this.transform.Rotate(Vector3.up, newRotation.y * RotationSpeed * Time.deltaTime, Space.World);
                // this.transform.rotation = Quaternion.RotateTowards(currentRotation, newRotation, RotationSpeed * Time.deltaTime);
            }

            return this;
        }
    }
}
