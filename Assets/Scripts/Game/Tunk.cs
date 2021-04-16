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
        scores = new List<int>(playerHands.Count);
        SetUp();
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
        
        foreach(CardStack hand in playerHands) {
            hand.ClearCardStack();
        }

        deck.GenerateDeck(true, true);

        deck.cardStack.Shuffle();
        output.cardStack.IsFaceUp = true;
        Deal(cardsDealt);
        ReplaceJokerStart();

        GameManager.gm.handDisplay.cardStack = currentPlayer;
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
        foreach (CardStack hand in playerHands) {
            for (int i = 0; i < cards; i++) {
                hand.AddCardToTop(deck.GetCard());
            }
        }
    }

    public void OnTunkCall(bool empty) {
        // TODO: point system
        Debug.Log("Tunk Called!");
        for (int i = 0; i < playerHands.Count; i++) {
            scores[i] = playerHands[i].TotalWorthTunk();
        }

        if (empty) {

        }
    }

    override
    public void PickUp(Interactable selectedDeck, Card play) {
        if (selectedDeck.Equals(GameManager.gm.handDisplay)) {
            PickupFromHand(play);
        }

        if (selectedDeck.Equals(deck) || selectedDeck.Equals(output)) {
            PickupFromDeckOutput();
        }
    }

    override
    public void Place(Interactable selectedDeck, Interactable prevDeck) {
        if (selectedDeck.Equals(GameManager.gm.handDisplay) && !selectedDeck.Equals(prevDeck)) {
            OnTurnEnd();
        }

        if (!(selectedDeck.Equals(GameManager.gm.handDisplay) && prevDeck.Equals(GameManager.gm.handDisplay))) {
            tunkCall.enabled = false;
        }
    }

    void PickupFromHand(Card play) {
        if (exchange.cardStack.NumberOfCards() != 0) {
            exchange.lockPlace = exchange.cardStack.GetCardRank(0) != play.Rank;
        }
        else {
            exchange.lockPlace = false;
        }
    }

    void PickupFromDeckOutput() {
        exchange.lockPlace = true;
        deck.lockPickup = true;
        output.lockPickup = true;
    }

    override
    public void OnTurnEnd() {
        Debug.Log("Ended Turn!");

        while (exchange.cardStack.NumberOfCards() != 0) {
            output.GiveCard(exchange.GetCard());
        }

        if (deck.cardStack.NumberOfCards() == 0) {
            OnTunkCall(true);
        }

        NewTurn();
    }

    void NewTurn() {
        currentPlayer = playerHands[++turn % playerHands.Count];
        if (turn >= playerHands.Count) {
            tunkCall.enabled = true;
        }

        deck.lockPickup = false;
        exchange.lockPlace = false;
        output.lockPickup = false;

        GameManager.gm.handDisplay.cardStack = currentPlayer;
    }
}
