using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    
    private Animator animator;
    private PlayerMovement playerMovement;
    private float coolDownTimer = Mathf.Infinity;
    private void Awake(){
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.J) && coolDownTimer > attackCoolDown && playerMovement.canAttack())  //Input.GetMouseButton(0)
            Attack();
        coolDownTimer += Time.deltaTime;
    }
    private void Attack(){
        SoundManager.instance.PlaySound(fireballSound);
        animator.SetTrigger("attack");
        coolDownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<FireballAttack>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball(){
        for (int i = 0; i < fireballs.Length; i++){
            if(!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
