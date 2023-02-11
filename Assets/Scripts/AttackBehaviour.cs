using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
  [SerializeField]
  private Animator animator;

  [SerializeField]
  private Equipment equipmentSystem;

    void Update()
    {
        // au moment ou on appui surt la touche clique gauche et que lequipment est nul.
        if(Input.GetMouseButtonDown(0) && equipmentSystem.equipedWeaponItem != null)
        {
         
            // alors lanimation qui a comme refference Attack ce lance. (Attack je les creer dans le menu animator) 
            animator.SetTrigger("Attack");
        }
       
    }
}
