using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSoundsEffects : MonoBehaviour
{
    //This script plays sound effects when main menu buttons are pressed.
    public AudioSource src;
    public AudioClip sfx1, sfx2;

    public void Button_Pulsado()
    {
        src.clip = sfx1;
        src.Play();
    } 
    public void Button_Back()
    {
        src.clip = sfx2;
        src.Play();
    } 
   
}
