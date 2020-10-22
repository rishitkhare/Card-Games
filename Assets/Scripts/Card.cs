public enum Suit {
    Spades,
    Clubs,
    Hearts,
    Diamonds
}
public enum SpecialNumber {
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 0
}
public class Card {

    public int suit{get; set; }
    public int number{get; set; }

    public Card(int suit, int number) {
        this.suit = suit;
        this.number = number;
    }

    public int CompareTo(Card other)
    {
        return this.number - other.number;
    }
}