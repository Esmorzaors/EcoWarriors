using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orb : MonoBehaviour
{
    //This script controls the behaviour of the orbs.
    public GameObject spawner_ref; //Ref of the spawn point for the orb.
    orb_controller controller_ref; //Ref to the orb controller (GameManager).
    public AudioClip sf1, sf2, sf3, sf4, sf5; //Different audio clips, assigned from the inspector.
    AudioSource selfref; //Ref to the AudioSource in the orb.

    private void Start()
    {
        spawner_ref = GameObject.Find("GameManager");
        controller_ref = spawner_ref.GetComponent<orb_controller>();
        selfref = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        int rand;
        rand = Random.Range(1, 5); //If anything enters the orb, we throw a dice.
        switch (rand) //Then based on the dice, we decide which sound it's going to be this time.
        {
            case 1:
                selfref.clip = sf1; //We assign a clip to the audio source.
                selfref.Play(); //And we play it, same for every other case within this switch. Each case is a different sound.
                break;
            case 2:
                selfref.clip = sf2;
                selfref.Play();
                break;
            case 3:
                selfref.clip = sf3;
                selfref.Play();
                break;
            case 4:
                selfref.clip = sf4;
                selfref.Play();
                break;
            case 5:
                selfref.clip = sf5;
                selfref.Play();
                break;
            default:
                print("try again loser hahaha");
                break;
        }
        if (other.GetComponent<player1>() != null) //If the player 1 enters the trigger
        {
            other.GetComponent<player1>().call_PU = true; //We let the power up be executed in the player.
            other.GetComponent<player1>().PU_rand = rand; //And we decide which power up is it going to be executed based on the dice we threw.
        }
        if (other.GetComponent<player2>() != null) //Same but for player 2.
        {
            other.GetComponent<player2>().call_PU = true;
            other.GetComponent<player2>().PU_rand = rand;
        }
        controller_ref.StartCoroutine("spawn_false"); //We call the coroutine that's in the orb_controller to spawn another orb.
        Destroy(gameObject); //We destroy the orb.
    }

    

}
