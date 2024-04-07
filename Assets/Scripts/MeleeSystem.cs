using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeSystem : MonoBehaviour
{
   [Header("Attacking")] 
   public float attackDistance = 3f;
   public float attackDelay = 0.4f;
   public float attackSpeed = 1f;
   public LayerMask attackLayer;
   private bool attacking = false;
   private bool readyToAttack = true;
   private int attackCount;
   
   //Input
   public PlayerControls input;
   private InputAction attack;

   //References
   public Transform attackPoint;
   public HUDManager hudManager;

   private void Awake()
   {
       input = new PlayerControls();
       
   }

   public void Attack(InputAction.CallbackContext context)
   {
       if (!readyToAttack || attacking||!hudManager.meleeActive) return;
       readyToAttack = false;
       attacking = true;
       
       Invoke(nameof(ResetAttack),attackSpeed);
       Invoke(nameof(AttackRaycast),attackDelay);
       
       
   }

   void ResetAttack()
   {
       attacking = false;
       readyToAttack = true;
   }

   void AttackRaycast()
   {
       if (Physics.Raycast(attackPoint.position, attackPoint.forward, out RaycastHit hit, attackDistance, attackLayer))
       {
           Debug.Log("Swing");
           if(hit.collider.CompareTag("Enemy")) 
           {
               Debug.Log("Enemy");
               hit.collider.GetComponent<EnemyAI>().TakeDamage(PlayerStats.Instance.activeMelee.GetDamage());
           }

           if (PlayerStats.Instance.activeMelee.itemType != MeleeItem.MeleeItemType.Fists)
           {
               PlayerStats.Instance.activeMelee.meleeHealth--;
           }
           
           hudManager.refreshMeleeHud();
           
       }
   }

   private void OnEnable()
   {
       attack = input.Player.FireTap;
       attack.Enable();
       attack.performed += Attack;
   }

   private void OnDisable()
   {
       attack.Disable();
   }
}
