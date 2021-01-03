using UnityEngine;

[CreateAssetMenu(menuName = "Card Sprite Array")]
public class CardSpriteArray : ScriptableObject {
    public Sprite Spades;
    public Sprite Clubs;
    public Sprite Hearts;
    public Sprite Diamonds;

    public Sprite Two;
    public Sprite Three;
    public Sprite Four;
    public Sprite Five;
    public Sprite Six;
    public Sprite Seven;
    public Sprite Eight;
    public Sprite Nine;
    public Sprite Ten;
    public Sprite Jack;
    public Sprite Queen;
    public Sprite King;
    public Sprite Ace;

    public Sprite GetSprite(Rank rank) {
        switch (rank) {
            case (Rank.Two):
                return Two;
            case (Rank.Three):
                return Three;
            case (Rank.Four):
                return Four;
            case (Rank.Five):
                return Three;
            case (Rank.Six):
                return Six;
            case (Rank.Seven):
                return Seven;
            case (Rank.Eight):
                return Eight;
            case (Rank.Nine):
                return Nine;
            case (Rank.Ten):
                return Ten;
            case (Rank.Jack):
                return Jack;
            case (Rank.Queen):
                return Queen;
            case (Rank.King):
                return King;
            case (Rank.Ace):
                return Ace;
            default:
                //this will never occur unless we add a new Rank
                return null;
        }
    }

    public Sprite GetSprite(Suit suit) {
        switch (suit) {
            case (Suit.Spades):
                return Spades;
            case (Suit.Clubs):
                return Clubs;
            case (Suit.Diamonds):
                return Diamonds;
            case (Suit.Hearts):
                return Hearts;
            default:
                //this will never occur unless we add a new Suit
                return null;
        }
    }
}
