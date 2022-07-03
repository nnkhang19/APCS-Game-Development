using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void Update(){
        LoadingManager.volume = slider.value;
    }
    public void PlayGame ()
    {
        print(LoadingManager.volume);
        int newLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if(newLevel > 2)
            newLevel = 0;
        SceneManager.LoadScene(newLevel);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
