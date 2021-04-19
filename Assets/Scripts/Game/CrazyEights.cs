using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CrazyEights : Game {
    public Deck output;
    public bool reverse;

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
        reverse = false;

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
        if (output.cardStack.GetCardRank(0) == Rank.Eight) {
            // implement 8 (buttons)
        }

        else if (output.cardStack.GetCardSuit(0) == Suit.BlackJoker || output.cardStack.GetCardSuit(0) == Suit.RedJoker) {
            // implement J (buttons)
            int chosenPlayerIndex = 0;
            turn = reverse ? turn - Math.Abs(turn % players.Count - chosenPlayerIndex) : turn + Math.Abs(turn % players.Count - chosenPlayerIndex);
            
            for (int i = 1; i <= 4; i++) {
                players[turn % players.Count].Hand.AddCardToBottom(deck.GetCard());
            }
        }

        else if (output.cardStack.GetCardRank(0) == Rank.Two) {
            TurnIncrement();
            for (int i = 1; i <= 2; i++) {
                players[turn % players.Count].Hand.AddCardToBottom(deck.GetCard());
            }

            TurnIncrement();
        }

        else if (output.cardStack.GetCardRank(0) == Rank.Queen) {
            reverse = !reverse;
            TurnIncrement();
        }

        else {
            TurnIncrement();
        }

        currentPlayer = players[turn % players.Count];
        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
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
        if ((play.Rank != Rank.Eight && play.Rank != output.cardStack.GetCardRank(0) && play.Suit != output.cardStack.GetCardSuit(0)) ||
            play.Suit == Suit.BlackJoker && (output.cardStack.GetCardSuit(0) == Suit.Diamonds || output.cardStack.GetCardSuit(0) == Suit.Hearts) ||
            play.Suit == Suit.RedJoker && (output.cardStack.GetCardSuit(0) == Suit.Clubs || output.cardStack.GetCardSuit(0) == Suit.Spades)) {

            output.lockPlace = true;
        }

        else {
            output.lockPlace = false;
        }
    }

    private void TurnIncrement() {
        if (reverse) {
            turn--;
        }

        else {
            turn++;
        }
    }

    private bool InvalidPlace(Card play) {
        if (play.Rank != Rank.Eight) { // if card is not eight

            if (output.cardStack.GetCardSuit(0) == (Suit.BlackJoker | Suit.RedJoker)) { // if output is a joker

                return (( output.cardStack.GetCardSuit(0) == Suit.BlackJoker && (play.Suit != (Suit.Spades | Suit.Clubs | Suit.BlackJoker)) ) ||
                        ( output.cardStack.GetCardSuit(0) == Suit.RedJoker && (play.Suit != (Suit.Diamonds | Suit.Hearts | Suit.RedJoker)) )); // returns if card is not the same color as output joker
            }

            if (play.Suit != (Suit.RedJoker | Suit.BlackJoker)) { // if card is not a joker

                return (play.Rank != output.cardStack.GetCardRank(0) && play.Suit != output.cardStack.GetCardSuit(0)); // returns if card is not same rank and not same suit
            }

            else { // if card is joker

                return (( play.Suit == Suit.BlackJoker && (output.cardStack.GetCardSuit(0) != (Suit.Spades | Suit.Clubs | Suit.BlackJoker)) ) ||
                        ( play.Suit == Suit.RedJoker && (output.cardStack.GetCardSuit(0) != (Suit.Hearts | Suit.Diamonds | Suit.RedJoker)) )); // returns if card joker is not the same color as output
            }
        }

        else { // if card is eight
            return false; // played anytime
        }
    }
}
