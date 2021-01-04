using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCard : MonoBehaviour {
    public CardSpriteArray spriteArray;
    public Card renderedCard;
    public int orderInLayer;
    public bool isFlipped;


    private SpriteRenderer suitRenderer;
    private SpriteRenderer rankRenderer;
    private SpriteRenderer colorRenderer;
    private SpriteRenderer cardRenderer;

    void Start() {
        suitRenderer = transform.Find("Suit").GetComponent<SpriteRenderer>();
        rankRenderer = transform.Find("Rank").GetComponent<SpriteRenderer>();
        colorRenderer = transform.Find("Color").GetComponent<SpriteRenderer>();
        cardRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if(!isFlipped) {
            cardRenderer.sprite = spriteArray.Card;
            suitRenderer.sprite = spriteArray.GetSuitSprite(renderedCard);
            rankRenderer.sprite = spriteArray.GetRankSprite(renderedCard);
            colorRenderer.sprite = spriteArray.GetColorSprite(renderedCard);
        }
        else {
            cardRenderer.sprite = spriteArray.Back;
            suitRenderer.sprite = null;
            rankRenderer.sprite = null;
            colorRenderer.sprite = null;
        }
    }

    private void UpdateCardRenderer() {

    }
}
