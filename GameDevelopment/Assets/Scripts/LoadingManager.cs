using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.N)){
            SceneManager.LoadScene(0);
        }
    }
}
