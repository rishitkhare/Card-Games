using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;


public class BS : Game {
    public Deck output;

    public Button BSCall;
    public List<Button> PlayerList;

    void Start() {
        game = CardGame.BS;
        SetUp();
    }

    override
    public void PickUp(Interactable selectedDeck, Card play) {

    }

    override
    public void Place(Interactable selectedDeck, Interactable prevDeck) {

    }

    override
    public void SetUp() {
        deck.lockPlace = true;
        deck.lockPickup = true;
        output.lockPickup = true;

        deck.cardStack.ClearCardStack();
        output.cardStack.ClearCardStack();

        foreach (Player player in players) {
            player.Hand.ClearCardStack();
        }

        deck.GenerateDeck(true, true);

        if (!singleDeck) {
            deck.GenerateDeck(false, true);
        }

        deck.cardStack.Shuffle();
        output.cardStack.IsFaceUp = true;

        BSDeal();

        while (deck.cardStack.NumberOfCards() != 0) {
            output.cardStack.AddCardToTop(deck.GetCard());
        }

        for (int i = 0; i < players.Count; i++) {
            for (int j = 0; j < cardsDealt; j++) {
                if (players[i].Hand.GetCardRank(j) == Rank.Ace && players[i].Hand.GetCardSuit(j) == Suit.Spades) {
                    currentPlayer = players[turn = i];
                    break;
                }
            }
        }

        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
    }

    override
    public void OnTurnEnd() {

    }

    override
    public void OnNewTurn() {
        currentPlayer = players[++turn % players.Count];
        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
    }

    private void BSDeal() {
        cardsDealt = deck.cardStack.NumberOfCards() / players.Count;
        foreach (Player p in players) {
            for (int i = 1; i <= cardsDealt; i++) {
                p.Hand.AddCardToTop(deck.GetCard());
            }
        }
    }

    public void OnBSCall() {

    }
}
