﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CrazyEights : Game {
    public Deck output;
    public bool reverse;

    public GameObject ButtonPrefab;

    public List<Button> suitButtons;

    List<ButtonData> PlayerButtons;

    void Start() {
        game = CardGame.CrazyEights;

        PlayerButtons = new List<ButtonData>();

        for(int i = 0; i < numberOfPlayers; i++) {
            //set up button for each player
            GameObject buttonObject = Instantiate(ButtonPrefab, GameManager.gm.CanvasGameObject.transform);
            PlayerButtons.Add(buttonObject.GetComponent<ButtonData>());
            PlayerButtons[i].textComponent.text = $"Player {i + 1}";
            PlayerButtons[i].rectTransform.anchoredPosition = new Vector3(-90f, -40f * (i + 1));
        }

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

        foreach(Player player in players) {
            //clear hand
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
        bool roundOver = false;
        CardEffect();
        foreach (Player p in players) {
            if (p.Hand.NumberOfCards() == 0) {
                roundOver = true;
                OnRoundOver();
                break;
            }
        }

        if (!roundOver) {
            OnNewTurn();
        }
    }

    override
    public void OnNewTurn() {
        output.lockPlace = false;

        currentPlayer = players[turn % players.Count];
        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
    }

    #endregion overrides

    private void OnRoundOver() {
        foreach (Player p in players) {
            p.Score += p.Hand.TotalWorthCrazyEights();
        }

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

        if (deck.cardStack.NumberOfCards() == 0) {
            for (int i = 1; i < output.cardStack.NumberOfCards(); i++) {
                deck.cardStack.AddCardToTop(output.cardStack.TakeBottomCard());
            }

            deck.cardStack.Shuffle();
        }
    }

    private void PickUpFromHand(Card play) {
        output.lockPlace = InvalidPlace(play);
    }

    private void TurnIncrement() {
        if (reverse) {
            turn--;
        }

        else {
            turn++;
        }
    }

    private void CardEffect() {
        if (output.cardStack.GetCardRank(0) == Rank.Eight) {
            ChooseSuit();
            TurnIncrement();
            // implement 8 (buttons)
        }

        else if (output.cardStack.GetCardSuit(0) == Suit.BlackJoker || output.cardStack.GetCardSuit(0) == Suit.RedJoker) {
            // implement J (buttons)
            int chosenPlayerIndex = 0;//ChoosePlayer();
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
    }

    // TODO
    private Suit ChooseSuit() {
        return 0;
    }

    // TODO
    public void ChoosePlayer(int playerID) {

        return; //from the dead
    }

    private bool InvalidPlace(Card play) {

        if (play.Rank != Rank.Eight) {

            if (output.cardStack.GetCardRank(0) == Rank.Eight) {
                if (ChooseSuit() == Suit.Hearts || ChooseSuit() == Suit.Diamonds) {

                    // returns if card is not chosen red suit or joker
                    return (play.Suit & (ChooseSuit() | Suit.RedJoker)) == Suit.None;
                }

                else if (ChooseSuit() == Suit.Hearts || ChooseSuit() == Suit.Diamonds) {

                    // returns if card is not chosen black suit or joker
                    return (play.Suit & (ChooseSuit() | Suit.BlackJoker)) == Suit.None;
                }
            }

            if (output.cardStack.GetCardSuit(0) == Suit.BlackJoker || output.cardStack.GetCardSuit(0) == Suit.RedJoker) {

                // returns if card is not the same color as output joker
                return (( output.cardStack.GetCardSuit(0) == Suit.BlackJoker && (play.Suit & (Suit.Spades | Suit.Clubs | Suit.BlackJoker)) == Suit.None ) ||
                        ( output.cardStack.GetCardSuit(0) == Suit.RedJoker && (play.Suit & (Suit.Diamonds | Suit.Hearts | Suit.RedJoker)) == Suit.None ));
            }

            if (play.Suit != Suit.RedJoker || play.Suit != Suit.BlackJoker) {

                // returns if card is not same rank and not same suit
                return (play.Rank != output.cardStack.GetCardRank(0) && play.Suit != output.cardStack.GetCardSuit(0));
            }

            else {

                // returns if card joker is not the same color as output
                return (( play.Suit == Suit.BlackJoker && (output.cardStack.GetCardSuit(0) & (Suit.Spades | Suit.Clubs | Suit.BlackJoker)) == Suit.None ) ||
                        ( play.Suit == Suit.RedJoker && (output.cardStack.GetCardSuit(0) & (Suit.Hearts | Suit.Diamonds | Suit.RedJoker)) == Suit.None ));
            }
        }

        else { // if card is eight
            return false;
        }
    }
}
