using Bullet;
using Doors;
using MineItem;
using UnityEngine;

namespace Player
{
    public class Protagonist : MonoBehaviour, IMineExplosion, IBulletDamage/*, TrapDamage, ForseHeal*/
    {
        [SerializeField] private int _hp = 100;
        [SerializeField] private float _speed = 0.1f;
        [SerializeField] private float _boost = 1.5f;
        [SerializeField] private float _sensHorizontal = 7f;
        [SerializeField] private float _forceJump = 300f;
        [SerializeField] private float _accelerationAnim = 0.1f;
        [SerializeField] private float _decelerationAnim = 0.5f;
        [SerializeField] private GameObject _bulletPrefub;
        [SerializeField] private GameObject _minePrefab;
        [SerializeField] private Transform _spawnBullet;
        [SerializeField] private Transform _spawnPointMine;

        private int _velocityHash;
        private float _velocity = 0.0f;
        private bool _isBoost;
        private bool _isGround;
        private bool _isHaveBlueCard;
        private Vector3 _direction;
        private Rigidbody _rb;
        private Animator _animator;

        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        public bool IsHaveBlueCard { get { return _isHaveBlueCard; } }

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
            _animator = GetComponent<Animator>();
            //_audioShoot = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _isGround = true;
            _isHaveBlueCard = false;
            //_paused = false;

            _velocityHash = Animator.StringToHash("Velocity");

            BlueCard.GiveBlueCard += GetBlueCard;
        }

        private void Update()
        {
            _direction.z = Input.GetAxis("Vertical");
            _direction.x = Input.GetAxis("Horizontal");
            _isBoost = Input.GetButton("Boost");

            transform.Rotate(0, Input.GetAxis("Mouse X") * _sensHorizontal, 0);

            if (Input.GetButtonDown("Jump") && _isGround)
                Jump();

            if (Input.GetButtonDown("Put mine"))
                SpawnMine();

            if (Input.GetButtonDown("Fire1"))
                Shoot();

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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Ground")
            {
                _animator.SetBool("IsJump", false);
                _isGround = true;
            }
        }


        private void Move()
        {
            float s;

            if (_direction == Vector3.zero)
                _animator.SetBool("IsMove", false);
            else
                _animator.SetBool("IsMove", true);

            if (_isBoost)
                s = _boost * _speed;
            else
                s = _speed;

            if (_isBoost && _velocity <= 1.0f)
                _velocity += Time.deltaTime * _accelerationAnim;
            else if (!_isBoost && _velocity > 0.0f)
                _velocity -= Time.deltaTime * _decelerationAnim;
            else if (!_isBoost && _velocity < 0.0f)
                _velocity = 0.0f;

            _animator.SetFloat(_velocityHash, _velocity);

            transform.Translate(_direction.normalized * s);
        }

        private void Jump()
        {
            _rb.AddForce(new Vector3(0, _forceJump, 0), ForceMode.Impulse);
            _animator.SetBool("IsJump", true);
            _isGround = false;
        }

        private void Shoot()
        {
            Instantiate(_bulletPrefub, _spawnBullet.position, _spawnBullet.rotation);
            _animator.SetTrigger("Shoot");
        }

        private void SpawnMine()
        {
            Instantiate(_minePrefab, _spawnPointMine.position, _spawnPointMine.rotation);
        }

        public void Hit(int damage)
        {
            _hp = _hp - damage;
            Debug.Log($"{gameObject.name} HP: {_hp}");
            DieProtagonist(_hp);
        }

        public void MineHit(int damage, float force, Vector3 positionMine)
        {
            _hp = _hp - damage;
            Debug.Log($"{gameObject.name} HP: {_hp}");
            
            var positionImpulse = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            Vector3 direction = positionImpulse - positionMine;
            _rb.AddForce(direction.normalized * force, ForceMode.Impulse);

            DieProtagonist(_hp);
        }

        private void GetBlueCard()
        {
            _isHaveBlueCard = true;

            Debug.Log($"Get blue card: {_isHaveBlueCard}");
        }

        private void DieProtagonist(int hp)
        {
            if(hp <= 0)
            {
                _animator.SetTrigger("Die");
                //Destroy(gameObject);
            }
        }

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


        private void OnDestroy()
        {
            BlueCard.GiveBlueCard -= GetBlueCard;
        }
    }
}