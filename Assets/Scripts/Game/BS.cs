using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BS : Game {
    public Deck output;

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

        cardsDealt = deck.cardStack.NumberOfCards() / players.Count;
        foreach (Player p in players) {
            for (int i = 1; i <= cardsDealt; i++) {
                p.Hand.AddCardToTop(deck.GetCard());
            }
        }

        while (deck.cardStack.NumberOfCards() != 0) {
            output.cardStack.AddCardToTop(deck.GetCard());
        }

        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
    }

    override
    public void OnTurnEnd() {

    }

    override
    public void OnNewTurn() {

    }
}
