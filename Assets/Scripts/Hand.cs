using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Interactable {

    public GameObject HandCard;
    public float handWidth = 3f;
    public float maxRotation = 60f;
    public float rotationOffset = 0f;
    public Vector3 anchor = Vector3.zero;

    List<RenderCard> renderedCards;


    // Start is called before the first frame update
    void Start() {
        cardStack = new CardStack(true);
        renderedCards = new List<RenderCard>();
        cardStack.AddCardToTop(new Card(Suit.Clubs, Rank.Eight, true));
        cardStack.AddCardToTop(new Card(Suit.Hearts, Rank.Ace, true));
        cardStack.AddCardToTop(new Card(Suit.Spades, Rank.Seven, false));
        cardStack.AddCardToTop(new Card(Suit.Diamonds, Rank.Nine, true));
        cardStack.AddCardToTop(new Card(Suit.Clubs, Rank.Eight, false));
    }

    // Update is called once per frame
    void Update() {
        SetSizeOfRenderedHand();
        RenderEachCardInHand();
        FanOut();
    }

    private void SetSizeOfRenderedHand() {
        while (renderedCards.Count < cardStack.NumberOfCards()) {
            //instantiate prefabs
            GameObject newCard = Instantiate(HandCard, transform.position, Quaternion.identity);
            renderedCards.Add(newCard.GetComponent<RenderCard>());
            newCard.transform.parent = transform;
        }
        while (renderedCards.Count > cardStack.NumberOfCards()) {
            //delete prefabs
            GameObject objectToDelete = renderedCards[0].gameObject;
            renderedCards.RemoveAt(0);
            Destroy(objectToDelete);
        }
    }

    private void RenderEachCardInHand() {
        for (int i = 0; i < renderedCards.Count; i++) {
            renderedCards[i].renderedCard = new Card(cardStack.GetCardSuit(i),
                cardStack.GetCardRank(i), cardStack.GetCardBack(i));
            renderedCards[i].orderInLayer = -i;
        }
    }

    private void FanOut() {
        for(int i = 0; i < renderedCards.Count; i++) {
            //rotation
            renderedCards[i].transform.eulerAngles = new Vector3(0, 0, (maxRotation / 2) -
                i * (maxRotation / (renderedCards.Count - 1)));

            //position
            //start at anchor
            renderedCards[i].transform.localPosition = anchor;

            //
            renderedCards[i].transform.localPosition +=
                handWidth * renderedCards[i].transform.up;

            //offset
            renderedCards[i].transform.eulerAngles = new Vector3(0, 0,
                renderedCards[i].transform.eulerAngles.z + rotationOffset);
        }
    }
}
