using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header ("Firetrap Timers")]
    
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    [Header ("Sound")]
    [SerializeField] private AudioClip firetrapSound;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool active;
    private bool triggered;

    private Health playerHealth;

    private void Awake(){
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update(){
        if(playerHealth != null && active){
            playerHealth.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.tag == "Player"){
            playerHealth = collider2D.GetComponent<Health>();
            if(!triggered)
                StartCoroutine(ActivateFireTrap());
            if(active)
                collider2D.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collider2D){
        if(collider2D.tag == "Player"){
            playerHealth = null;
        }
    }
    private IEnumerator ActivateFireTrap(){
        triggered = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        active = true;
        spriteRenderer.color = Color.white;
        animator.SetBool("activated", true);
        yield return new WaitForSeconds(activeTime);
        triggered = false;
        active = false;
        animator.SetBool("activated", false);
    }
    
}
