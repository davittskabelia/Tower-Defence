using Assets.Scripts.Skeleton;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour, IFreezable, IDamagable
{
    public float Cooldown;
    public float Power;
    public int Score;
    public float Health;

    private NavMeshAgent agent;
    private Animator animator;


    private bool attack = false;

    private float currentCooldown = float.MaxValue;

    private float currentHealth;

    private float freezeCooldown = float.MinValue;
    private bool isFrozen;


    public void Start()
    {
        animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);

        currentHealth = float.MaxValue;
    }

    public void OnResurrected()
    {
        var destination = GameLogic._instance.AttackLocations[Random.Range(0, GameLogic._instance.AttackLocations.Count)]
            .transform.position;

        agent.destination = destination;

        currentHealth = Health;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Wall")
        {
            agent.enabled = false;
            animator.SetBool("Idle", true);
            attack = true;
        }
    }


    public void AfterHit()
    {
        if (agent.enabled) agent.isStopped = false;
    }


    public void OnDeath()
    {
        animator.SetBool("Death", true);
        animator.speed = 1;
        agent.enabled = false;
    }

    public void AfterDeath()
    {
        GameLogic._instance.SkeletonsLeft -= 1;
        GameLogic._instance.Score += Score;
        Destroy(gameObject);
    }

    public void OnAttack()
    {
        WallController.Instance.OnHit(Power);
    }

    public void Update()
    {
        if (isFrozen)
        {
            if (freezeCooldown >= 0.0f)
            {
                freezeCooldown -= Time.deltaTime;
            }
            else
            {
                isFrozen = false;
                animator.speed = 1;
                if (agent.enabled) agent.isStopped = false;
            }
        }
        else
        {
            if (attack)
            {
                if (currentCooldown >= Cooldown)
                {
                    animator.SetTrigger("Attack");
                    currentCooldown = 0.0f;
                }
                else
                {
                    currentCooldown += Time.deltaTime;
                }
            }
        }
    }

    public void Freeze(float duration)
    {
        animator.speed = 0;
        freezeCooldown = duration;
        isFrozen = true;
        if (agent.enabled) agent.isStopped = true;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        

        if (!isFrozen) animator.SetTrigger("Hit");

        if (currentHealth <= 0) OnDeath();
        if (agent.enabled) agent.isStopped = true;
    }
    
}