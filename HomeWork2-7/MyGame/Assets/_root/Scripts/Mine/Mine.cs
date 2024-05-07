using System.Collections;
using UnityEngine;

namespace MineItem
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private float _damage = 30f;
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] private float _radiusExplosion = 8f;
        [SerializeField] private float _force = 1000f;

        //private Transform target;
        //[SerializeField] private AudioSource audioExplosion;
        //[SerializeField] GameObject explosionPartical;


        private void Start ()
        {
            StartCoroutine("TimeToDie");
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" || other.tag == "Enemy")
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion);
                foreach (Collider hit in colliders)
                {
                    IMineExplosion obj;

                    if (hit.TryGetComponent<IMineExplosion>(out obj))
                    {
                        if (obj != null)
                        {
                            obj.MineHit(_damage, _force, transform.position);
                        }
                    }
                }

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