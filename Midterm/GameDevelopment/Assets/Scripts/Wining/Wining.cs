using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wining : MonoBehaviour
{
    public void LoadMainMenu(){
        SceneManager.LoadScene(0);
    }
    public void StartAgain(){
        SceneManager.LoadScene(1);
    }
}
