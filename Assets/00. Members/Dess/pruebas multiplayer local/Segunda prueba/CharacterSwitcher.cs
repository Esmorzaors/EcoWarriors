using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    int index = 0;
    [SerializeField] List<GameObject> fighrters = new List<GameObject>();
    PlayerInputManager manager;
        
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        index = Random.Range(0, fighrters.Count);
        manager.playerPrefab = fighrters[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch(PlayerInput input)
    {
        index = Random.Range(0, fighrters.Count);
        manager.playerPrefab = fighrters[index];
    }
}
