using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerSwitcher : MonoBehaviour
{
    int index = 0;
    [SerializeField] List<GameObject> players = new List<GameObject>();
    PlayerInputManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        index = Random.Range(0, players.Count);
        manager.playerPrefab = players[index];
        PlayerInputManager.instance.JoinPlayer(0, -1, null);
        PlayerInputManager.instance.JoinPlayer(1, -1, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchNextSpawnCharacter(PlayerInput input)
    {
        index = Random.Range(0, players.Count);
        manager.playerPrefab = players[index];
    }
    
     
}
