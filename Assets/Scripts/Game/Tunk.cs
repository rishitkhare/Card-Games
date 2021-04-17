using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tunk : Game {
    public Deck deck;
    public Deck output;
    public Deck exchange;

    public int cardsDealt;
    public int pointCap;

    public Button tunkCall;

    // Start is called before the first frame update
    void Start() {
        SetUp();
    }

    #region overrides

    override
    public void SetUp() {
        SortByTunkWorth();
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

        deck.cardStack.Shuffle();
        output.cardStack.IsFaceUp = true;
        Deal(cardsDealt);
        ReplaceJokerStart();

        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
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

        NewTurn();
    }

    #endregion overrides

    private void ReplaceJokerStart() {
        while (output.cardStack.NumberOfCards() == 0 || output.cardStack.GetCardSuit(0) == Suit.BlackJoker || output.cardStack.GetCardSuit(0) == Suit.BlackJoker) {
            if (output.cardStack.NumberOfCards() != 0) {
                deck.cardStack.AddCardToBottom(output.GetCard());
            }
            output.cardStack.AddCardToTop(deck.GetCard());
        }
    }

    private void Deal(int cards) {
        foreach (Player player in players) {
            for (int i = 0; i < cards; i++) {
                player.Hand.AddCardToTop(deck.GetCard());
            }
        }
    }

    public void OnTunkCall() {
        Debug.Log("Tunk Called!");

        //sort based on round earning
        SortByTunkWorth();

        bool tiedHand = players[0].Hand.TotalWorthTunk() == players[1].Hand.TotalWorthTunk();

        if (players[0].Equals(currentPlayer) && !tiedHand) {
            for (int i = 1; i < players.Count; i++) {
                players[i].Score += players[i].Hand.TotalWorthTunk();
            }
        }
        else {
            currentPlayer.Score += 30; //RIP
        }

        if(players[0].Score >= pointCap) {
            LogTheLeader(true);
        }
        else {
            LogTheLeader(false);
            SetUp();
        }

        //TODO : Compare hand leaderboard


    }

    public void OnDeckEmpty() {
        Debug.Log("Emptied Deck!");

        SortByTunkWorth();

        bool tiedHand = players[0].Hand.TotalWorthTunk() == players[1].Hand.TotalWorthTunk();

        if(tiedHand) {
            int i = 1;
            players[0].Score += 30;

            while(players[i] == players[i - 1]) {
                players[i].Score += 30;
                i++;
            }
        }

        LogTheLeader(false);
        SetUp();
    }

    private void LogTheLeader(bool gameEnded) {
        //sort based on total score
        players.Sort();

        if (players[0].Score == players[1].Score) {
            int i = 1;
            string message = $"Players {players[0].ID}";

            while (players[i] == players[i - 1]) {
                message += $", {players[i].ID}";
                i++;
            }

            message += gameEnded ? " won!" : " are in the lead!";
            Debug.Log(message);

        }
        else {
            string message = $"Player {players[0].ID} ";
            message += gameEnded ? " won!" : " is in the lead!";
            Debug.Log(message);
        }
    }

    private void SortByTunkWorth() {
        //TODO : Get the train of thought to leave the station
        //TODO : Make sure there is zero case scenario for the beginning call
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

    void NewTurn() {
        currentPlayer = players[++turn % players.Count];
        if (turn >= players.Count) {
            tunkCall.enabled = true;
        }

        deck.lockPickup = false;
        exchange.lockPlace = false;
        output.lockPickup = false;

        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
    }
}
