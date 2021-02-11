using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Interactable {

    public GameObject HandCard;
    public float handWidth = 3f;
    public float selectedHandWidth = 4f;
    public float timeToSelectAnimation = 0.4f;
    public float maxRotation = 60f;
    public float rotationOffset = 0f;
    public float clerpRate = 0.3f;
    public Vector3 anchor = Vector3.zero;

    List<RenderCard> renderedCards;
    List<Collider2D> cardColliders;
    List<FanOut> cardFanOuts;

    // Start is called before the first frame update
    void Start() {
        cardStack = new CardStack(true);
        renderedCards = new List<RenderCard>();
        cardColliders = new List<Collider2D>();
        cardFanOuts = new List<FanOut>();
        cardStack.AddCardToTop(new Card(Suit.Clubs, Rank.Eight));
        cardStack.AddCardToTop(new Card(Suit.Hearts, Rank.Ace));
        cardStack.AddCardToTop(new Card(Suit.Spades, Rank.Seven, DeckColor.Blue));
        cardStack.AddCardToTop(new Card(Suit.Diamonds, Rank.Nine));
        cardStack.AddCardToTop(new Card(Suit.Clubs, Rank.Ten, DeckColor.Blue));
        cardStack.AddCardToBottom(new Card(Suit.Spades, Rank.Ace));
    }

    // Update is called once per frame
    void Update() {
        SetSizeOfRenderedHand();
        RenderEachCardInHand();
        IndexCards();
    }

    override
    public Card GetCard(Vector2 mousePosition) {
        //return cardStack.TakeTopCard();
        for (int i = 0; i < renderedCards.Count; i++) {
            if (cardColliders[i].OverlapPoint(mousePosition)) {

                Card taken_Card = cardStack.TakeCardAt(i);

                GameObject objectToDelete = renderedCards[i].gameObject;
                renderedCards.RemoveAt(i);
                cardColliders.RemoveAt(i);
                cardFanOuts.RemoveAt(i);

                Destroy(objectToDelete);

                return taken_Card;
            }
        }

        Debug.Log("no card clicked, top one taken");
        return cardStack.TakeTopCard();
    }
    override
    public void GiveCard(Card card) {
        cardStack.AddCardToBottom(card);
    }


# region accessors
    public int GetNumberOfCardsInHand() {
        return renderedCards.Count;
    }

    public List<Collider2D> GetCardColliders() {
        return cardColliders;
    }
#endregion

    private void SetSizeOfRenderedHand() {
        while (renderedCards.Count < cardStack.NumberOfCards()) {
            //instantiate prefabs
            GameObject newCard = Instantiate(HandCard, transform.position, Quaternion.identity);
            renderedCards.Add(newCard.GetComponent<RenderCard>());
            cardColliders.Add(newCard.GetComponent<Collider2D>());
            cardFanOuts.Add(newCard.GetComponent<FanOut>());
            newCard.transform.parent = transform;
        }
        //while (renderedCards.Count > cardStack.NumberOfCards()) {
        //    //delete prefabs
        //    //TODO : find correct index
        //    GameObject objectToDelete = renderedCards[0].gameObject;
        //    renderedCards.RemoveAt(0);
        //    cardColliders.RemoveAt(0);
        //    cardFanOuts.RemoveAt(0);
        //    Debug.Log("removed with failsafe");

        //    Destroy(objectToDelete);
        //}
    }

    private void RenderEachCardInHand() {
        for (int i = 0; i < renderedCards.Count; i++) {
            renderedCards[i].renderedCard = new Card(cardStack.GetCardSuit(i),
                cardStack.GetCardRank(i), cardStack.GetCardBack(i));


            renderedCards[i].orderInLayer = -i;
        }
    }

    private void IndexCards() {
        for(int i = 0; i < cardFanOuts.Count; i++) {
            cardFanOuts[i].fanIndex = i;
        }
    }
}
