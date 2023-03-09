using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement1 : MonoBehaviour
{
    Rigidbody rigi;
    public float speed = 5;//velocidad
    private Vector2 movementInput;//vector de movimietno
    [SerializeField]
    private int playerIndex = 0;//index de jugador para llamarlo luego

    void Start()
    {
        rigi = GetComponent<Rigidbody>();

    }


    void Update()
    {
        //movimiento
        Vector3 movement = new Vector3(movementInput.x, rigi.velocity.y, movementInput.y);
        rigi.velocity = movement;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        
        if (movement != Vector3.zero)
        {

            Quaternion rot = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));//We look forward the direction we are walking to.
            rot = rot.normalized;//We normalize the rotation.
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 6 * Time.deltaTime);//We interpolate the rotation.


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
}
