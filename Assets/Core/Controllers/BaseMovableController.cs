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
            SetMoveTo(newPosition);
        }

        public void Rotate(Quaternion newRotation)
        {
            SetRotateTo(newRotation);
        }

        public void SetPosition(Vector3 newPosition)
        {
            SetMoveTo(newPosition);
        }

        public void SetRotation(Quaternion newRotation)
        {
            SetRotateTo(newRotation);
        }

        public Vector3 GetPosition()
        {
            return currentPosition;
        }

        public Quaternion GetRotation()
        {
            return currentRotation;
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
            currentPosition = transform.position;
            currentRotation = transform.rotation;
            newPosition = Vector3.zero;
            newRotation = new Quaternion();
        }

        public void FixedUpdate()
        {
            // Handle Player Movement.
            getRigidBody()
                .updateCurrentPosition()
                .updateCurrentRotation()
                .performMovementIfRequired()
                .performRotationIfRequired();
        }

        // Get Once.
        private BaseMovableController getRigidBody()
        {
            if (rigidBody == null) {
                rigidBody = GetComponent<Rigidbody>();
            }

            return this;
        }

        private BaseMovableController updateCurrentPosition()
        {
            currentPosition = transform.position;
            return this;
        }

        private BaseMovableController updateCurrentRotation()
        {
            currentRotation = transform.rotation;
            return this;
        }

        private BaseMovableController performMovementIfRequired()
        {
            if (Vector3.zero == newPosition) {
                return this;
            }


            if (newPosition == Vector3.forward) {
                rigidBody.AddForce(transform.forward * MovementSpeed);
            } else {
                rigidBody.AddForce((transform.forward * -1) * MovementSpeed);
            }
            SetMoveTo(Vector3.zero);

            return this;
        }

        private BaseMovableController performRotationIfRequired()
        {
            if (newRotation.y < 1f && newRotation.y > -1f) {
                return this;
            }

            if (newRotation.y == 1f) {
                transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
            } else if (newRotation.y == -1f) {
                transform.Rotate(Vector3.down * RotationSpeed * Time.deltaTime);
            }

            SetRotateTo(new Quaternion());

            return this;
        }
    }
}
