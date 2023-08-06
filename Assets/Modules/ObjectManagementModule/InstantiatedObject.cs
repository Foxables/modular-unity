using UnityEngine;
namespace Modules.ObjectManagementModule {
    public class InstantiatedObject : ScriptableObject
    {
        private GameObject GameObject { get; set; }
        private long Id { get; }
        private bool Destroyed = false;

        public InstantiatedObject(GameObject gameObject)
        {
            this.GameObject = gameObject;
            this.Id = this.GenerateId();
        }

        public long GetId()
        {
            return this.Id;
        }

        public GameObject GetGameObject()
        {
            return this.GameObject;
        }

        public InstantiatedObject Destroy()
        {
            if (this.Destroyed || this.GameObject == null) {
                return this;
            }

            this.GameObject.SetActive(false);
            Destroy(this.GameObject);
            return this;
        }

        private long GenerateId()
        {
            return System.DateTime.Now.Ticks;
        }
    }
}