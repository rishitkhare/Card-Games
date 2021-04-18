using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CrazyEights : Game {
    public Deck output;

    void Start() {
        SetUp();
    }

    #region overrides
    override
    public void PickUp(Interactable selectedDeck, Card play) {
        if (selectedDeck.Equals(GameManager.gm.handDisplay)) {
            PickUpFromHand(play);
        }

        if (selectedDeck.Equals(deck)) {
            PickUpFromDeck();
        }
    }

    override
    public void Place(Interactable selectedDeck, Interactable prevDeck) {
        if (selectedDeck.Equals(output)) {
            OnTurnEnd();
        }
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
        Deal();
        ReplaceSpecial();

        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
    }

    override
    public void OnTurnEnd() {

    }

    override
    public void OnNewTurn() {

    }

    #endregion overrides

    private void ReplaceSpecial() {
        while (output.cardStack.NumberOfCards() == 0 ||
            output.cardStack.GetCardRank(0) == Rank.Eight ||
            output.cardStack.GetCardRank(0) == Rank.Two ||
            output.cardStack.GetCardRank(0) == Rank.Queen ||
            output.cardStack.GetCardSuit(0) == Suit.BlackJoker ||
            output.cardStack.GetCardSuit(0) == Suit.RedJoker) {

            if (output.cardStack.NumberOfCards() != 0) {
                deck.cardStack.AddCardToBottom(output.GetCard());
            }

            output.cardStack.AddCardToTop(deck.GetCard());
        }
    }

    private void PickUpFromDeck() {
        output.lockPlace = true;
    }

    private void PickUpFromHand(Card play) {
        if (play.Rank != Rank.Eight ||
            play.Suit == Suit.BlackJoker && (output.cardStack.GetCardSuit(0) == Suit.Diamonds || output.cardStack.GetCardSuit(0) == Suit.Hearts) ||
            play.Suit == Suit.RedJoker && (output.cardStack.GetCardSuit(0) == Suit.Clubs || output.cardStack.GetCardSuit(0) == Suit.Spades)) {

            output.lockPlace = true;
        }

        else {
            output.lockPlace = false;
        }
    }
}
