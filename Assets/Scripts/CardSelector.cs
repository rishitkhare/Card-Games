using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(RenderCard))]
public class CardSelector : MonoBehaviour {
    RenderCard render;
    CardStack storeCard;
    Camera cam;

    // Start is called before the first frame update
    void Start() {
        render = gameObject.GetComponent<RenderCard>();
        storeCard = new CardStack(true);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Interactable selectedDeck = GetSelectedInteractable();

            if(storeCard.NumberOfCards() == 0) {
                storeCard.AddCardToTop(selectedDeck.GetCard(cam.ScreenToWorldPoint(Input.mousePosition)));
            }
            else {
                selectedDeck.GiveCard(storeCard.TakeTopCard());
            }
        }


        try {
            render.renderedCard = new Card(storeCard.GetCardSuit(0), storeCard.GetCardRank(0), storeCard.GetCardBack(0));
        }
        catch(ArgumentOutOfRangeException){
            render.renderedCard = null;
        }
    }

    private Interactable GetSelectedInteractable() {
        Interactable result = null;
        float minDistanceToDeck = -1f;

        foreach(Interactable d in GameManager.gm.interactableDecks) {
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
