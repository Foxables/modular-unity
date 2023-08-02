using UnityEngine;
namespace Modules.MovableObjectModule {
    public class MovableObjectEventPayload
    {
        public Vector3 newPosition { get; set; }
        public Quaternion newRotation { get; set; }
        public Vector3 currentPosition { get; set; }
        public Quaternion currentRotation { get; set; }
        public GameObject target { get; set; }

        public MovableObjectEventPayload(Vector3 newPosition, Quaternion newRotation, Vector3 currentPosition, Quaternion currentRotation, GameObject target)
        {
            this.newPosition = newPosition;
            this.newRotation = newRotation;
            this.currentPosition = currentPosition;
            this.currentRotation = currentRotation;
            this.target = target;
        }
    }
}