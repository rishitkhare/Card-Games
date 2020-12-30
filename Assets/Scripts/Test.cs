using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public RenderCard renderCard;

    // Start is called before the first frame update
    void Start() {
        CardStack a = new CardStack(true);
        a.GenerateDeck52();


        Debug.Log(a);

        a.Shuffle();

        Debug.Log(a);

        a.SortByRank();
        Debug.Log(a);

        Card card = new Card(Suit.Spades, Rank.Ace);
        renderCard.renderedCard = card;
    }
}
