using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Game : MonoBehaviour {

    public int numberOfPlayers;
    [HideInInspector] 
    public List<Hand> players;
    [HideInInspector]
    public Hand currentPlayer;
    [HideInInspector] 
    public int turn;
    [HideInInspector]
    public CardSelector cs;

    // Start is called before the first frame update
    void Start()
    {
        players = new List<Hand>();
        AddPlayersToGame();
        currentPlayer = players[0] ?? new Hand();
        turn = 0;
        SetUp();
        cs.onCardPickup.AddListener(PickUp);
        cs.onCardPlace.AddListener(Place);
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

    public abstract void PickUp(Interactable selectedDeck, Card play);
    public abstract void Place(Interactable selectedDeck, Interactable prevDeck);
    public abstract void SetUp();
    public abstract void OnTurnEnd();
}
