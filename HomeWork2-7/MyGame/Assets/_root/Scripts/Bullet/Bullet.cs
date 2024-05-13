using UnityEngine;

namespace Bullet
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _lifeTime = 10f;

        //[SerializeField] private Light lightFlash;
        //[SerializeField] private float lightTime = 0.02f;


        void FixedUpdate()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
            Destroy(gameObject, _lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            IBulletDamage obj;

            if (other.TryGetComponent<IBulletDamage>(out obj))
            {
                if (obj != null)
                {
                    obj.Hit(_damage);
                    Destroy(gameObject);
                }
            }

            //StartCoroutine(LightFlash());
        }

        //private IEnumerator LightFlash()
        //{
        //    lightFlash.GetComponent<Light>().enabled = true;

        //    yield return new WaitForSeconds(lightTime);

        //    lightFlash.GetComponent<Light>().enabled = false;
        //}
    }
}