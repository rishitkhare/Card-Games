using System;
using System.Collections.Generic;
using UnityEngine;

public class CardStack {

    #region Fields and Constructor

    private List<Card> cards;
    public bool IsFaceUp { get; set; }

    public CardStack(bool isFaceUp) {
        IsFaceUp = isFaceUp;
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

    public int GetCardNumber(int index) {
        return (int) cards[index].Rank;
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

    public void GenerateDeck52() {
        foreach(Suit suit in Enum.GetValues(typeof(Suit))) {
            foreach (Rank rank in Enum.GetValues(typeof(Rank))) {
                AddCardToTop(new Card(suit, rank));
            }
        }
    }

    //TODO: Find built-in shuffle method
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

    public void AddCardToTop(Card newCard) {
        cards.Insert(0, newCard);
    }

    public void AddCardToBottom(Card newCard) {
        cards.Add(newCard);
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