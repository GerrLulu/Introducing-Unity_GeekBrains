using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour//, BulletDamage
{
    /*private Transform target;

    [SerializeField] private Transform neck;
    [SerializeField] private float radiusShoot = 13f;
    [SerializeField] private float speedRotation;
    [SerializeField] private float hp = 100f;

    [SerializeField] private float timeReload = 2f;
    private bool isShootBullet = true;
    [SerializeField] GameObject bulletPrefub;
    [SerializeField] Transform spawnBullet;
    [SerializeField] private int countBullet = 5;

    public void Hit(float damage)
    {
        hp = hp - damage;
        if (hp <= 0)
            Destroy(gameObject);
    }

    private void Start()
    {
        target = FindObjectOfType<Protagonist>().transform;

        StartCoroutine((IEnumerator)Reload(timeReload));
    }

    private void Update()
    {
        if (Vector3.Distance(target.position, transform.position) > radiusShoot)
            return;
        Vector3 upPoint = new Vector3(target.position.x, neck.position.y, target.position.z);
        Vector3 stepDir = Vector3.RotateTowards(neck.forward, upPoint - neck.position, speedRotation * Time.deltaTime, 0f);
        neck.rotation = Quaternion.LookRotation(stepDir);
        
        if (isShootBullet)
            SpawnBullet();
    }

    private IEnumerable Reload(float _timeReload)
    {
        yield return new WaitForSeconds(1f);
        while (countBullet > 0)
        {
            countBullet--;
            isShootBullet = true;

            yield return new WaitForSeconds(_timeReload);
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefub, spawnBullet.position, spawnBullet.rotation);
        isShootBullet = false;
    }*/
}
