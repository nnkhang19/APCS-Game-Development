using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private float damage;
    private Health playerHealth;
    private void Update(){
        if(playerHealth != null){
            playerHealth.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.tag == "Player"){
            playerHealth = collider2D.GetComponent<Health>();
            collider2D.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collider2D){
        if(collider2D.tag == "Player"){
            playerHealth = null;
        }
    }
}
