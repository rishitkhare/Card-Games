using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCard : MonoBehaviour {
    public CardSpriteArray spriteArray;
    public Card renderedCard;
    public int orderInLayer;
    public bool isFlipped; //TODO: make this


    private SpriteRenderer suitRenderer;
    private SpriteRenderer rankRenderer;
    private SpriteRenderer cardRenderer;

    void Start() {
        suitRenderer = transform.Find("Suit").GetComponent<SpriteRenderer>();
        rankRenderer = transform.Find("Rank").GetComponent<SpriteRenderer>();
        cardRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        //cardRenderer.sprite =
        suitRenderer.sprite = spriteArray.GetSprite(renderedCard.Suit);
        rankRenderer.sprite = spriteArray.GetSprite(renderedCard.Rank);
    }

    private void UpdateCardRenderer() {

    }
}
