using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
  public NavMeshAgent agent;

  public Transform player;

  public LayerMask playerMask, groundMask;

  public int health;
  //Patroling
  public Vector3 walkPoint;
  public bool walkPointSet;
  public float walkPointRange;

  //Attacking
  public float timeBetweenAttacks;
  public bool alreadyAttacked;
  
  //States
  public float sightRange, attackRange;
  public bool playerInSightRange;
  public bool playerInAttackRange;

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
    if (!walkPointSet)
    {
      setWalkPoint();
    }

    if (walkPointSet)
    {
      agent.SetDestination(walkPoint);
    }

    Vector3 distanceToWalkPoint = transform.position-walkPoint;

    if (distanceToWalkPoint.magnitude < 1f)
    {
      walkPointSet = false;
    }
  }

  private void setWalkPoint()
  {
    /*float randomX = Random.Range(-walkPointRange, walkPointRange);
    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
    {
      walkPointSet = true;
    }*/
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
