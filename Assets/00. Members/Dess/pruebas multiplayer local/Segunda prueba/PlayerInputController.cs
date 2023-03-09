using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputController : MonoBehaviour
{
    public PlayerInput playerInput;
    private movement1 mover;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<movement1>();
        var index = playerInput.playerIndex;
        mover = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
      

    }


    //funcion que recoge el PlayerInput para darle al movimiento despues de darle a la A
    public void OnMove(CallbackContext context)
    {
        if (mover != null)
            mover.SetInputVector(context.ReadValue<Vector2>());
    }


   
}
