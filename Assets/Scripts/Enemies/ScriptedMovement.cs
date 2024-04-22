using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptedMovement : MonoBehaviour
{
    // Start is called before the first frame update

   
 public NavMeshAgent agent;

  public Transform player;

  public LayerMask playerMask, groundMask;

  public int health;
  //Patroling
  public Transform walkPoint;
  public Transform[] patrolPoints;
  public int currentWalkpoint=0;
  public bool walkPointSet;
  public float walkPointRange;
  
  

  private float h;
    private float v;
  

  //Attacking
  public float timeBetweenAttacks;
  public bool alreadyAttacked;
  
  //States
  public float sightRange, attackRange;
  public bool playerInSightRange;
  public bool playerInAttackRange;

    public Animator _animator;

  
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Awake()
  {
    
    player=GameObject.Find("Player").transform;
    agent = GetComponent<NavMeshAgent>();
    GetComponent<EnemyAI>().enabled = false;
    
  }

  private void Update()
  {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        _animator.SetFloat("hzInput", 1, 0.1f, Time.deltaTime); //Animations blend together better with float and Time.deltaTime
        _animator.SetFloat("vInput", 1, 0.1f, Time.deltaTime);

        //check for range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
   
    if (!playerInAttackRange && !playerInSightRange)
    {
      StartCoroutine(Patroling());
    }
    else if (playerInSightRange&&!playerInAttackRange)
    {
      ChasePlayer();
    }
    else if (playerInSightRange&&playerInAttackRange)
    {
      AttackPlayer();
    }
  }

  IEnumerator Patroling()
  {
    if (!walkPointSet)
    {
      setWalkPoint();
    }

    if (walkPointSet)
    {
      agent.SetDestination(walkPoint.position);
    }

    Vector3 distanceToWalkPoint = transform.position-walkPoint.position;

    if (distanceToWalkPoint.magnitude < 1f)
    {
      
      agent.SetDestination(transform.position);
      yield return new WaitForSeconds(2f);
      Debug.Log("WayPoint Reached");
      walkPointSet = false;
    }
    
  }

  private void setWalkPoint()
  {
    walkPoint = patrolPoints[currentWalkpoint];
    walkPointSet = true;
    if (currentWalkpoint < patrolPoints.Length - 1)
    {
      currentWalkpoint++;
      
    }
    else
    {
      gameObject.GetComponent<EnemyAI>().enabled = true;
      gameObject.GetComponent<EnemyAI>().health = health;
      Destroy(this);
    }
  }

  private void ChasePlayer()
  {
    agent.SetDestination(player.position);
  }

  private void AttackPlayer()
  {
    agent.SetDestination(transform.position);
    transform.LookAt(player);
    if (!alreadyAttacked)
    {
      //AttackCode
      
      //
      alreadyAttacked = true;
      Invoke(nameof(ResetAttack),timeBetweenAttacks);
    }
  }



  private void ResetAttack()
  {
    alreadyAttacked = false;
  }
  
  
  

  public void TakeDamage(int damage)
  {
    health -= damage;
    if (health <= 0)
    {
      defeatEnemy();
    }
  }

  public void defeatEnemy()
  {
    Destroy(this.gameObject);
  }
}
