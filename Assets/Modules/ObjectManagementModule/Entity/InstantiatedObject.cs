using UnityEngine;
namespace Modules.ObjectManagementModule.Entity {
    public class InstantiatedObject : ScriptableObject
    {
        private GameObject GameObject { get; set; }
        private long Id { get; }
        private bool Destroyed = false;

        public InstantiatedObject()
        {
            Id = GenerateId();
        }

        public InstantiatedObject(GameObject gameObject)
        {
            GameObject = gameObject;
            Id = GenerateId();
        }

        public long GetId()
        {
            return Id;
        }

        public void SetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public GameObject GetGameObject()
        {
            return GameObject;
        }

        public InstantiatedObject Destroy()
        {
            if (Destroyed || GameObject == null) {
                return this;
            }

            GameObject.SetActive(false);
            Destroy(GameObject);
            Destroyed = true;
            return this;
        }

        private long GenerateId()
        {
            return System.DateTime.Now.Ticks;
        }
    }
}