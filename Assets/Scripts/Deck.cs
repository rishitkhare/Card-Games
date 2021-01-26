﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Deck : MonoBehaviour {
    [HideInInspector]
    public CardStack cardStack;

    RenderCard render;
    public Text textBox;

    // Start is called before the first frame update
    void Start() {
        cardStack = new CardStack(true);
        cardStack.AddCardToTop(new Card(Suit.Spades, Rank.Ace));
        cardStack.AddCardToTop(new Card(Suit.Diamonds, Rank.Eight));
        cardStack.AddCardToTop(new Card(Suit.Diamonds, Rank.Nine));
        cardStack.AddCardToTop(new Card(Suit.Hearts, Rank.Seven));
        cardStack.AddCardToTop(new Card(Suit.BlackJoker, Rank.Joker));
        cardStack.AddCardToTop(new Card(Suit.RedJoker, Rank.Joker));
        render = gameObject.GetComponent<RenderCard>();
    }

    void Update() {
        try {
            Suit suit = cardStack.GetCardSuit(0);
            Rank rank = cardStack.GetCardRank(0);
            Card cardData = new Card(suit, rank);

            textBox.text = cardData.ToString();
        }
        catch (System.ArgumentOutOfRangeException) {
            textBox.text = "";
        }

        try {
            render.renderedCard = new Card(cardStack.GetCardSuit(0), cardStack.GetCardRank(0));
        }
        catch (System.ArgumentOutOfRangeException) {
            render.renderedCard = null;
        }
    }


}