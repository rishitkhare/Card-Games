using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Rendering;

namespace Framework
{
    [RequireComponent(typeof(RenderCard))]
    public class CardStack
    {

        #region Fields and Constructor

        protected List<Card> cards;
        public bool IsFaceUp { get; set; }

        public CardStack(bool isFaceUp)
        {
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
        public string ToString()
        {
            string answer = cards.Count.ToString() + " Cards:\n";

            for (int i = 0; i < cards.Count; i++)
            {
                answer += cards[i] + "\n";
            }

            return answer;
        }

        public Suit GetCardSuit(int index) => cards[index].Suit;
        public Rank GetCardRank(int index) => cards[index].Rank;
        public int GetCardNumber(int index) => cards[index].rankAsInt;
        public DeckColor GetCardBack(int index) => cards[index].DeckColor;

        //cards are removed from deck when taken.
        public Card TakeCardAt(int index)
        {
            try
            {
                Card myCard = cards[index];
                cards.RemoveAt(index);

                return myCard;
            }
            catch (ArgumentOutOfRangeException)
            {
                //empty deck returns null.
                return null;
            }
        }

        public int TotalWorth() => cards.Sum(c => c.rankAsInt);
        public int TotalWorthTunk() => cards.Sum(c => Math.Min(c.rankAsInt, (int)Rank.Ten));
        public int TotalWorthCrazyEights() => cards.Sum(c =>
            (c.Rank == Rank.Two || c.Rank == Rank.Jack || c.Rank == Rank.Queen || c.Rank == Rank.King) ?
                10 :
            (c.Rank == Rank.Eight || c.Rank == Rank.Joker) ?
                50 :
            c.rankAsInt);

        public int NumberOfCards() => cards.Count;
        public Card TakeTopCard() => TakeCardAt(0);
        public Card TakeBottomCard() => TakeCardAt(cards.Count - 1);

        #endregion


        #region Mutators

        public void GenerateDeck52(DeckColor deckColor)
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    if (suit > Suit.None && suit < Suit.RedJoker && rank != Rank.Joker)
                    {
                        AddCardToTop(new Card(suit, rank, deckColor));
                    }
                }
            }

            //      BRUH PLS NO

            //      ((IEnumerable<Suit>)Enum.GetValues(typeof(Suit))).Where(suit => suit > Suit.None && suit < Suit.RedJoker).ToList()
            //          .ForEach(suit => ((IEnumerable<Rank>)Enum.GetValues(typeof(Rank))).Where(rank => rank != Rank.Joker).ToList()
            //          .ForEach(rank => AddCardToTop(new Card(suit, rank, deckColor))));
        }

        public void GenerateDeck54(DeckColor deckColor)
        {
            GenerateDeck52(deckColor);
            AddCardToTop(new Card(Suit.RedJoker, Rank.Joker, deckColor));
            AddCardToTop(new Card(Suit.BlackJoker, Rank.Joker, deckColor));
        }

        public void Shuffle()
        {
            System.Random rand = new System.Random();
            List<Card> newCards = new List<Card>();

            while (cards.Count != 0)
            {
                int index = rand.Next(cards.Count);
                newCards.Add(cards[index]);
                cards.RemoveAt(index);
            }

            cards = newCards;
        }

        public void AddCardAt(int index, Card newCard) => cards.Insert(index, newCard);
        public void AddCardToTop(Card newCard) => cards.Insert(0, newCard);
        public void AddCardToBottom(Card newCard) => cards.Add(newCard);
        public void ClearCardStack() => cards.Clear();

        #endregion


        #region MergeSort Algorithm

        public void SortByRank()
        {
            cards = SortStackByRank(cards);
        }

        // (merge) sort by number OR suit and number
        private List<Card> SortStackByRank(List<Card> stack)
        {
            //base case
            if (stack.Count <= 1)
            {
                return stack;
            }

            //split into two lists
            List<Card> left = new List<Card>();
            List<Card> right = new List<Card>();

            for (int i = 0; i < stack.Count / 2; i++)
            {
                left.Add(stack[i]);
            }
            for (int i = stack.Count / 2; i < stack.Count; i++)
            {
                right.Add(stack[i]);
            }

            left = SortStackByRank(left);
            right = SortStackByRank(right);

            return MergeStackRank(left, right);
        }

        public List<Card> MergeStackRank(List<Card> left, List<Card> right)
        {
            int leftParser = 0;
            int rightParser = 0;
            List<Card> sorted = new List<Card>();

            while (leftParser < left.Count && rightParser < right.Count)
            {
                if (left[leftParser].RankCompareTo(right[rightParser]) <= 0)
                {
                    sorted.Add(left[leftParser]);
                    leftParser++;
                }
                else
                {
                    sorted.Add(right[rightParser]);
                    rightParser++;
                }
            }

            while (leftParser < left.Count)
            {
                sorted.Add(left[leftParser]);
                leftParser++;
            }
            while (rightParser < right.Count)
            {
                sorted.Add(right[rightParser]);
                rightParser++;
            }

            return sorted;
        }

        public List<Card> SortStackBySuitAndRank(List<Card> cards)
        {
            return null;
        }

        #endregion
    }
}