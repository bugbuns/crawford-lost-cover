using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

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
  public bool isStandStillEnemy=false;

  private float h;
    private float v;
  

  //Attacking
  public float timeBetweenAttacks;
  public bool alreadyAttacked;
  public RaycastHit rayhit;
  public Transform attackPoint;
  public LayerMask whatIsPlayer;
  
  
  //States
  public float sightRange, attackRange;
  public bool playerInSightRange;
  public bool playerInAttackRange;

    public Animator _animator;

    
    void Start()
    {
        if(!isStandStillEnemy)setWalkPoint();
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
      if (isStandStillEnemy) {agent.SetDestination(transform.position); return;} //If the enemy has no movement, stand still
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

  public void Patroling()
  {
    if (!walkPointSet)
    {
      Invoke(nameof(setWalkPoint),2f);
    }

    if (walkPointSet)
    {
      agent.SetDestination(walkPoint.position);
      _animator.SetBool("isWalking", true);
      
    }

    Vector3 distanceToWalkPoint = transform.position-walkPoint.position;
    
    if (distanceToWalkPoint.magnitude < 1f)
    {
     
      _animator.SetBool("isWalking", false);
      agent.SetDestination(transform.position);
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
      Debug.Log("walkpoint set");
      return;
    }
    Debug.Log("Patrol Points");
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
    _animator.SetBool("isWalking", false);
    transform.LookAt(player);
    if (!alreadyAttacked)
    {
            int attackMode = UnityEngine.Random.Range(1, 4);
            _animator.SetTrigger("Punch " + attackMode);
            Debug.Log("Punch");
      if (Physics.Raycast(attackPoint.transform.position, attackPoint.transform.forward, out rayhit, attackRange+5, whatIsPlayer))
      {
        Debug.Log(rayhit.collider.name);
        if(rayhit.collider.CompareTag("Player"))
          rayhit.collider.GetComponent<playerController>().takeDamage(damageDealt);
        rayhit.collider.GetComponent<Rigidbody>().AddForce(Vector3.back*100);
        HitPlayer();
      }
      //AttackCode

      else
      {
        Debug.Log("Missed");
      }
      //
      alreadyAttacked = true;
      Invoke(nameof(ResetAttack),timeBetweenAttacks);
    }
    
  }



  private void ResetAttack()
  {
    alreadyAttacked = false;
  }

  public void HitPlayer()
  {
    player.GetComponent<playerController>().takeDamage(damageDealt);
    
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
