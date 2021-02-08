using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Deck : Interactable {

    RenderCard render;
    public Text textBox;
    public bool redDeck;

    // Start is called before the first frame update
    void Start() {
        cardStack = new CardStack(true);

        if (redDeck) {
            cardStack.GenerateDeck52(true);
        }

        else {
            cardStack.GenerateDeck52(false);
        }

        render = gameObject.GetComponent<RenderCard>();
        render.isFlipped = true;
    }

    void Update() {
        try {
            Suit suit = cardStack.GetCardSuit(0);
            Rank rank = cardStack.GetCardRank(0);
            bool back = cardStack.GetCardBack(0);
            Card cardData = new Card(suit, rank, back);

            textBox.text = cardData.ToString();
        }
        catch (System.ArgumentOutOfRangeException) {
            textBox.text = "";
        }

        try {
            render.renderedCard = new Card(cardStack.GetCardSuit(0), cardStack.GetCardRank(0), cardStack.GetCardBack(0));
        }
        catch (System.ArgumentOutOfRangeException) {
            render.renderedCard = null;
        }
    }


}