using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public CardStack cardStack;
    public bool lockPickup = false;
    public bool lockPlace = false;

    public abstract Card GetCard(Vector2 point);
    public Card GetCard() {
        return cardStack.TakeTopCard();
    }
    public abstract void GiveCard(Card card);
}
