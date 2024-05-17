using Player;
using System.Collections;
using UnityEngine;

namespace Turret
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private int _hp = 100;
        [SerializeField] private float _rotationIdleSpeed;
        [SerializeField] private float _rotationAtackSpeed;
        [SerializeField] private float _timeReload = 5f;
        [SerializeField] private float _timeBetweenShots = 0.5f;
        [SerializeField] private int _countBullets = 5;
        [SerializeField] private Transform _neckTurret;
        [SerializeField] private Transform _spawnBullets;
        [SerializeField] private Protagonist _player;
        [SerializeField] private GameObject _bulletPrefab;

        private bool _isIdle;
        private bool _isShoot;
        private int _currentBullets;
        private float _distanceFire;
        private Transform _target;
        private Ray _directionFire;
        private RaycastHit _hit;
        private AudioSource _audioShoot;

        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }


        private void Awake()
        {
            _audioShoot = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _isIdle = true;
            _target = _player.transform;
            _currentBullets = _countBullets;
            _distanceFire = GetComponent<SphereCollider>().radius - 1.8f;

            StartCoroutine(Shooting());
        }

        private void Update()
        {
            _directionFire = new Ray(_spawnBullets.position, _spawnBullets.forward);

            var rayCast = Physics.Raycast(_directionFire, out _hit, _distanceFire);

            if (rayCast)
            {
                if(_hit.collider.tag == "Player" && _isShoot)
                    Fire();
            }
        }

        private void FixedUpdate()
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
                _isIdle = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
                _isIdle = true;
        }


        private void Patrol() => _neckTurret.Rotate(Vector3.up, _rotationIdleSpeed * Time.deltaTime);

        private void Atack()
        {
            Vector3 upPoint = new Vector3(_target.position.x, _neckTurret.position.y, _target.position.z);

            Vector3 stepDir = Vector3.RotateTowards(_neckTurret.forward, upPoint - _neckTurret.position,
                _rotationAtackSpeed * Time.deltaTime, 0f);

            _neckTurret.rotation = Quaternion.LookRotation(stepDir);
        }

        private void Fire()
        {
            Instantiate(_bulletPrefab, _spawnBullets.position, _spawnBullets.rotation);

            _audioShoot.Play();
            
            _isShoot = false;
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                if (_currentBullets == 0)
                {
                    yield return new WaitForSeconds(_timeReload);
                    _currentBullets = _countBullets;
                }
                else
                {
                    _currentBullets--;
                    _isShoot = true;
                    yield return new WaitForSeconds(_timeBetweenShots);
                }
            }
        }
    }
}