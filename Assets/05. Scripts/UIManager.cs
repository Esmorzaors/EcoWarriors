using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Canvas canvasPause, canvasUI;
    public static float min, sec, time;
    public TMP_Text textTimer;
    public Slider sliderplayer1, sliderplayer2;

    void Start()
    {
        canvasPause.GetComponent<Canvas>().enabled = false;//We disable the canvas for the pause, just in case.
        time = 90;//We set the time for the game.
    }

    void Update()
    {
        sliderplayer1.value = player1.tmp; //These are the slider to know when each player has finished casting their skill
        sliderplayer2.value = player2.tmp2;
        if (Input.GetKeyDown(KeyCode.Escape)) //If we press escape.
        { 
            canvasPause.GetComponent<Canvas>().enabled = !canvasPause.GetComponent<Canvas>().enabled;//We enable the canvas pause.

            if (Time.timeScale == 1)
            {    //If the time speed is 1
                Time.timeScale = 0;     //May the time speed become zero.
            }
            else if (Time.timeScale == 0)
            {   //If the time speed is 0
                Time.timeScale = 1;     //May the time speed become one.
            }
        }

        //Clock
        time = time - Time.deltaTime;
        min = time / 60;
        min = Mathf.FloorToInt(min);
        sec = Mathf.FloorToInt(time - min * 60);
        if (sec > 59)
        {
            sec = 0;
        }

        textTimer.text = string.Format("{0:00}:{01:00}", min, sec);

        if (time <= 0)
        {
            Time_up();//If the time reaches zero, we call the function that manages it.
        }

    }

    static void Time_up()
    {
        if (UIManager.time <= 0)
        {
            if (player1.plantados.Length < player1.suciedades.Length)//We check which player has the most tiles under the control
            {
                SceneManager.LoadScene("player2win");//And we load a scene depending on who won.
            }
            else
            {
                SceneManager.LoadScene("player1win");
            }
        }
    }

    public void exitToMenu()
    {
        SceneManager.LoadScene("Inicio"); //Exit to menu function.

    }
}
