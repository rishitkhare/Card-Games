using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack {

    #region Fields and Constructor

    private List<Card> cards;
    private bool isFaceUp;

    public CardStack(bool isFaceUp) {
        this.isFaceUp = isFaceUp;
        cards = new List<Card>();
    }

    #endregion fields



    #region Accessors

    /*
     * NOTE: "GetCard(int index)" method should not exist
     * in order to prevent accidental duplication; i.e. if the method 
     * a.AddCardToTop(b.GetCard(0));
     * was used for CardStacks a & b, the top card from b would
     * be duplicated and added to stack a.
    */

    public int GetCardSuit(int index) {
        return cards[index].Suit;
    }

    public int GetCardNumber(int index) {
        return cards[index].Number;
    }

    //cards are removed from deck when taken
    public Card TakeCardAt(int index) {
        Card myCard = cards[index];
        cards.RemoveAt(index);

        return myCard;
    }

    public Card TakeTopCard() { return TakeCardAt(0); }
    public Card TakeBottomCard() { return TakeCardAt(cards.Count - 1); }

    #endregion



    #region Mutators

    public void AddCardToTop(Card newCard) {
        cards.Insert(0, newCard);
    }

    public void AddCardToBottom(Card newCard) {
        cards.Add(newCard);
    }

    #endregion
}
