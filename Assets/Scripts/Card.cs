using System;

public enum Suit {
    Spades = 0,
    Clubs = 1,
    Hearts = 2,
    Diamonds = 3
}
public enum Rank {
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

    public Rank Rank{get; }

    public Card(Suit suit, Rank rank) {
        Suit = suit;
        Rank = rank;
    }

    override
    public string ToString() {
        if(rankAsInt <= 10 && rankAsInt != 1) {
            return rankAsInt + " of " + Suit;
        }
        //else
        return Rank + " of " + Suit;
    }
    
    public int RankCompareTo(Card other) {
        return rankAsInt - other.rankAsInt;
    }

    public int SuitCompareTo(Card other) {
        return (int)Suit - (int)other.Suit;
    }
}