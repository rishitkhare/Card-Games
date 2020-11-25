using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCard : MonoBehaviour {
    public Suit SuitOfRenderedCard;
    public Rank RankOfRenderedCard;
    public int orderInLayer;

    private SpriteRenderer suitRenderer;
    private SpriteRenderer rankRenderer;
    private SpriteRenderer cardRenderer;

    void Start() {
        suitRenderer = transform.Find("Suit").GetComponent<SpriteRenderer>();
        rankRenderer = transform.Find("Rank").GetComponent<SpriteRenderer>();
        cardRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetCardRenderer(Suit suit, Rank rank) {
        SuitOfRenderedCard = suit;
        RankOfRenderedCard = rank;
    }

    private void UpdateCardRenderer() {

    }
}
