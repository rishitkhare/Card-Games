using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Game : MonoBehaviour {

    public int numberOfPlayers;
    [HideInInspector] 
    public List<Player> players;
    [HideInInspector]
    public Player currentPlayer;
    [HideInInspector] 
    public int turn;


    // Start is called before the first frame update
    void Awake()
    {
        players = new List<Player>();
        AddPlayersToGame();
        currentPlayer = players[0];
        turn = 0;
        GameManager.gm.cardSelector.onCardPickup.AddListener(PickUp);
        GameManager.gm.cardSelector.onCardPlace.AddListener(Place);
    }

    void AddPlayersToGame() {
        for(int i = 0; i < numberOfPlayers; i ++) {
            players.Add(new Player($"{i + 1}"));
        }
    }

    public abstract void PickUp(Interactable selectedDeck, Card play);
    public abstract void Place(Interactable selectedDeck, Interactable prevDeck);
    public abstract void SetUp();
    public abstract void OnTurnEnd();
}
