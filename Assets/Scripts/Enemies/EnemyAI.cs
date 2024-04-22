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

  public int damageDealt=10;
  //Patroling
  public Transform walkPoint;
  public Transform[] patrolPoints;
  public Transform[] scriptedPoints;
  public int currentWalkpoint=0;
  public bool walkPointSet=false;
  public float walkPointRange;
  
  //ScriptedMovement
  public bool hasScriptedMovement=true;

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
      _animator.SetBool("isWalking", true);
      Debug.Log("Moving");
    }

    Vector3 distanceToWalkPoint = transform.position-walkPoint.position;

    if (distanceToWalkPoint.magnitude < 1f)
    {
      _animator.SetBool("isWalking", false);
      agent.SetDestination(transform.position);
      yield return new WaitForSeconds(2f);
      walkPointSet = false;
    }
    
  }

  private void setWalkPoint()
  {
    if (hasScriptedMovement)
    {
      walkPoint = scriptedPoints[currentWalkpoint];
      if (currentWalkpoint < scriptedPoints.Length - 1)
      {
        currentWalkpoint++;
      }
      else
      {
        hasScriptedMovement = false;
      }

      walkPointSet = true;

      return;
    }
    
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
      HitPlayer();
      
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

  public void HitPlayer()
  {
    //player.GetComponent<playerController>().takeDamage(damageDealt);
    
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
