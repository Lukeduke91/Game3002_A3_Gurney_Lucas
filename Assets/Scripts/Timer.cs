using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 90;
    public TextMeshProUGUI timerText;

    // Update the clock per frame
    void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //timeValue = 0;
        }

        DisplayTimer(timeValue);
    }
    //displays the timer in the game
    void DisplayTimer(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        //This gives the Minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //This displays the Minutes and seconds on the UI timer
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
