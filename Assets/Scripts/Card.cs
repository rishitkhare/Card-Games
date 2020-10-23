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
    Jack,
    Queen,
    King,
    Ace
}
public class Card {

    public Suit Suit{get; set; }
    public Rank Rank{get; set; }

    public Card(Suit suit, Rank rank) {
        Suit = suit;
        Rank = rank;
    }

    override
    public string ToString() {
        if((int) Rank <= 10) {
            return (int) Rank + " of " + Suit;
        }
        //else
        return Rank + " of " + Suit;
    }
    
    public int RankCompareTo(Card other) {
        return (int) Rank - (int) other.Rank;
    }

    public int SuitCompareTo(Card other) {
        return (int)Suit - (int)other.Suit;
    }
}