using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public CardStack cardStack;

    public abstract Card GetCard(Vector2 point);
    public abstract void GiveCard(Card card);
}
