using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointEndLevel : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private GameObject winingCanvas;
    private Animator animator;
    private void Awake(){
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.tag == "Player"){
            animator.SetTrigger("appear");
            SoundManager.instance.PlaySound(checkpointSound);
        }
    }
    private void LoadNewLevel(){
        int newLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if(newLevel > 2)
            winingCanvas.SetActive(true);
        else
            SceneManager.LoadScene(newLevel);
    }
}
