using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 10f;

    [SerializeField] private Light lightFlash;
    [SerializeField] private float lightTime = 0.02f;

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<BulletDamage>();
        if (obj != null)
            obj.Hit(damage);
        StartCoroutine(LightFlash());
        Destroy(gameObject, lightTime);
    }

    private IEnumerator LightFlash()
    {
        lightFlash.GetComponent<Light>().enabled = true;

        yield return new WaitForSeconds(lightTime);

        lightFlash.GetComponent<Light>().enabled = false;
    }
}
