using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectTile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator animator;
    
    private void Awake(){
        animator = GetComponent<Animator>();
    }

    public void ActivateProjectTile(float _direction){
        transform.localScale = new Vector3(_direction * Mathf.Abs(transform.localScale.x)
        , transform.localScale.y, transform.localScale.z);
        speed = Mathf.Abs(speed) * _direction;

        lifetime = 0;
        gameObject.SetActive(true); //true
    }
    private void Update(){
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > resetTime)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collider2D){
        GameObject tmp = collider2D.gameObject;
        if(tmp.layer == LayerMask.NameToLayer("ArrowTrap")) return;
        if(tmp.layer != LayerMask.NameToLayer("Enemy")){
            base.OnTriggerEnter2D(collider2D);
            if(animator != null)
                animator.SetTrigger("explore");
            else
                gameObject.SetActive(false);
        }
    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
