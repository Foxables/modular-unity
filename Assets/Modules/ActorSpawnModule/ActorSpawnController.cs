using System;
using Core.EventBus;
using Modules.ActorSpawnModule.Events;
using Modules.ObjectManagementModule.Events.Payloads;
using UnityEngine;

namespace Modules.ActorSpawnModule {
    class ActorSpawnController: MonoBehaviour {
        private int SpawnPointID;

        [Header("Spawn Controls")]
        [Tooltip("How long to wait between spawning entities.")]
        public int SpawnInterval;
        [Tooltip("Number of entities to spawn per spawn interval.")]
        public int AmountPerInterval;
        [Tooltip("Maximum number of currently living entities that can be spawned by this spawn point.")]
        public int MaxActive;
        [Tooltip("The maximum number of entities that can ever be spawned by this spawn point.")]
        public int MaxLifetime;
        [Tooltip("How long to spawn entities for.")]
        public int SpawnLifetime;
        [Space(10)]
        public bool SpawnInfinitely;
        [Tooltip("Begin spawning entities when the spawn point has been initialized. Uncheck to wait for the first interval to begin spawning.")]
        public bool SpawnOnStart;
        [Tooltip("Spawn entities linearly throughout spawn intervals.")]
        public bool LinearSpawning;
        [Tooltip("Despawn all entities spawned by this spawn point when the spawn lifetime ends.")]
        public bool DespawnOnEnd;

        [Header("Spawnable Entities Configuration")]
        public string[] SpawnableActorPaths;
        [Range(0, 1)]
        public float[] SpawnableActorChance;

        private bool isActive = false;

        private PublisherInterface Publisher;
        private int CurrentFrame = 1;
        private int SpawnPointLifeTimer = 0;
        private int TotalEntitiesSpawned = 0;
        private int ActiveSpawnedEntities = 0;

        void Start() {
            SetSpawnPointId();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Vector3 pos = transform.position;
            pos.y += 0.6f;
            Gizmos.DrawWireSphere(pos, 0.5f);
        }

        void OnDrawGizmosSelected() {
            Gizmos.color = Color.magenta;
            Vector3 pos = transform.position;
            pos.y += 0.6f;
            Gizmos.DrawRay(pos, transform.forward);
        }

        public void FixedUpdate() {
            if (ShouldSpawn() == true) {
                Spawn();
            }
        }

        private float getFixedDeltaTime() {
            return Time.fixedDeltaTime;
        }

        private ActorSpawnController DespawnIfLifetimeEnd()
        {
            if (DespawnOnEnd) {
                // Destroy(gameObject, SpawnLifetime);
                // Send despawn event for all living actors.
            }

            return this;
        }

        private ActorSpawnController UpdateCounter() {
            float frame = getFixedDeltaTime() * CurrentFrame % 1;
            if (frame >= (1 - getFixedDeltaTime())) {
                // Reset frame counter.
                CurrentFrame = 0;
                SpawnPointLifeTimer++; // Increment Life Time Counter for Spawn Point.
                Debug.Log("Spawn Point Life Timer: " + SpawnPointLifeTimer);
            }

            CurrentFrame++;

            return this;
        }

        private bool IntervalReached() {
            float interval = SpawnInterval;
            if (SpawnPointLifeTimer == 0) {
                return false;
            }
            int hasReachedInterval = (int)(SpawnPointLifeTimer % interval);

            return hasReachedInterval == 0;
        }

        public void SetPublisher(PublisherInterface publisher) {
            Publisher = publisher;
            Debug.Log("Publisher Was Set");
        }

        public void Init() {
            isActive = true;
            if (SpawnOnStart) {
                Debug.Log("Spawn on Start!");
                Spawn();
                SpawnOnStart = false;
            }
            Debug.Log("Spawn Point Initialized");
        }

        private bool ShouldSpawn () {
            if (Publisher == null) {
                return false;
            }

            if (!isActive || SpawnableActorPaths.Length == 0) {
                return false;
            }

            if (!SpawnInfinitely && SpawnPointLifeTimer >= SpawnLifetime && SpawnLifetime != 0) {
                isActive = false;
                DespawnIfLifetimeEnd();
                return false;
            }

            if (!SpawnInfinitely && TotalEntitiesSpawned >= MaxLifetime && MaxLifetime != 0) {
                return false;
            }

            if (MaxActive > 0 && ActiveSpawnedEntities >= MaxActive) {
                return false;
            }

            if (UpdateCounter().IntervalReached()) {
                Debug.Log("Spawn interval reached.");
                return true;
            }

            if (LinearSpawning) {
                return true;
            }

            return false;
        }

        private int AmountToSpawn() {
            if (!isActive) {
                return 0;
            }

            if (LinearSpawning) {
                return GetLinearSpawnAmount();
            }

            return Math.Min(AmountPerInterval, MaxActive - ActiveSpawnedEntities);
        }

        private void Spawn() {
            int amount = AmountToSpawn();
            if (amount == 0) {
                Debug.Log("Nothing to spawn");
                return;
            }

            Debug.Log("Spawning " + amount + " entities");

            ActiveSpawnedEntities += amount;
            TotalEntitiesSpawned += amount;
            DispatchWeightedSpawnEvents(amount);
        }

        private void DispatchWeightedSpawnEvents(int amount) {
            // Distribute the amount of entities by the number of entities that can be spawned.
            for (int i = 0; i < amount; i++) {
                bool hasSpawned = false;
                while (!hasSpawned) {
                    var r = UnityEngine.Random.Range(0, SpawnableActorPaths.Length);
                    var path = SpawnableActorPaths[r];
                    if (SpawnableActorChance.Length > r) {
                        if (SpawnableActorChance[r] != 0) {
                            // Dispatch spawn event.
                            int chance = (int)(SpawnableActorChance[r] * 100);
                            var r1 = UnityEngine.Random.Range(0, 100);
                            if (r1 > chance) {
                                continue;
                            }
                        }
                    }

                    var payload = new InstantiateObjectEventPayload(path, transform.position, transform.rotation);
                    SpawnActorEvent e = ScriptableObject.CreateInstance<SpawnActorEvent>();
                    e.SetPayload(payload);
                    Debug.Log("Dispatched spawn event for " + path);
                    Publisher.Dispatch(e);
                    hasSpawned = true;
                }
            }
        }

        private int GetLinearSpawnAmount() {
            float interval = SpawnInterval;
            float amount = AmountPerInterval;
            return (int)(getFixedDeltaTime() / interval * amount);
        }

        public void RemoveSpawnedEntity() {
            ActiveSpawnedEntities--;
        }

        private void SetSpawnPointId() {
            SpawnPointID = gameObject.GetInstanceID();
            Debug.Log("Spawn Point Registered as " + SpawnPointID);
        }

        public int GetSpawnPointId() {
            return SpawnPointID;
        }
    }
}