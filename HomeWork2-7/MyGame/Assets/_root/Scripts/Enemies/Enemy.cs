using MineItem;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IMineExplosion/*, BulletDamage, TrapDamage*/
    {
        [SerializeField] private float _hp = 100f;
        [SerializeField] private Transform[] _wayPoints;

        private Rigidbody _rb;
        private NavMeshAgent _agent;
        private int m_CurrentWaypointIndex;

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
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % _wayPoints.Length;
                _agent.SetDestination(_wayPoints[m_CurrentWaypointIndex].position);
            }
        }


        public void MineHit(/*float forse,*/ float damage)
        {
            _hp = _hp - damage;
            Debug.Log($"{gameObject.name} HP: {_hp}");

            //rb.AddForce(forse, forse, forse, ForceMode.Impulse);
            if (_hp <= 0)
                Destroy(gameObject);
        }

        //public void Hit(float damage)
        //{
        //    _hp = _hp - damage;
        //    if (_hp <= 0)
        //        gameObject.SetActive(false);
        //}

        //public void TrapHit(float damage)
        //{
        //    _hp = _hp - damage;
        //    if (_hp <= 0)
        //        gameObject.SetActive(false);
        //}
    }
}