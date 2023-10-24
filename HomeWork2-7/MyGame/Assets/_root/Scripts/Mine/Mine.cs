using UnityEngine;

public class Mine : MonoBehaviour
{
    //private Transform target;

    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private float _damage = 30f;
    [SerializeField] private float _forse = 1000f;
    [SerializeField] private float _radiusExplosion = 8f;

    //[SerializeField] private AudioSource audioExplosion;

    //[SerializeField] GameObject explosionPartical;


    private void Update()
    {
        Destroy(gameObject, _lifeTime);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            other.GetComponent<IMineExplosion>().MineHit(_forse, _damage);
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(_forse, _forse, _forse, ForceMode.Impulse);
                    hit.GetComponent<IMineExplosion>().MineHit(_forse, _damage);
                }
            }
            //audioExplosion.Play();
            //Instantiate(explosionPartical, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}