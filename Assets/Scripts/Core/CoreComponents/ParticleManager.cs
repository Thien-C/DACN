using UnityEngine;

public class ParticleManager : CoreComponent
{
    private Transform particleContainer;

    protected override void Awake()
    {
        base.Awake();
        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }

    public GameObject StartParticle(GameObject particlePrefab, Vector2 possition, Quaternion rotation)
    {
        return Instantiate(particlePrefab, possition, rotation, particleContainer);
    }

    public GameObject StartParticle(GameObject particlePrefab)
    {
        return Instantiate(particlePrefab, transform.position, Quaternion.identity);
    }

    public GameObject StartParticleWithRandomRotation(GameObject particlePrefab)
    {
        var randomRotation = Quaternion.Euler(0f,0f,Random.Range(0f,360f));
        return StartParticle(particlePrefab,transform.position,randomRotation);
    }

    public void SpawnObjectsInMultipleDirections(GameObject spawnPrefab, int count, float radius)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = i * (180f / count); // Chia đều góc theo số lượng spawn
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            Vector2 spawnPosition = (Vector2)transform.position + direction * radius;

            Quaternion rotation = Quaternion.identity; // Giữ nguyên hướng hoặc điều chỉnh nếu cần
            Instantiate(spawnPrefab, spawnPosition, rotation);
        }
    }

}
