using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectalbe : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.tag == "Player"){
            SoundManager.instance.PlaySound(pickupSound);
            collider2D.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
