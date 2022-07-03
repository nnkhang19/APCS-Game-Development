using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class FireballAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    private void Awake(){
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void Update(){
        if(hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider2D){
        GameObject tmp = collider2D.gameObject;
        if(tmp.layer == LayerMask.NameToLayer("ArrowTrap")) return;
        if(collider2D.tag != "Player"){
            hit = true;
            boxCollider2D.enabled = false;
            animator.SetTrigger("explore");
            if(tmp.layer == LayerMask.NameToLayer("Enemy")){
                if(collider2D.GetComponent<Health>() != null)
                    collider2D.GetComponent<Health>().TakeDamage(1);
                else
                    tmp.SetActive(false);
            }

        }
    }
    public void SetDirection(float _direction){
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
