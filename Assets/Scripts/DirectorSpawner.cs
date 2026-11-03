using UnityEngine;


    public class DirectorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject healthPackPrefab;
    [SerializeField] private float spawnCheckInterval = 5f;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnCheckInterval)
        {
            timer = 0f;
            TryDirectorSpawn();
        }
    }

    private void TryDirectorSpawn()
    {
        if (AIDirector.Instance == null) return;

        // Check if director allows spawning based on current stress cycles
        if (AIDirector.Instance.CanSpawnHorde())
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            AIDirector.Instance.RegisterEnemy(enemy);
        }
        else if (AIDirector.Instance.ShouldDropSupplies() && Random.value < 0.3f)
        {
            // Drop safety loot if player is highly stressed or in a Fade phase
            Instantiate(healthPackPrefab, transform.position, Quaternion.identity);
        }
    }
}

