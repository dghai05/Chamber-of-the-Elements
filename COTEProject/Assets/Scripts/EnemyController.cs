using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float moveSpeed = 3f;

    [Header("Combat Settings")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 4f;

    [Header("References")]
    [SerializeField] private Animator animator;

    private float lastAttackTime;
    private bool isAttacking = false;
    private bool isChasing;

    private Transform player;
    private Enemy enemy;

    private Rigidbody rb; // Optional for grounded movement

    void Start()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();

        if (Player.User != null)
            player = Player.User.transform;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null || enemy == null || !enemy.IsAlive()) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (isAttacking) return;

        if (distance <= attackRange)
        {
            AttemptAttack();
        }
        else if (distance <= chaseRange)
        {
            isChasing = true;
        }
        else if (distance > chaseRange + 2f)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else if (animator != null)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10f * Time.deltaTime);
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (animator != null)
            animator.SetBool("isWalking", true);
    }

    void AttemptAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            isAttacking = true;

            if (animator != null)
            {
                animator.SetBool("isWalking", false); // stop walk animation
                animator.ResetTrigger("AttackTrigger");
                animator.SetTrigger("AttackTrigger");
            }

            Invoke(nameof(EndAttack), 1f); // match your attack animation length
        }
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    // Called via animation event
    public void DealDamage()
    {
        if (player == null || enemy == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            Player.User.TakeDamage(enemy.AttackPower);
            Debug.Log($"{enemy.name} dealt {enemy.AttackPower} damage to player!");
        }
        else
        {
            Debug.Log($"{enemy.name} missed — player out of range.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
