using UnityEngine;

public class MineSpawn : MonoBehaviour
{
    [SerializeField] GameObject minePrefub;


    private void Update()
    {
        if (Input.GetButtonDown("Put mine"))
            SpawnMine();
    }

    private void SpawnMine()
    {
        Instantiate(minePrefub, transform.position, Quaternion.identity);
    }
}