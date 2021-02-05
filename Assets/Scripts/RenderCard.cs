using UnityEngine;

public class RenderCard : MonoBehaviour {
    CardSpriteArray spriteArray;

    [HideInInspector]
    public Card renderedCard;


    public int orderInLayer;
    public bool isFlipped;


    private SpriteRenderer suitRenderer;
    private SpriteRenderer rankRenderer;
    private SpriteRenderer colorRenderer;
    private SpriteRenderer cardRenderer;

    void Start() {
        spriteArray = GameManager.gm.spriteArray;
        suitRenderer = transform.Find("Suit").GetComponent<SpriteRenderer>();
        rankRenderer = transform.Find("Rank").GetComponent<SpriteRenderer>();
        colorRenderer = transform.Find("Color").GetComponent<SpriteRenderer>();
        cardRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        SetCardLayering();
        UpdateCardRenderer();
    }

    public void FlipCard() {
        isFlipped = !isFlipped;
        UpdateCardRenderer();
    }

    private void SetCardLayering() {
        cardRenderer.sortingOrder = orderInLayer * 4;
        suitRenderer.sortingOrder = orderInLayer * 4 + 1;
        colorRenderer.sortingOrder = orderInLayer * 4 + 2;
        rankRenderer.sortingOrder = orderInLayer * 4 + 3;
    }

    private void UpdateCardRenderer() {
        //handles null values
        Card topCard;
        try {
            topCard = new Card(renderedCard.Suit, renderedCard.Rank);
        }
        catch (System.NullReferenceException) {
            topCard = null;
        }

        if (topCard == null) {
            cardRenderer.sprite = null;
            suitRenderer.sprite = null;
            rankRenderer.sprite = null;
            colorRenderer.sprite = null;
        }
        else if (!isFlipped) {
            cardRenderer.sprite = spriteArray.Card;
            suitRenderer.sprite = spriteArray.GetSuitSprite(topCard);
            rankRenderer.sprite = spriteArray.GetRankSprite(topCard);
            colorRenderer.sprite = spriteArray.GetColorSprite(topCard);
        }
        else {
            cardRenderer.sprite = spriteArray.Back;
            suitRenderer.sprite = null;
            rankRenderer.sprite = null;
            colorRenderer.sprite = null;
        }
    }
}