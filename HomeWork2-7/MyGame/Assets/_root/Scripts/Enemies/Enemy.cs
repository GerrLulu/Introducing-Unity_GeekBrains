using Bullet;
using MineItem;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IMineExplosion, IBulletDamage/*, TrapDamage*/
    {
        [SerializeField] private float _hp = 100f;
        [SerializeField] private float _distanceHaunt = 7f;
        [SerializeField] private Transform[] _wayPoints;
        [SerializeField] private Transform _eyePosition;
        [SerializeField] private Protagonist _protagonist;
        [SerializeField] private LayerMask _layerMask;

        private int m_CurrentWaypointIndex;
        private Rigidbody _rb;
        private NavMeshAgent _agent;
        private Ray _rayToPlayer;

        //Animator animator;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _agent = GetComponent<NavMeshAgent>();

            //animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _agent.SetDestination(_wayPoints[0].position);
        }

        private void Update()
        {
            var direction = _protagonist.transform.position - _eyePosition.position;
            direction = new Vector3(direction.x, direction.y + 1.7f, direction.z);
            _rayToPlayer = new Ray(_eyePosition.position, direction);
            Debug.DrawRay(_eyePosition.position, direction, Color.red);

        }

        private void FixedUpdate()
        {
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % _wayPoints.Length;
                _agent.SetDestination(_wayPoints[m_CurrentWaypointIndex].position);
            }
        }


        public void Hit(float damage)
        {
            _hp = _hp - damage;
            Debug.Log($"{gameObject.name} HP: {_hp}");
            //if (_hp <= 0)
            //    gameObject.SetActive(false);
        }

        public void MineHit(/*float forse,*/ float damage)
        {
            _hp = _hp - damage;
            Debug.Log($"{gameObject.name} HP: {_hp}");

            //rb.AddForce(forse, forse, forse, ForceMode.Impulse);
            //if (_hp <= 0)
            //    Destroy(gameObject);
        }

        //public void TrapHit(float damage)
        //{
        //    _hp = _hp - damage;
        //    if (_hp <= 0)
        //        gameObject.SetActive(false);
        //}
    }
}