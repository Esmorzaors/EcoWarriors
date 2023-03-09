using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class player2 : MonoBehaviour
{
    Rigidbody rigi;
    public static float vel = 4;//Speed of the player 1
    public int PU_rand; //Variable to access the switch of the power ups, it's given value from the orb.
    public bool call_PU; //Boolean with which we are going to control when to execute a Power up, it's given value from the orb.
    public GameObject suciedad;//Prefab that we are going to instantiate with the skill of the player 2.
    public float castTime;//Max cast time for the skill.
    public static float tmp2 = 0;//Cast timer.
    public Animator myanim;//The animator of the player 2.
    public bool skillActivada = false; //Is the player executing the skill, for the animator.

    public string tag_mat_walk;//We are going to find out which material we are walking on, and we are going to use this
                               //variable to store that information.

    //Variables for the rol_switcher powerup
    public GameObject prefab_suciedades, prefab_plantados; //Prefabs that we are going to instantiate.
    
    //Walking on different surfaces.
    public AudioClip basura, limpio; //Clips audio we are going to play whe walking on different surfaces.
    public AudioSource AS; //Audio sourcepublic AudioClip basura, limpio; //Clips audio



    void Start()
    {
        //References to the different components that we are going to use in this script.
        rigi = GetComponent<Rigidbody>();
        myanim = this.GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
    }


    


    private void Update()
    {

        //We calculate the movement of the player in the two axis.
        Vector3 mov = new Vector3(Input.GetAxisRaw("Horizontal1") * vel, rigi.velocity.y, Input.GetAxisRaw("Vertical1") * vel);
        rigi.velocity = mov;//We apply that vector to the velocity of the rigidbody.

        if (Input.GetAxisRaw("Horizontal1") != 0 || Input.GetAxisRaw("Vertical1") != 0)//If we are moving.
        {
            if (skillActivada)//If we are executing the skill.
            {
                myanim.SetBool("SkillAnim", true);//We set the needed booleans in the animator to the desired state.
                myanim.SetBool("Walking", false);
            }
            else
            {
                myanim.SetBool("Walking", true);//If we are not using the skill, and we are moving, we walk.
            }
            Quaternion rot = Quaternion.LookRotation(new Vector3(mov.x, 0, mov.z));//We look forward the direction we are walking to.
            rot = rot.normalized;//We normalize the rotation.
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 6 * Time.deltaTime);//We interpolate the rotation.
            myanim.SetBool("Iddle", false);//We are not walking.
        }

        if (Input.GetAxisRaw("Horizontal1") == 0 && Input.GetAxisRaw("Vertical1") == 0 && !skillActivada)//If we are moving with activated skill
        {
            if (tag_mat_walk != null)//If we are walking on a surface that is not a default material.
            {
                StartCoroutine("playaudio");//We play the correspondent audio.
            }
            AS.Play();//We play the current clip on the AudioSour
            myanim.SetBool("Walking", false);
            myanim.SetBool("Iddle", true);
        }

        //Raycast to detect the collision with the floor.
        Ray ray = new Ray(transform.position, transform.up * -1);//Our ray.
        RaycastHit hit;//The data of the objects we hit.
        Debug.DrawRay(transform.position, transform.up * -1 * .5f, Color.red);//Debug of the ray to make developing easier.

        if (Physics.Raycast(ray, out hit, .5f) && Input.GetKey(KeyCode.U))//If the ray hits something and we are pressing the U button.
        {
            skillActivada = true;//We are executing the skill.
            myanim.SetBool("SkillAnim", true);
            myanim.SetBool("Iddle", false);
            rigi.velocity = new Vector3(0, 0, 0);//We negate the movement of the player.
            if (hit.transform.CompareTag("Spot") || hit.transform.CompareTag("Planta"))//If the floor has any of those two tags.
            {
                tmp2 += Time.deltaTime;//Real time we've been casting.
                if (tmp2 >= castTime)//If we've achieved the time to cast the skill.
                {
                    Instantiate(suciedad, hit.transform.position, hit.transform.rotation);//We instantiate our kind of floor.
                    Destroy(hit.transform.gameObject);//And we destroy the existing one
                    if (tmp2 > castTime)//If we do not reach the cast time, we reset it, so every time we cast we have to cast the same amount of time.
                    {
                        tmp2 = 0;
                        myanim.SetBool("SkillAnim", false);
                    }
                    print("ensuciar");
                }
            }
        }

        //RAYCAST TO DETECT THE TYPE O FLOOR WE'RE ON.
        if (Physics.Raycast(ray, out hit, .5f))
        {
            if (hit.collider != null)//If we hit something.
            {
                tag_mat_walk = hit.collider.tag;//We store the tag of the floor.
                if (tag_mat_walk == "Suciedad")//If the tag is rubbish.
                {
                    AS.clip = basura;//We play the rubbish sound.
                }
                if (tag_mat_walk == "Planta")//If the tag is flowers.
                {
                    AS.clip = limpio;//We play the flowers sound.
                }
            }


        }



        //Call to the switch everytime an orb is destroyed
        if (call_PU)
        {
            PE_exec();//We execute the switch.
            call_PU = false;//We do not allow this to happen more than once by automatically setting the bool to false.
        }

        if (Input.GetKeyUp(KeyCode.U))//If we release "U"
        {
            tmp2 = 0;
            vel = 4;
            myanim.SetBool("SkillAnim", false);
            skillActivada = false;
        }

        
    }







    //Switch to decide which power up is going to be executed.
    void PE_exec()
    {
        switch (PU_rand)
        {
            case 1:
                StartCoroutine("PU_speed");
                break;
            case 2:
                StartCoroutine("PU_Invert_controls");
                break;
            case 3:
                StartCoroutine("PU_Stun");
                break;
            case 4:
                PU_rol_switcher();
                break;
            case 5:
                print("PU_conquista_de_casillas");
                break;
            default:
                print("try again loser hahaha");
                break;
        }
    }

    public IEnumerator PU_speed()
    {
        float vel_temp;
        vel_temp = vel;//We store the current speed of the player.
        vel += 5; //We add speed to the player
        yield return new WaitForSeconds(1.5f);//For a certain ammount of time
        vel = vel_temp;//And then we set it back to its original value.
    }

    public IEnumerator PU_Stun()
    {
        float temp_vel = player2.vel; //We store the speed of the other player.
        player2.vel = 0; //We set the speed of the other player to zero.
        yield return new WaitForSeconds(1.5f);//Just for 1.5 seconds
        player2.vel = temp_vel;//And then we restore it.

    }

    public IEnumerator PU_Invert_controls()
    {
        float temp_vel = player2.vel;//We store the velocity of the other player.
        player2.vel = player2.vel * -1f;//We invert the velocity of the other player.
        yield return new WaitForSeconds(3f);//Just for a certain ammount of time.
        player2.vel = temp_vel;//Then we restore it.
    }

    

    public void PU_rol_switcher()
    {
        foreach(GameObject obj in player1.plantados)//We instantiate the counterpart of the tile on every tile of a type.
        {
            Vector3 pos_ = obj.transform.position;
            Instantiate(prefab_suciedades, pos_, obj.transform.rotation);
            Destroy(obj);
        }

        foreach (GameObject obj in player1.suciedades)
        {
            Vector3 pos_ = obj.transform.position;
            Instantiate(prefab_plantados, pos_, obj.transform.rotation);
            Destroy(obj);
        }
    }

    public IEnumerator playaudio()
    {
        AS.Play(0);
        yield return new WaitForSeconds(0.3f);
    }
}