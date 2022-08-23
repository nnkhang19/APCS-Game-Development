using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image healthBarTotal;
    [SerializeField] private Image healthBarCurrent;
    private void Start(){
        healthBarTotal.fillAmount = playerHealth.currentHealth / 10;
    }
    private void Update(){
        healthBarCurrent.fillAmount = playerHealth.currentHealth / 10;
    }
}
