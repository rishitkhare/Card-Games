public enum Suit {
    Spades,
    Clubs,
    Hearts,
    Diamonds
}
public enum SpecialNumber {
    King = 11,
    Queen = 12,
    Jack = 13,
    Ace = 0
}
public class Card {

    public int Suit{get; set; }
    public int Number{get; set; }

    public Card(int suit, int number) {
        Suit = suit;
        Number = number;
    }
}