using System.Collections;
using UnityEngine;

namespace MineItem
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private float _damage = 30f;
        [SerializeField] private float _lifeTime = 5.0f;

        //[SerializeField] private float _forse = 1000f;
        //[SerializeField] private float _radiusExplosion = 8f;

        //private Transform target;
        //[SerializeField] private AudioSource audioExplosion;

        //[SerializeField] GameObject explosionPartical;


        private void Start ()
        {
            StartCoroutine("TimeToDie");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" || other.tag == "Enemy")
            {
                other.GetComponent<IMineExplosion>().MineHit(/*_forse,*/ _damage);
                //Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion);
                //foreach (Collider hit in colliders)
                //{
                //    Rigidbody rb = hit.GetComponent<Rigidbody>();
                //    if (rb != null)
                //    {
                //        rb.AddForce(_forse, _forse, _forse, ForceMode.Impulse);
                //        hit.GetComponent<IMineExplosion>().MineHit(_forse, _damage);
                //    }
                //}
                //audioExplosion.Play();
                //Instantiate(explosionPartical, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        private IEnumerator TimeToDie()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }
    }
}