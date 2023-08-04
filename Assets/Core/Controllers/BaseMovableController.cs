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
            this.newPosition = Vector3.zero;
            this.newRotation = new Quaternion();
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
            if (Vector3.zero == newPosition) {
                return this;
            }

            Debug.Log("----BaseMovableController: Moving");

            if (newPosition == Vector3.forward) {
                this.rigidBody.AddForce(this.transform.forward * MovementSpeed);
            } else {
                this.rigidBody.AddForce((this.transform.forward * -1) * MovementSpeed);
            }
            this.SetMoveTo(Vector3.zero);

            return this;
        }

        private BaseMovableController performRotationIfRequired()
        {
            if (newRotation.y < 1f && newRotation.y > -1f) {
                return this;
            }
            Debug.Log("----BaseMovableController: Rotating");

            if (newRotation.y == 1f) {
                this.transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
            } else if (newRotation.y == -1f) {
                this.transform.Rotate(Vector3.down * RotationSpeed * Time.deltaTime);
            }

            this.SetRotateTo(new Quaternion());

            return this;
        }
    }
}
