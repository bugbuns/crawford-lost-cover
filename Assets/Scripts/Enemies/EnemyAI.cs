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
  public Transform[] patrolPoints;
  public int currentWalkpoint=0;
  public bool walkPointSet;
  public float walkPointRange;
  
  //ScriptedMovement
  public bool hasScriptedMovement;

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
    if (hasScriptedMovement)
    {
      return;
    }
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
      walkPointSet = false;
    }
    
  }

  private void setWalkPoint()
  {
    walkPoint = patrolPoints[currentWalkpoint];
    if (currentWalkpoint < patrolPoints.Length - 1)
    {
      currentWalkpoint++;
    }
    else
    {
      currentWalkpoint = 0;
    }

    walkPointSet = true;
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
