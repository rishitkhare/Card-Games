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
        render = gameObject.GetComponent<RenderCard>();
        render.cardStack = this.cardStack;
    }

    void Update() {
        try {
            textBox.text = new Card(cardStack.GetCardSuit(0), cardStack.GetCardRank(0)).ToString();
        }
        catch(System.ArgumentOutOfRangeException) {
            textBox.text = "";
        }
    }


}