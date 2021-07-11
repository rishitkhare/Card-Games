using System;
namespace Framework
{
    [Flags]
    public enum Suit
    {
        None = 0,
        Spades = 1,
        Clubs = 1 << 1,
        Hearts = 1 << 2,
        Diamonds = 1 << 3,
        RedJoker = 1 << 4,
        BlackJoker = 1 << 5,
    }

    [Flags]
    public enum SuitCombo
    {
        Blacks = Suit.Spades | Suit.Clubs,
        AllBlacks = Blacks | Suit.BlackJoker,
        Reds = Suit.Hearts | Suit.Diamonds,
        AllReds = Reds | Suit.RedJoker,
        Jokers = Suit.RedJoker | Suit.BlackJoker
    }

    public enum DeckColor
    {
        Red,
        Blue
    }

    [Flags]
    public enum Rank
    {
        Joker = 0,
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }
    public class Card
    {

        public Suit Suit { get; }
        public int rankAsInt { get => (int)Rank; }

        public Rank Rank { get; }

        public DeckColor DeckColor { get; }

        public Card(Suit suit, Rank rank, DeckColor back)
        {
            Suit = suit;
            Rank = rank;
            double power = Math.Log((int)Suit, 2);

            if (Suit <= 0 || Suit > Suit.BlackJoker || (int)Math.Ceiling(power) != (int)Math.Floor(power) || //checks if suit is not power of 2
               rank < 0 || rank > Rank.King)
            {
                throw new ArgumentException();
            }

            DeckColor = back;
        }

        public Card(Suit suit, Rank rank) : this(suit, rank, DeckColor.Red) { }

        override
        public string ToString()
        {
            if (Rank != Rank.Joker)
            {
                return ((Rank <= Rank.Ten && Rank != Rank.Ace) ? "" + rankAsInt : "" + Rank) + " of " + Suit;
            }

            return "Joker";
        }

        public int RankCompareTo(Card other) => rankAsInt - other.rankAsInt;
        public int SuitCompareTo(Card other) => (int)Suit - (int)other.Suit;
    }
}