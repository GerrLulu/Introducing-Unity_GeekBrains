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
        [SerializeField] private float _huntingDistance = 7f;
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

        private void FixedUpdate()
        {
            RaycastHit hit;

            Vector3 direction = _protagonist.transform.position - _eyePosition.position;
            direction = new Vector3(direction.x, direction.y + 1.7f, direction.z);
            _rayToPlayer = new Ray(_eyePosition.position, direction);
            Physics.Raycast(_rayToPlayer, out hit);

            

            if (hit.collider != null)
            {
                if (hit.distance <= _huntingDistance)
                {
                    _agent.SetDestination(_protagonist.transform.position);
                    Debug.DrawRay(_eyePosition.position, direction, Color.red);
                }
                else
                {
                    Patrol();
                    Debug.DrawRay(_eyePosition.position, direction, Color.green);
                }
            }
        }


        private void Patrol()
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

        public void MineHit(float damage, float force, Vector3 positionMine)
        {
            _hp = _hp - damage;
            Debug.Log($"{gameObject.name} HP: {_hp}");

            var positionImpulse = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            Vector3 direction = positionImpulse - positionMine;
            _rb.AddForce(direction.normalized * force, ForceMode.Impulse);

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