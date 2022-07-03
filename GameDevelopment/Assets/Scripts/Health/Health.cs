using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float statingHealth;
    public float currentHealth {get; private set;}
    private Animator animator;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    private bool isFlash;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;

    [Header ("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake(){
        currentHealth = statingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isFlash = false;
    }

    private IEnumerator Invunerability(){
        Physics2D.IgnoreLayerCollision(8,9, true);
        isFlash = true;
        for (int i = 0; i < numberOfFlashes; i++){
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        isFlash = false;
        Physics2D.IgnoreLayerCollision(8,9, false);
    }
    public void TakeDamage(float _damage){
        if(!isFlash){
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, statingHealth);
            if(currentHealth > 0){
                animator.SetTrigger("hurt");
                StartCoroutine(Invunerability());
                SoundManager.instance.PlaySound(hurtSound);
            }
            else{
                if(!dead){
                    dead = true;
                    foreach(Behaviour item in components){
                        item.enabled = false; 
                    }
                    if(gameObject.tag == "Player"){
                        animator.SetBool("grounded", true); 
                        animator.SetBool("run", false);
                        animator.Play("Idle");
                    }
                    
                    animator.SetTrigger("die");
                    SoundManager.instance.PlaySound(deathSound);
                }
            }
        }
    }
    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + Mathf.Abs(_value), 0, statingHealth);
    }
    public void Respawn(){
        print("Respawn_Health");
        dead = false;
        AddHealth(statingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");
        StartCoroutine(Invunerability());

        foreach(Behaviour item in components){
            item.enabled = true; 
        }
    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
