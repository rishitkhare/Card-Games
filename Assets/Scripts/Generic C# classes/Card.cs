using System;

public enum Suit {
    RedJoker = -1,
    BlackJoker = 0,
    Spades = 1,
    Clubs = 2,
    Hearts = 3,
    Diamonds = 4
}
public enum Rank {
    Joker = 0,
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
    Ace = 1
}
public class Card {

    public Suit Suit { get; }
    public int rankAsInt { get { return (int) Rank; } }

    public Rank Rank{ get; }

    public bool isRed { get; }

    public Card(Suit suit, Rank rank, bool back) {
        Suit = suit;
        Rank = rank;
        if((suit <= 0 && rank != Rank.Joker)
            || (suit > 0 && rank == Rank.Joker)) {
            throw new ArgumentException();
        }
        isRed = back;
    }

    override
    public string ToString() {
        if(Rank != Rank.Joker) {
            if(rankAsInt <= 10 && rankAsInt != 1) {
                return rankAsInt + " of " + Suit;
            }
            //else
            return Rank + " of " + Suit;
        }
        else {
            return "Joker";
        }
    }
    
    public int RankCompareTo(Card other) {
        return rankAsInt - other.rankAsInt;
    }

    public int SuitCompareTo(Card other) {
        return (int)Suit - (int)other.Suit;
    }
}