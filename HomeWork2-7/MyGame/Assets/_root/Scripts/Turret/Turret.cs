using Player;
using UnityEngine;

namespace Turret
{
    public class Turret : MonoBehaviour//, BulletDamage
    {
        [SerializeField] private Transform _neckTurret;
        [SerializeField] private Protagonist _player;
        [SerializeField] private float _rotationIdleSpeed;
        [SerializeField] private float _rotationAtackSpeed;
        //[SerializeField] private float _radiusShoot = 13f;
        [SerializeField] private float _hp = 100f;

        //[SerializeField] private float _timeReload = 2f;
        //[SerializeField] GameObject _bulletPrefub;
        //[SerializeField] Transform _spawnBullet;
        //[SerializeField] private int _countBullet = 5;

        private Transform _target;
        private bool _isIdle;
        //private bool _isShootBullet = true;


        private void Start()
        {
            _target = _player.transform;
            _isIdle = true;

            //StartCoroutine((IEnumerator)Reload(_timeReload));
        }

        private void Update()
        {
            if (_isIdle)
            {
                Patrol();
            }
            else
            {
                Atack();
            }

            //if (_isShootBullet)
            //    SpawnBullet();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                _isIdle = false;
                Debug.Log(_isIdle);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                _isIdle = true;
                Debug.Log(_isIdle);
            }
        }


        private void Patrol()
        {
            _neckTurret.Rotate(Vector3.up, _rotationIdleSpeed * Time.deltaTime);
        }

        private void Atack()
        {
            Vector3 upPoint = new Vector3(_target.position.x, _neckTurret.position.y, _target.position.z);

            Vector3 stepDir = Vector3.RotateTowards(_neckTurret.forward, upPoint - _neckTurret.position,
                _rotationAtackSpeed * Time.deltaTime, 0f);

            _neckTurret.rotation = Quaternion.LookRotation(stepDir);
        }

        public void Hit(float damage)
        {
            _hp = _hp - damage;
            if (_hp <= 0)
                Destroy(gameObject);
        }

        //private IEnumerable Reload(float _timeReload)
        //{
        //    yield return new WaitForSeconds(1f);
        //    while (_countBullet > 0)
        //    {
        //        _countBullet--;
        //        _isShootBullet = true;

        //        yield return new WaitForSeconds(_timeReload);
        //    }
        //}

        //private void SpawnBullet()
        //{
        //    GameObject bullet = GameObject.Instantiate(_bulletPrefub, _spawnBullet.position, _spawnBullet.rotation);
        //    _isShootBullet = false;
        //}
    }
}