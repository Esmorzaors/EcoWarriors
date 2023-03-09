using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Function called from a OnClick button event.
    public void Jugar()
    {
        StartCoroutine("jugar");
    }

    //Quit function called from a OnClick button event.
    public void Exit()
    {
        Application.Quit();
    }

    //Coroutine to make a smoother transition between the click of the button and the load of the next scene.
    public IEnumerator jugar()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game"); //Poner nombre de la escena.
    }
}
