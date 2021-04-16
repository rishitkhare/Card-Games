using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Game : MonoBehaviour {

    public int numberOfPlayers;
    [HideInInspector] 
    public List<CardStack> playerHands;
    [HideInInspector]
    public CardStack currentPlayer;
    [HideInInspector] 
    public int turn;


    // Start is called before the first frame update
    void Awake()
    {
        playerHands = new List<CardStack>();
        AddPlayersToGame();
        currentPlayer = playerHands[0];
        turn = 0;
        GameManager.gm.cardSelector.onCardPickup.AddListener(PickUp);
        GameManager.gm.cardSelector.onCardPlace.AddListener(Place);
    }

    void AddPlayersToGame() {
        for(int i = 0; i < numberOfPlayers; i ++) {
            playerHands.Add(new CardStack(true));
        }
    }

    public abstract void PickUp(Interactable selectedDeck, Card play);
    public abstract void Place(Interactable selectedDeck, Interactable prevDeck);
    public abstract void SetUp();
    public abstract void OnTurnEnd();
}
