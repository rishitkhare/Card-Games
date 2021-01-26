using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    [HideInInspector]
    public CardStack cardStack;

    public RenderCard[] renderedCards;

    // Start is called before the first frame update
    void Start() {
        cardStack = new CardStack(true);
        cardStack.AddCardToTop(new Card(Suit.Clubs, Rank.Eight));
        cardStack.AddCardToTop(new Card(Suit.Hearts, Rank.Nine));
        cardStack.AddCardToTop(new Card(Suit.Clubs, Rank.Eight));
        cardStack.AddCardToTop(new Card(Suit.Clubs, Rank.Nine));
        cardStack.AddCardToTop(new Card(Suit.Clubs, Rank.Eight));
    }

    // Update is called once per frame
    void Update() {
        for(int i = 0; i < 5; i ++) {
            renderedCards[i].renderedCard = new Card(cardStack.GetCardSuit(i),
                cardStack.GetCardRank(i));
        }
    }
}
