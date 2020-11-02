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
    public int Rank { get { return (int) rank; } }

    private Rank rank;

    public Card(Suit suit, Rank rank) {
        Suit = suit;
        this.rank = rank;
    }

    override
    public string ToString() {
        if(Rank <= 10 && Rank != 1) {
            return Rank + " of " + Suit;
        }
        //else
        return rank + " of " + Suit;
    }
    
    public int RankCompareTo(Card other) {
        return Rank - other.Rank;
    }

    public int SuitCompareTo(Card other) {
        return (int)Suit - (int)other.Suit;
    }
}