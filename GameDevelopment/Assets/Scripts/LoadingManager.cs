using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static float volume;
    [SerializeField] public AudioSource audioSource;

    private void Awake(){
        print("LoadingManager awake");
        print("Volum is" + volume.ToString());
        audioSource.volume = volume;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }
    }
}
