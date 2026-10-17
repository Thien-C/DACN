using UnityEngine;

namespace Hung.CoreSystem
{
    public class SceneSpawnPoint : MonoBehaviour
    {
        [SerializeField] private int spawnIndex;
        public int SpawnIndex => spawnIndex;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            Gizmos.DrawLine(transform.position, transform.position + transform.right);
        }
    }
}
