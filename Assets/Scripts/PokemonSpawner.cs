using UnityEngine;

/**
 * Spawns pokemons around this object's enter with the radius.
 */
public class PokemonSpawner : MonoBehaviour
{
    public Transform parent;
    public GameObject[] prefabs;
    public int amount = 30;
    public float spawnRadius;
    public float distanceRadius;

    public class InstanceDetails
    {
        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        
        public InstanceDetails(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            this.prefab = prefab;
            this.position = position;
            this.rotation = rotation;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

}
