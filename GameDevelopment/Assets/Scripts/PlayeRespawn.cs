using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    // private Transform currentCheckpoint;
    private Health playerHealth;
    private Vector3 currentCheckpoint;
    private void Awake(){
        playerHealth = GetComponent<Health>();
        currentCheckpoint = transform.position;
    }
    private void Respawn(){
        transform.position = currentCheckpoint;
        playerHealth.Respawn();
    }
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.transform.tag == "Checkpoint"){
            currentCheckpoint = collider2D.transform.position;
            SoundManager.instance.PlaySound(checkpointSound);
            collider2D.GetComponent<Collider2D>().enabled = false;
            collider2D.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
