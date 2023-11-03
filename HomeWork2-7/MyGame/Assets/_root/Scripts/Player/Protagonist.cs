using MineItem;
using UnityEngine;

namespace Player
{
    public class Protagonist : MonoBehaviour, IMineExplosion/*, BulletDamage, TrapDamage, ForseHeal*/
    {
        [SerializeField] private float _hp = 100f;

        [SerializeField] private float _speed = 0.1f;
        [SerializeField] private float _boost = 1.5f;
        [SerializeField] private float _sensHorizontal = 7f;
        private Vector3 _direction;
        private bool _isBoost;
        private float _velocity = 1.0f;
        private Rigidbody _rb;

        [SerializeField] private GameObject _minePrefab;
        [SerializeField] private Transform _spawnPointMine;

        private bool _isHaveBlueCard;

        //[SerializeField] private float _acceleration = 0.1f;
        //[SerializeField] private float _deceleration = 0.5f;
        //private int _velocityHash;

        //[SerializeField] private float _forseJump = 300f;


        //private bool _isGround;

        //Animator _animator;

        //[SerializeField] private Transform _spawnBullet;
        //[SerializeField] private GameObject _bulletPrefub;
        //private GameObject _bullet;

        //[SerializeField] private Slider _sliderHP;
        //[SerializeField] private GameObject _panelHP;
        //[SerializeField] private GameObject _panelMenuPause;
        //private bool _paused;

        //public AudioMixerGroup Mixer;

        //[SerializeField] private AudioSource _audioShoot;

        /*private void OnGUI()
        {
            GUI.BeginGroup(new Rect(10, 10, 400, 100));
            GUI.Box(new Rect(10, 10, 400, 100), "Player Life");
            GUI.TextField(new Rect(10, 20, 40, 30) ,"" + hp);
            GUI.HorizontalSlider(new Rect(15, 70, 380, 40), hp, 0.0f ,100.0f);
            GUI.EndClip();
        }*/

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            //_isGround = true;

            //_animator = GetComponent<Animator>();
            //_velocityHash = Animator.StringToHash("Velocity");

            //_paused = false;

            //_audioShoot = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _isHaveBlueCard = false;
        }

        private void Update()
        {
            _direction.z = Input.GetAxis("Vertical");
            _direction.x = Input.GetAxis("Horizontal");
            _isBoost = Input.GetButton("Boost");

            transform.Rotate(0, Input.GetAxis("Mouse X") * _sensHorizontal, 0);

            if (Input.GetButtonDown("Put mine"))
                SpawnMine();

            //if (_isBoost && _velocity <= 1.0f)
            //    _velocity += Time.deltaTime * _acceleration;
            //else if (!_isBoost && _velocity > 0.0f)
            //    _velocity -= Time.deltaTime * _deceleration;
            //else if (!_isBoost && _velocity < 0.0f)
            //    _velocity = 0.0f;

            //_animator.SetFloat(_velocityHash, _velocity);


            //if (_direction == Vector3.zero)
            //{
            //    _animator.SetBool("IsMove", false);

            //    if (Input.GetButtonDown("Fire1"))
            //        _animator.SetTrigger("Shoot");
            //}
            //else
            //    _animator.SetBool("IsMove", true);


            //if (Input.GetButtonDown("Jump") && _isGround)
            //{
            //    _rb.AddForce(new Vector3(0, _forseJump, 0), ForceMode.Impulse);
            //    _animator.SetBool("IsJump", true);
            //    _isGround = false;
            //}

            //_sliderHP.value = _hp;

            //if (Input.GetButtonDown("Pause"))
            //{
            //    if (!_paused)
            //    {
            //        PausedGame();
            //        _paused = true;
            //    }
            //}
        }

        private void FixedUpdate()
        {
            Move();
        }


        private void Move()
        {
            float s;

            if (_isBoost)
                s = _boost * _speed;
            else
                s = _speed;

            transform.Translate(_direction.normalized * s * _velocity);
        }

        private void SpawnMine()
        {
            Instantiate(_minePrefab, _spawnPointMine.position, _spawnPointMine.rotation);
        }

        public void MineHit(/*float forse, */float damage)
        {
            _hp = _hp - damage;
            //_rb.AddForce(forse, forse, forse, ForceMode.Impulse);
            if (_hp <= 0)
            {
                Destroy(gameObject);
                //_animator.SetTrigger("Die");
            }
        }

        //public void Hit(float damage)
        //{
        //    _hp = _hp - damage;
        //    if (_hp <= 0)
        //        _animator.SetTrigger("Die");
        //}

        //public void TrapHit(float damage)
        //{
        //    _hp = _hp - damage;
        //    if (_hp <= 0)
        //        _animator.SetTrigger("Die");
        //}

        //public void hpUp(float _hp)
        //{
        //    this._hp += _hp;
        //    if (this._hp > 100)
        //        this._hp = 100;
        //}

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if(collision.collider.tag == "Ground")
        //    {
        //        _animator.SetBool("IsJump", false);
        //        _isGround = true;
        //    }
        //}


        //private void SpawnBullet()
        //{
        //    _audioShoot.Play();

        //    _bullet = Instantiate(_bulletPrefub, _spawnBullet.position, _spawnBullet.rotation);
        //}

        //public void RestartGame()
        //{
        //    SceneManager.LoadScene(1);
        //}

        //public void Quit()
        //{
        //    Application.Quit();
        //}

        //public void PausedGame()
        //{
        //    Time.timeScale = 0;
        //    _panelHP.SetActive(false);
        //    _panelMenuPause.SetActive(true);
        //}

        //public void BackGame()
        //{
        //    Time.timeScale = 1;
        //    _paused = false;
        //    _panelHP.SetActive(true);
        //    _panelMenuPause.SetActive(false);
        //}

        //public void ToggleMusic(bool enabled)
        //{
        //    if(enabled)
        //        Mixer.audioMixer.SetFloat("MusicVolume", -80);
        //    else
        //        Mixer.audioMixer.SetFloat("MusicVolume", 0);
        //}

        //public void ChangeVolume(float volume)
        //{
        //    Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
        //}
    }
}