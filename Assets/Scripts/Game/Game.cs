using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour {

    public int numberOfPlayers;
    [HideInInspector] 
    public List<Hand> players;
    [HideInInspector] 
    public int turn;

    // Start is called before the first frame update
    void Start()
    {
        players = new List<Hand>();
        AddPlayersToGame();
        turn = 0;
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddPlayersToGame() {
        for(int i = 0; i < numberOfPlayers; i++) {
            Instantiate(GameManager.gm.hand);
        }
    }

    public abstract void SetUp();
    public abstract void Turn();
}
