using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    [Header ("Range Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [Header ("Player Parameters")]
    [SerializeField] private LayerMask playerPlayer;

    [Header ("Fireball Sound")]
    [SerializeField] private AudioClip fireballSound;

    private float coolDownTimer = Mathf.Infinity;
    private Animator anim;
    private EnemyPatrol enemyPatrol;
    private void Awake(){
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update(){
        coolDownTimer += Time.deltaTime;

        //Attack only when see player
        if(PlayerInSight()){
            if(coolDownTimer > attackCoolDown){
                coolDownTimer = 0;
                anim.SetTrigger("rangedAttack");
            }
        }
        if (enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private void RangedAttack(){
        SoundManager.instance.PlaySound(fireballSound);
        coolDownTimer = 0;
        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<EnemyProjectTile>().ActivateProjectTile(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall(){
        for(int i = 0; i < fireballs.Length; i++){
            if(!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlayerInSight(){
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * range * Mathf.Sign(transform.localScale.x) * colliderDistance
            , new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z)
            , 0, Vector2.left, 0, playerPlayer);
        return raycastHit2D.collider != null;
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * range * Mathf.Sign(transform.localScale.x) * colliderDistance
            , new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }
}
