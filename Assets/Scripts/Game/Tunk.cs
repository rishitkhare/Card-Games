using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tunk : Game {
    public Deck deck;
    public Deck output;
    public Deck exchange;

    public int cardsDealt;

    public Button tunkCall;
    List<int> scores;

    // Start is called before the first frame update
    void Start()
    {
        scores = new List<int>(players.Count);
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override
    public void SetUp() {
        tunkCall.enabled = false;
        deck.lockPlace = true;
        exchange.lockPickup = true;
        output.lockPlace = true;

        deck.cardStack.ClearCardStack();
        output.cardStack.ClearCardStack();
        exchange.cardStack.ClearCardStack();
        
        foreach(Hand hand in players) {
            hand.cardStack.ClearCardStack();
        }

        deck.GenerateDeck(true, true);

        deck.cardStack.Shuffle();
        output.cardStack.IsFaceUp = true;
        Deal(cardsDealt);
        ReplaceJokerStart();
    }

    void ReplaceJokerStart() {
        while (output.cardStack.NumberOfCards() == 0 || output.cardStack.GetCardSuit(0) == Suit.BlackJoker || output.cardStack.GetCardSuit(0) == Suit.BlackJoker) {
            if (output.cardStack.NumberOfCards() != 0) {
                deck.cardStack.AddCardToBottom(output.GetCard());
            }
            output.cardStack.AddCardToTop(deck.GetCard());
        }
    }

    void Deal(int cards) {
        foreach (Hand hand in players) {
            for (int i = 0; i < cards; i++) {
                hand.cardStack.AddCardToTop(deck.GetCard());
            }
        }
    }

    public void OnTunkCall(bool empty) {
        // TODO: point system
        for (int i = 0; i < players.Count; i++) {
            scores[i] = players[i].cardStack.TotalWorthTunk();
        }

        if (empty) {

        }
    }

    override
    public void PickUp(Interactable selectedDeck, Card play) {
        if (selectedDeck.Equals(currentPlayer)) {
            PickupFromHand(play);
        }

        if (selectedDeck.Equals(deck) || selectedDeck.Equals(output)) {
            PickupFromDeckOutput();
        }
    }

    override
    public void Place(Interactable selectedDeck, Interactable prevDeck) {
        if (selectedDeck.Equals(currentPlayer) && !selectedDeck.Equals(prevDeck)) {
            OnTurnEnd();
        }

        if (!(selectedDeck.Equals(currentPlayer) && prevDeck.Equals(currentPlayer))) {
            tunkCall.enabled = false;
        }
    }

    void PickupFromHand(Card play) {
        if (exchange.cardStack.NumberOfCards() != 0) {
            exchange.lockPlace = exchange.cardStack.TakeTopCard().Rank != play.Rank;
        }
    }

    void PickupFromDeckOutput() {
        exchange.lockPlace = true;
        deck.lockPickup = true;
        output.lockPickup = true;
    }

    override
    public void OnTurnEnd() {
        while (exchange.cardStack.NumberOfCards() != 0) {
            output.GiveCard(exchange.GetCard());
        }

        if (deck.cardStack.NumberOfCards() == 0) {
            OnTunkCall(true);
        }

        NewTurn();
    }

    void NewTurn() {
        currentPlayer = players[++turn % players.Count];
        if (turn >= players.Count) {
            tunkCall.enabled = true;
        }
    }
}
