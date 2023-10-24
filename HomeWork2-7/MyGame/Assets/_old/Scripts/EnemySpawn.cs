using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;
    private int countEnemy = 0;
    [SerializeField] int maxCountEnemy = 3;

    //private List<GameObject> enemyPool;

    private void Start()
    {
        enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (enemy == null && countEnemy < maxCountEnemy)
        {
            enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            countEnemy += 1;
        }
    }
}
