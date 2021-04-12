using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunk : Game {
    new List<Hand> players;
    Deck deck;
    Deck output;
    public int cardsDealt;
    private bool tunkCalled;

    // Start is called before the first frame update
    void Start()
    {
        players = base.players;
        deck = new Deck();
        output = new Deck();
        SetUp();
        tunkCalled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override
    public void SetUp() {
        deck.cardStack.Shuffle();
        output.cardStack.ClearCardStack();
        output.cardStack.IsFaceUp = true;
        Deal(cardsDealt);

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

    override
    public void Turn() {
        while (!tunkCalled) {
            Hand hand = players[turn % players.Count];
            /*
            if (turn >= players.Count) {
                Tunk();
            }

            Place(hand);
            Draw(hand);
            turn++;

            if (deck is empty) {
                Tunk();
            }
            */
        }

    }

    void Place(Hand hand) {

    }

}
