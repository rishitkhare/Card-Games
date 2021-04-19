﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Tunk : Game {
    public Deck output;
    public Deck exchange;

    public Button tunkCall;

    // Start is called before the first frame update
    void Start() {
        game = CardGame.Tunk;
        SetUp();
    }

    #region overrides

    override
    public void SetUp() {
        tunkCall.enabled = false;
        deck.lockPlace = true;
        exchange.lockPickup = true;
        output.lockPlace = true;

        deck.cardStack.ClearCardStack();
        output.cardStack.ClearCardStack();
        exchange.cardStack.ClearCardStack();
        
        foreach(Player player in players) {
            player.Hand.ClearCardStack();
        }
        
        deck.GenerateDeck(true, true);

        if (!singleDeck) {
            deck.GenerateDeck(false, true);
        }

        deck.cardStack.Shuffle();
        output.cardStack.IsFaceUp = true;
        exchange.cardStack.IsFaceUp = true;
        Deal();
        ReplaceJokerStart();

        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
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

    override
    public void OnTurnEnd() {
        Debug.Log("Ended Turn!");

        while (exchange.cardStack.NumberOfCards() != 0) {
            output.GiveCard(exchange.GetCard());
        }

        if (deck.cardStack.NumberOfCards() == 0) {
            OnDeckEmpty();
        }

        OnNewTurn();
    }

    override
    public void OnNewTurn() {
        currentPlayer = players[++turn % players.Count];
        if (turn >= players.Count) {
            tunkCall.enabled = true;
        }

        deck.lockPickup = false;
        exchange.lockPlace = false;
        output.lockPickup = false;

        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
    }

    #endregion overrides

    private void ReplaceJokerStart() {
        while (output.cardStack.NumberOfCards() == 0 || output.cardStack.GetCardSuit(0) == Suit.BlackJoker || output.cardStack.GetCardSuit(0) == Suit.RedJoker) {
            if (output.cardStack.NumberOfCards() != 0) {
                deck.cardStack.AddCardToBottom(output.GetCard());
            }
            output.cardStack.AddCardToTop(deck.GetCard());
        }
    }

    public void OnTunkCall() {
        Debug.Log("Tunk Called!");

        //sort based on round earning
        SortByHandWorth();

        bool tiedHand = players[0].Hand.TotalWorthTunk() == players[1].Hand.TotalWorthTunk();

        if (players[0].Equals(currentPlayer) && !tiedHand) {
            for (int i = 1; i < players.Count; i++) {
                players[i].Score += players[i].Hand.TotalWorthTunk();
            }
        }
        else {
            currentPlayer.Score += 30;
        }

        SetUpNewRound();
    }

    public void OnDeckEmpty() {
        Debug.Log("Emptied Deck!");

        SortByHandWorth();

        bool tiedHand = players[0].Hand.TotalWorthTunk() == players[1].Hand.TotalWorthTunk();

        if(tiedHand) {
            int i = 1;
            players[0].Score += 30;

            while(players[i] == players[i - 1]) {
                players[i].Score += 30;
                i++;
            }
        }

        SetUpNewRound();
    }

    private void SetUpNewRound() {
        players.Sort();

        if (players[players.Count - 1].Score >= pointCap) {
            LogTheLeader(true);
            // end game
        }

        else {
            LogTheLeader(false);
            SortByHandWorth();
            SetUp();
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
}
