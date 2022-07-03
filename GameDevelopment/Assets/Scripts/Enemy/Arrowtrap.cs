using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowtrap : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireObjects;
    private float coolDownTimer;
    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [Header ("Player Parameters")]
    [SerializeField] private LayerMask playerPlayer;
    private Health playerHelth;

    [Header ("Attack Parameters")]
    [SerializeField] private float range;
    [Header ("Sound")]
    [SerializeField] private AudioClip arrowSound;
    private void Attack(){
        coolDownTimer = 0;

        SoundManager.instance.PlaySound(arrowSound);
        fireObjects[FindArrow()].transform.position = firePoint.position;
        fireObjects[FindArrow()].GetComponent<EnemyProjectTile>().ActivateProjectTile(1);
    }
    private int FindArrow(){
        for(int i = 0; i < fireObjects.Length; i++){
            if(!fireObjects[i].activeInHierarchy)
                return i;
        }
        return 0;
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
    private void Update(){
        if(PlayerInSight()){
            if(coolDownTimer > attackCoolDown)
                Attack();
            coolDownTimer += Time.deltaTime;
        }
        
    }
}
