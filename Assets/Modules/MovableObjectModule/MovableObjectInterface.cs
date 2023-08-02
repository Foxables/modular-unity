using UnityEngine;
namespace Modules.MovableObjectModule {
    interface MovableObjectInterface
    {
        void Move(Vector3 newPosition);
        void Rotate(Quaternion newRotation);
        void SetPosition(Vector3 newPosition);
        void SetRotation(Quaternion newRotation);
        Vector3 GetPosition();
        Quaternion GetRotation();
        void SetMoveTo(Vector3 newPosition);
        void SetRotateTo(Quaternion newPosition);
    }
}
