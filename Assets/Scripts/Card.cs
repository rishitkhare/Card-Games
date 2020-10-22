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

    public int Suit{get; set; }
    public int Number{get; set; }

    public Card(int suit, int number) {
        Suit = suit;
        Number = number;
    }

    public int CompareTo(Card other)
    {
        return this.number - other.number;
    }
}