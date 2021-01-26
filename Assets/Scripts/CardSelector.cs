using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(RenderCard))]
public class CardSelector : MonoBehaviour {
    RenderCard render;
    CardStack storeCard;

    // Start is called before the first frame update
    void Start() {
        render = gameObject.GetComponent<RenderCard>();
        storeCard = new CardStack(true);
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Deck selectedDeck = GetSelectedDeck();

            if(storeCard.NumberOfCards() == 0) {
                storeCard.AddCardToTop(selectedDeck.cardStack.TakeTopCard());
            }
            else {
                selectedDeck.cardStack.AddCardToTop(storeCard.TakeTopCard());
            }
        }


        try {
            render.renderedCard = new Card(storeCard.GetCardSuit(0), storeCard.GetCardRank(0));
        }
        catch(ArgumentOutOfRangeException){
            render.renderedCard = null;
        }
    }

    private Deck GetSelectedDeck() {
        Deck result = null;
        float minDistanceToDeck = -1f;

        foreach(Deck d in GameManager.gm.interactableDecks) {
            float distanceToDeck = (d.gameObject.transform.position - transform.position).magnitude;
            if(result == null) {
                result = d;
                minDistanceToDeck = distanceToDeck;
            }
            else if(distanceToDeck < minDistanceToDeck) {
                result = d;
                minDistanceToDeck = distanceToDeck;
            }
        }

        return result;
    }

    
}
