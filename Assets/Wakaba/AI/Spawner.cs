using UnityEngine;
namespace Wakaba.AI
{
    public class Spawner : MonoBehaviour
    {
        public Vector3 size = Vector3.one;
        public Vector3 centre = Vector3.zero;

        [SerializeField, Tooltip("Use the object's Y position always when spawning an object.")]
                         private bool floorYPosition = false;
        [SerializeField] private Vector2 spawnRate = new Vector2(0, 1);

        [SerializeField] private bool shouldSpawnBoss = false;
        [SerializeField, Range(0, 100)] private float bossSpawnChance = 1;

        [SerializeField] private GameObject bossPrefab = null;
        [SerializeField] private GameObject enemyPrefab = null;

        private float time = 0;
        private float timeStep = 0;

        public void Spawn()
        {
            GameObject prefab = shouldSpawnBoss && Random.Range(0, 100) < bossSpawnChance ? bossPrefab : enemyPrefab;
            Vector3 position = transform.position + new Vector3(Random.Range(-size.x * 0.5f, size.x * 0.5f),
                                                                floorYPosition ? 0 : Random.Range(-size.y * 0.5f, size.y * 0.5f),
                                                                Random.Range(-size.z * 0.5f, size.z * 0.5f)) + centre;

            position = transform.InverseTransformPoint(position);

            Instantiate(prefab, position, transform.rotation, transform);
            
            timeStep = Random.Range(spawnRate.x, spawnRate.y);
            time = 0;
        }

        private void Start() => timeStep = Random.Range(spawnRate.x, spawnRate.y);

        private void Update()
        {
            if (time < timeStep) time += Time.deltaTime * Time.timeScale;
            else Spawn();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Store the default matrix.
            Matrix4x4 baseMatrix = Gizmos.matrix;

            // Make the gizmos use the object(s)'s matrix.
            Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
            Gizmos.matrix = rotationMatrix;

            // Draw a green, partially transparent cube.
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(centre, size);

            // Reset the gizmo's matrix back to default.
            Gizmos.matrix = baseMatrix;
        }
#endif
    }
}