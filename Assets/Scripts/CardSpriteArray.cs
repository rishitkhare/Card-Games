using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Card Sprite Array")]
public class CardSpriteArray : ScriptableObject {
    public bool seperateColorSprite;

    public Sprite Card;
    public Sprite Back;

    public Sprite Red;
    public Sprite Black;
    public Sprite JokerRedSprite;
    public Sprite JokerBlackSprite;

    public Sprite Spades;
    public Sprite Clubs;
    public Sprite Hearts;
    public Sprite Diamonds;
    public Sprite JokerSuit;

    //Item1 = black, Item2 = red (uses black by default)
    public Sprite[] Two;
    public Sprite[] Three;
    public Sprite[] Four;
    public Sprite[] Five;
    public Sprite[] Six;
    public Sprite[] Seven;
    public Sprite[] Eight;
    public Sprite[] Nine;
    public Sprite[] Ten;
    public Sprite[] Jack;
    public Sprite[] Queen;
    public Sprite[] King;
    public Sprite[] Ace;
    public Sprite[] JokerRank;

    public Sprite GetRankSprite(Card card) {
        if(card == null) {
            return null;
        }
        if (seperateColorSprite || card.Suit == Suit.Spades || card.Suit == Suit.Clubs) {
            switch (card.Rank) {
                case (Rank.Two):
                    return Two[0];
                case (Rank.Three):
                    return Three[0];
                case (Rank.Four):
                    return Four[0];
                case (Rank.Five):
                    return Five[0];
                case (Rank.Six):
                    return Six[0];
                case (Rank.Seven):
                    return Seven[0];
                case (Rank.Eight):
                    return Eight[0];
                case (Rank.Nine):
                    return Nine[0];
                case (Rank.Ten):
                    return Ten[0];
                case (Rank.Jack):
                    return Jack[0];
                case (Rank.Queen):
                    return Queen[0];
                case (Rank.King):
                    return King[0];
                case (Rank.Ace):
                    return Ace[0];
                case (Rank.Joker):
                    return JokerRank[0];
                default:
                    //this will never occur unless we add a new Rank
                    return null;
            }
        }
        else {
            switch (card.Rank) {
                case (Rank.Two):
                    return Two[1];
                case (Rank.Three):
                    return Three[1];
                case (Rank.Four):
                    return Four[1];
                case (Rank.Five):
                    return Five[1];
                case (Rank.Six):
                    return Six[1];
                case (Rank.Seven):
                    return Seven[1];
                case (Rank.Eight):
                    return Eight[1];
                case (Rank.Nine):
                    return Nine[1];
                case (Rank.Ten):
                    return Ten[1];
                case (Rank.Jack):
                    return Jack[1];
                case (Rank.Queen):
                    return Queen[1];
                case (Rank.King):
                    return King[1];
                case (Rank.Ace):
                    return Ace[1];
                case (Rank.Joker):
                    return JokerRank[1];
                default:
                    //this will never occur unless we add a new Rank
                    return null;
            }
        }
    }

    public Sprite GetSuitSprite(Card card) {
        if (card == null) {
            return null;
        }
        switch(card.Suit) {
            case (Suit.Clubs):
                return Clubs;
            case (Suit.Diamonds):
                return Diamonds;
            case (Suit.Hearts):
                return Hearts;
            case (Suit.Spades):
                return Spades;
            case (Suit.RedJoker):
                return JokerSuit;
            case (Suit.BlackJoker):
                return JokerSuit;
            default:
                return null;
        }
    }

    public Sprite GetColorSprite(Card card) {
        if(seperateColorSprite && card != null) {
            if(card.Suit == Suit.RedJoker) {
                return JokerRedSprite;
            }
            else if(card.Suit == Suit.BlackJoker) {
                return JokerBlackSprite;
            }
            else if (card.Suit == Suit.Spades || card.Suit == Suit.Clubs) {
                return Black;
            }
            else {
                return Red;
            }
        }
        else {
            return null;
        }

    }
}
