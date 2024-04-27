using Bullet;
using Player;
using System.Collections;
using UnityEngine;

namespace Turret
{
    public class Turret : MonoBehaviour, IBulletDamage
    {
        [SerializeField] private float _hp = 100f;
        [SerializeField] private float _rotationIdleSpeed;
        [SerializeField] private float _rotationAtackSpeed;
        [SerializeField] private float _timeReload = 5f;
        [SerializeField] private float _timeBetweenShots = 0.5f;
        [SerializeField] private int _countBullet = 5;
        [SerializeField] private Transform _neckTurret;
        [SerializeField] private Transform _spawnBullets;
        [SerializeField] private Protagonist _player;
        [SerializeField] GameObject _bulletPrefub;

        private bool _isIdle;
        private bool _isShoot = true;
        private Transform _target;


        private void Start()
        {
            _target = _player.transform;
            _isIdle = true;

            //StartCoroutine(Shooting(_timeBetweenShots, _timeReload));

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
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                _isIdle = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                _isIdle = true;
            }
        }


        private void Patrol()
        {
            _neckTurret.Rotate(Vector3.up, _rotationIdleSpeed * Time.deltaTime);

            //StopCoroutine((IEnumerator)Shooting(_timeBetweenShots, _timeReload));
        }

        private void Atack()
        {
            Vector3 upPoint = new Vector3(_target.position.x, _neckTurret.position.y, _target.position.z);

            Vector3 stepDir = Vector3.RotateTowards(_neckTurret.forward, upPoint - _neckTurret.position,
                _rotationAtackSpeed * Time.deltaTime, 0f);

            _neckTurret.rotation = Quaternion.LookRotation(stepDir);

            //StartCoroutine(Shooting(_timeBetweenShots, _timeReload));

            //Shoot();
        }

        public void Hit(float damage)
        {
            _hp = _hp - damage;
            Debug.Log($"{gameObject.name} HP: {_hp}");
            //if (_hp <= 0)
            //    Destroy(gameObject);
        }

        private IEnumerator Shooting(float timeBetweenShots, float timeReload)
        {
            Debug.Log("Start");
            int countBullet = _countBullet;
            if(_isIdle == false)
            {
                while (countBullet > 0)
                {
                    Instantiate(_bulletPrefub, _spawnBullets.position, _spawnBullets.rotation);
                    _countBullet--;
                    new WaitForSeconds(timeBetweenShots);

                    //if (countBullet == 0)
                    //{
                    //    new WaitForSeconds(timeReload);
                    //    countBullet = _countBullet;
                    //}
                }
            }

            Debug.Log("End");
            yield return new WaitForSeconds (timeReload);
        }

        //private void Shoot()
        //{
        //    Instantiate(_bulletPrefub, _spawnBullets.position, _spawnBullets.rotation);
        //}

        //private IEnumerable Reload(float _timeReload)
        //{
        //    yield return new WaitForSeconds(1f);
        //    while (_countBullet > 0)
        //    {
        //        _countBullet--;
        //        //_isShoot = true;

        //        yield return new WaitForSeconds(_timeReload);
        //    }
        //}
    }
}