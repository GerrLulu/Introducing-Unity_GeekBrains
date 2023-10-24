using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, BulletDamage, TrapDamage, IMineExplosion
{
    int m_CurrentWaypointIndex;

    [SerializeField] private float hp = 100f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] wayPoints;

    private Rigidbody rb;

    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        agent.SetDestination(wayPoints[0].position);

    }

    private void Update()
    {
        if(agent.remainingDistance < agent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % wayPoints.Length;
            agent.SetDestination(wayPoints[m_CurrentWaypointIndex].position);
        }
    }

    public void Hit(float damage)
    {
        hp = hp - damage;
        if (hp <= 0)
            gameObject.SetActive(false);
    }

    public void TrapHit(float damage)
    {
        hp = hp - damage;
        if (hp <= 0)
            gameObject.SetActive(false);
    }

    public void MineHit(float forse, float damage)
    {
        hp = hp - damage;
        rb.AddForce(forse, forse, forse, ForceMode.Impulse);
        if (hp <= 0)
            Destroy(gameObject);
    }
}
