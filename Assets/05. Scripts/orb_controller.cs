using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class orb_controller : MonoBehaviour
{

    public GameObject[] orbs; //Array in which we store the orb positions to spawn.
    public bool is_there_orb = true; //Boolean to know if there are any orbs in game or not.
    public GameObject prefab_instantiate; //Orb to instantiate.
    public float orb_spawn_delay; //Variable to control the delay of the spawn for the next orb.

    // Start is called before the first frame update
    void Start()
    {
        orbs = GameObject.FindGameObjectsWithTag("orb_spawner"); //We assign the orb spawners to the array, based on a tag.
    }

    // Update is called once per frame
    void Update()
    {
        if (is_there_orb) //We check if there's already an orb in game.
        {
            StartCoroutine("Spawn"); //We start the spawn of another orb if there isn't any in the scene.
        }
    }

    public IEnumerator spawn_false() //Function that's called from the orb before it's destroyed to start the process of spawning another one.
    {
        yield return new WaitForSeconds(orb_spawn_delay); //Delay.
        is_there_orb = true; //We start the spawn of another orb.
    }

    public void Spawn() //Function that spawns another orb.
    {
        //We instantiate an orb in any of the points where orbs can spawn. We do this by throwing a dice, the maximum number is
        //the length of the array in which we stored the possible spawn points for the orbs.
        Instantiate(prefab_instantiate, orbs[Random.Range(0, orbs.Length)].transform.position, Quaternion.identity);
        is_there_orb = false; //We just spawned an orb, so we stop the spawning of orbs until further notice.
    }
}
