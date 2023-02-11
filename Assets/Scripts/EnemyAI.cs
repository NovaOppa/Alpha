using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]

    [SerializeField]
    // on prend le component creer sur l'ours pour qu il suive.
    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;

    [Header ("Stats")]

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float chaseSpeed;

    // [le modifier a la main sur unity]
    [SerializeField]
    private float detectionRaduis;

    [SerializeField]
    private float attackRaduis;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float damageDealt;

    [SerializeField]
    private float rotationSpeed;

    //prendre la position du joueur Transform.
    private Transform player;

    
    private PlayerStats playerStats;


    [Header("Wandering Parameters")]

    [SerializeField]
    private float wanderingWaitTimeMax;

    [SerializeField]
    private float wanderingWaitTimeMin;

    [SerializeField]
    private float wanderingWaitDistanceMax;

    [SerializeField]
    private float wanderingWaitDistanceMin;

    private bool hasDestination;
    private bool isAttacking;

    private void Awake()
    {
        Transform playerTransform =  GameObject.FindGameObjectWithTag("Player").transform;

        player = playerTransform;
        playerStats = playerTransform.GetComponent<PlayerStats>();
    }

    void Update()
    {

        //si(la distance entre 2 object(position player, la position de lobject ou est se script on ecrit transform) est inferieur o radius)
        if(Vector3.Distance(player.position, transform.position) < detectionRaduis && !playerStats.isDead)
        {
            agent.speed = chaseSpeed;

            // kel direction regarder
            Quaternion rot = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);

            if(!isAttacking)
            {
                if(Vector3.Distance(player.position, transform.position) < attackRaduis)
            {
                //attaquer le joueur
              StartCoroutine(AttackPlayer()); 
            }
            else
            {
                // la destination du NavMeshAgent"agent"(va suivre la position du joueur).
            agent.SetDestination(player.position);
            }
        }

      }
        // le cas inverse dune condition if().
        else
        {
            agent.speed = walkSpeed;

          //si (la distance quil reste entre lours et sa destination < 0.75f et quil nattend pas de nouvelle destination)
            if(agent.remainingDistance < 0.75f && !hasDestination)
            {
                StartCoroutine(GetNewDestination());
            }
            
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
     
    }

    IEnumerator GetNewDestination()
    {
        // ours a une destination
       hasDestination = true;

       yield return new WaitForSeconds(Random.Range(wanderingWaitTimeMax, wanderingWaitTimeMin)); 


       Vector3 nextDestination = transform.position;
       nextDestination += Random.Range(wanderingWaitDistanceMin, wanderingWaitDistanceMax) * new Vector3(Random.Range(-1, 1), 0f, Random.Range(-1, 1)).normalized;

       NavMeshHit hit;
       if(NavMesh.SamplePosition(nextDestination, out hit, wanderingWaitDistanceMax, UnityEngine.AI.NavMesh.AllAreas))
       {
           agent.SetDestination(hit.position);
       }
       hasDestination = false;

    }

    IEnumerator AttackPlayer()
        {
            isAttacking = true;
            agent.isStopped = true;

            animator.SetTrigger("Attack");

            playerStats.TakeDamage(damageDealt);

            yield return new WaitForSeconds(attackDelay);

        agent.isStopped = false;
        isAttacking = false;
        }
    

    private void OnDrawGizmos()
    {
        // la couleur de gizmos
         Gizmos.color = Color.yellow;
        // on choisit la forme de gizmos(mettre lendroit de cette sphere, et la taille de la sphere).
         Gizmos.DrawWireSphere(transform.position, detectionRaduis);

         // la couleur de gizmos
         Gizmos.color = Color.red;
        // on choisit la forme de gizmos(mettre lendroit de cette sphere, et la taille de la sphere).
         Gizmos.DrawWireSphere(transform.position, attackRaduis);
    }



}
