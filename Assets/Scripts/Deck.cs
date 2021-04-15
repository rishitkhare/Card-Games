using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Deck : Interactable {

    RenderCard render;
    public Text textBox;

    // Start is called before the first frame update
    void Awake() {
        cardStack = new CardStack(false);

        render = gameObject.GetComponent<RenderCard>();
    }

    void Update() {
        render.isFlipped = !cardStack.IsFaceUp;

        if(textBox != null){
            try {
                Suit suit = cardStack.GetCardSuit(0);
                Rank rank = cardStack.GetCardRank(0);
                DeckColor back = cardStack.GetCardBack(0);
                Card cardData = new Card(suit, rank, back);

                textBox.text = cardData.ToString();
            }
            catch (System.ArgumentOutOfRangeException) {
                textBox.text = "";
            }
        }

        try {
            render.renderedCard = new Card(cardStack.GetCardSuit(0), cardStack.GetCardRank(0), cardStack.GetCardBack(0));
        }
        catch (System.ArgumentOutOfRangeException) {
            render.renderedCard = null;
        }
    }

    public void GenerateDeck(bool redDeck, bool fullDeck) {
        if (redDeck) {
            if (fullDeck) {
                cardStack.GenerateDeck54(DeckColor.Red);
            }

            else {
                cardStack.GenerateDeck52(DeckColor.Red);
            }
        }

        else {
            if (fullDeck) {
                cardStack.GenerateDeck54(DeckColor.Blue);
            }

            else {
                cardStack.GenerateDeck52(DeckColor.Blue);
            }
        }
    }

    override
    public Card GetCard(Vector2 point) {
        return cardStack.TakeTopCard();
    }

    override
    public void GiveCard(Card card) {
        cardStack.AddCardToTop(card);
    }


}