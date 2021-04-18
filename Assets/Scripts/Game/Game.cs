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
    public Deck deck;
    [HideInInspector] 
    public int turn;
    public int cardsDealt;
    public bool singleDeck;


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

    public void Deal() {
        foreach (Player player in players) {
            for (int i = 0; i < cardsDealt; i++) {
                player.Hand.AddCardToTop(deck.GetCard());
            }
        }
    }

    public abstract void PickUp(Interactable selectedDeck, Card play);
    public abstract void Place(Interactable selectedDeck, Interactable prevDeck);
    public abstract void SetUp();
    public abstract void OnTurnEnd();
    public abstract void OnNewTurn();
}
