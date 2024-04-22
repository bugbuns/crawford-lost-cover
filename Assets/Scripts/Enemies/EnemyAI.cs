using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
  public NavMeshAgent agent;

  public Transform player;

  public LayerMask playerMask, groundMask;

  public int health;
  //Patroling
  public Transform walkPoint;
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
  }

  private void Update()
  {
    //check for range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

    if (!playerInAttackRange && !playerInSightRange)
    {
      Patroling();
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

  private void Patroling()
  {
    /*if (!walkPointSet)
    {
      setWalkPoint();
    }*/

    if (walkPointSet)
    {
      agent.SetDestination(walkPoint.position);
    }

    Vector3 distanceToWalkPoint = transform.position-walkPoint.position;

    if (distanceToWalkPoint.magnitude < 1f)
    {
      //walkPointSet = false;
      agent.SetDestination(transform.position);
    }
    
  }

  private void setWalkPoint()
  {
   
  }

  private void ChasePlayer()
  {
    agent.SetDestination(player.position);
    _animator.SetBool("isWalking", true);
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
    _animator.SetBool("isWalking", false);
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
