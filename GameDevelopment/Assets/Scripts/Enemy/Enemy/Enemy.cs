using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [Header ("Player Parameters")]
    [SerializeField] private LayerMask playerPlayer;
    
    [Header ("Attack Sound")]
    [SerializeField] private AudioClip attackSound;
    private float coolDownTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHelth;

    private EnemyPatrol enemyPatrol;
    private void Awake(){
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update(){
        coolDownTimer += Time.deltaTime;

        //Attack only when see player
        if(PlayerInSight()){
            if(coolDownTimer > attackCoolDown && playerHelth.currentHealth > 0){
                coolDownTimer = 0;
                anim.SetTrigger("attack");
                SoundManager.instance.PlaySound(attackSound); 
            }
        }
        if (enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private bool PlayerInSight(){
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * range * Mathf.Sign(transform.localScale.x) * colliderDistance
            , new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z)
            , 0, Vector2.left, 0, playerPlayer);

        if(raycastHit2D.collider != null){
            playerHelth = raycastHit2D.transform.GetComponent<Health>();
        }

        return raycastHit2D.collider != null;
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * range * Mathf.Sign(transform.localScale.x) * colliderDistance
            , new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }
    private void DamagePlayer(){
        if(PlayerInSight()){
            playerHelth.TakeDamage(damage);
        }
    }
}
