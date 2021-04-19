using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RenderCard))]
public class CardStack {

    #region Fields and Constructor

    protected List<Card> cards;
    public bool IsFaceUp { get; set; }

    public CardStack(bool isFaceUp) {
        IsFaceUp = isFaceUp;
        cards = new List<Card>();
    }


    #endregion Fields and Constructor


    #region Accessors

    /*
     * NOTE: "GetCard(int index)" method should not exist
     * in order to prevent accidental duplication; i.e. if the method
     * 
     * a.AddCardToTop(b.GetCard(0));
     * 
     * was called where a and b are of type CardStack, the top card from b
     * would be duplicated and added to stack a.
    */
    override
    public string ToString() {
        string answer = cards.Count.ToString() + " Cards:\n";

        for(int i = 0; i < cards.Count; i++) {
            answer += cards[i] + "\n";
        }

        return answer;
    }

    public Suit GetCardSuit(int index) {
        return cards[index].Suit;
    }

    public Rank GetCardRank(int index) {
        return cards[index].Rank;
    }

    public int GetCardNumber(int index) {
        return cards[index].rankAsInt;
    }

    public DeckColor GetCardBack(int index) {
        return cards[index].DeckColor;
    }

    //cards are removed from deck when taken.
    public Card TakeCardAt(int index) {
        try {
            Card myCard = cards[index];
            cards.RemoveAt(index);

            return myCard;
        }
        catch(ArgumentOutOfRangeException) {
            //empty deck returns null.
            return null;
        }
    }

    public int TotalWorth() {
        int total = 0;

        foreach (Card card in cards) {
            total += card.rankAsInt;
        }

        return total;
    }

    public int TotalWorthTunk() {
        int total = 0;

        foreach (Card card in cards) {
            total += (card.rankAsInt < 10) ? card.rankAsInt : 10;
        }

        return total;
    }

    public int TotalWorthCrazyEights() {
        int total = 0;
        foreach (Card card in cards) {
            if (card.Rank == (Rank.Two | Rank.Jack | Rank.Queen | Rank.King)) {
                total += 10;
            }

            else if (card.Rank == Rank.Eight || card.Suit == (Suit.RedJoker | Suit.BlackJoker)) {
                total += 50;
            }

            else {
                total += card.rankAsInt;
            }
        }

        return total;
    }

    public int NumberOfCards() { return cards.Count; }

    public Card TakeTopCard() { return TakeCardAt(0); }
    public Card TakeBottomCard() { return TakeCardAt(cards.Count - 1); }

    #endregion


    #region Mutators

    public void GenerateDeck52(DeckColor deckColor) {
        foreach(Suit suit in Enum.GetValues(typeof(Suit))) {
            foreach (Rank rank in Enum.GetValues(typeof(Rank))) {
                // suit > 0 means card is not joker
                if(suit > 0 && rank != Rank.Joker) {
                    AddCardToTop(new Card(suit, rank, deckColor));
                }
            }
        }

    }

    public void GenerateDeck54(DeckColor deckColor) {
        GenerateDeck52(deckColor);
        AddCardToTop(new Card(Suit.RedJoker, Rank.Joker, deckColor));
        AddCardToTop(new Card(Suit.BlackJoker, Rank.Joker, deckColor));
    }

    public void Shuffle() {
        System.Random rand = new System.Random();
        List<Card> newCards = new List<Card>();

        while(cards.Count != 0) {
            int index = rand.Next(cards.Count);
            newCards.Add(cards[index]);
            cards.RemoveAt(index);
        }

        cards = newCards;
        
    }

    public void AddCardAt(int index, Card newCard) {
        cards.Insert(index, newCard);
    }

    public void AddCardToTop(Card newCard) {
        cards.Insert(0, newCard);
    }

    public void AddCardToBottom(Card newCard) {
        cards.Add(newCard);
    }

    public void ClearCardStack() {
        cards.Clear();
    }

    #endregion


    #region MergeSort Algorithm

    public void SortByRank() {
        cards = SortStackByRank(cards);
    }

    // (merge) sort by number OR suit and number
    private List<Card> SortStackByRank(List<Card> stack) {
        //base case
        if(stack.Count <= 1) {
            return stack;
        }

        //split into two lists
        List<Card> left = new List<Card>();
        List<Card> right = new List<Card>();

        for(int i = 0; i < stack.Count / 2; i ++) {
            left.Add(stack[i]);
        }
        for(int i = stack.Count / 2; i < stack.Count; i ++) {
            right.Add(stack[i]);
        }

        left = SortStackByRank(left);
        right = SortStackByRank(right);

        return MergeStackRank(left, right);
    }

    public List<Card> MergeStackRank(List<Card> left, List<Card> right) {
        int leftParser = 0;
        int rightParser = 0;
        List<Card> sorted = new List<Card>();

        while (leftParser < left.Count && rightParser < right.Count) {
            if(left[leftParser].RankCompareTo(right[rightParser]) <= 0) {
                sorted.Add(left[leftParser]);
                leftParser++;
            }
            else {
                sorted.Add(right[rightParser]);
                rightParser++;
            }
        }

        while (leftParser < left.Count) {
            sorted.Add(left[leftParser]);
            leftParser++;
        }
        while (rightParser < right.Count) {
            sorted.Add(right[rightParser]);
            rightParser++;
        }

        return sorted;
    }

    public List<Card> SortStackBySuitAndRank(List<Card> cards) {
        return null;
    }

    #endregion
}