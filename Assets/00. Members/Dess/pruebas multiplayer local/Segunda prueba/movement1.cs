using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement1 : MonoBehaviour
{
    Rigidbody rigi;
    public float speed = 5;//velocidad
    private Vector2 movementInput;//vector de movimietno
    public int playerIndex ;//index de jugador para llamarlo luego


    public int PU_rand; //Variable to access the switch of the power ups, it's given value from the orb.
    public bool call_PU; //Boolean with which we are going to control when to execute a Power up, it's given value from the orb.
    public GameObject prefabSkill;//Prefab that we are going to instantiate with the skill of the player 1.
    public float castTime;//Max cast time for the skill.
    public static float tmp = 0;//Cast timer.
    public Animator myanim;//The animator of the player 1.
    public bool skillActivada = false; //Is the player executing the skill, for the animator.

    public string tag_mat_walk;//We are going to find out which material we are walking on, and we are going to use this
                               //variable to store that information.

    //Variables for the rol_switcher powerup
    public static GameObject[] plantados, suciedades; //Arrays in which we are going to store the different tiles that are already activated in the scene.
    public GameObject prefab_suciedades, prefab_plantados; //Prefabs that we are going to instantiate.

    //Walking on different surfaces.
    public AudioClip basura, limpio; //Clips audio we are going to play whe walking on different surfaces.
    public AudioSource AS; //Audio sourcepublic AudioClip basura, limpio; //Clips audio


    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        myanim = this.GetComponent<Animator>();
        AS = GetComponent<AudioSource>();

    }


    void Update()
    {
        //movimiento
        Vector3 movement = new Vector3(movementInput.x, rigi.velocity.y, movementInput.y);
        rigi.velocity = movement;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        //We calculate the movement of the player
        if (movement != Vector3.zero)
        {

            Quaternion rot = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));//We look forward the direction we are walking to.
            rot = rot.normalized;//We normalize the rotation.
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 6 * Time.deltaTime);//We interpolate the rotation.
            myanim.SetBool("Iddle", false);//We are not walking.

            if (skillActivada)//If we are executing the skill.
            {
                myanim.SetBool("SkillAnim", true);//We set the needed booleans in the animator to the desired state.
                myanim.SetBool("Walking", false);
            }
            else
            {
                myanim.SetBool("Walking", true);//If we are not using the skill, and we are moving, we walk.
            }

        }

        //If we are moving with activated skill
        if (movement == Vector3.zero && !skillActivada)
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

        if (Physics.Raycast(ray, out hit, .5f) && Input.GetKey(KeyCode.E)) //If the ray hits something and we are pressing the E button.
        {
            skillActivada = true;//We are executing the skill.
            myanim.SetBool("SkillAnim", true);
            myanim.SetBool("Iddle", false);
            rigi.velocity = new Vector3(0, 0, 0);//We negate the movement of the player.
            if (hit.transform.CompareTag("Spot") || hit.transform.CompareTag("Suciedad"))//If the floor has any of those two tags.
            {
                tmp += Time.deltaTime;//Real time we've been casting.
                if (tmp >= castTime)//If we've achieved the time to cast the skill.
                {
                    Instantiate(prefabSkill, hit.transform.position, hit.transform.rotation);//We instantiate our kind of floor.
                    Destroy(hit.transform.gameObject);//And we destroy the existing one
                    if (tmp > castTime)//If we do not reach the cast time, we reset it, so every time we cast we have to cast the same amount of time.
                    {
                        tmp = 0;
                        myanim.SetBool("SkillAnim", false);
                        myanim.SetBool("Iddle", true);

                    }
                    print("plantar");
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

        if (Input.GetKeyUp(KeyCode.E))//If we press "E"
        {
            tmp = 0;
            speed = 4;
            myanim.SetBool("SkillAnim", false);
            skillActivada = false;

        }

    }



    public int GetPlayerIndex()//funcion para llamar a los jugadores
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction)
    {
        movementInput = direction;
    }

    public void SetBoolSkill(bool skill)
    {
        skillActivada = skill;
    }

    public IEnumerator playaudio()
    {
        AS.Play(0);
        yield return new WaitForSeconds(0.3f);
    }



}
