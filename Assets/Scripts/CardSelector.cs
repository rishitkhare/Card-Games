using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(RenderCard))]
public class CardSelector : MonoBehaviour {
    RenderCard render;
    CardStack storeCard;
    Camera cam;

    public float flipRate = 0.3f;

    float currentRotation = 0f;

    // Start is called before the first frame update
    void Start() {
        render = gameObject.GetComponent<RenderCard>();
        storeCard = new CardStack(true);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update() {
        Interactable selectedDeck = GetSelectedInteractable();
        if (Input.GetMouseButtonDown(0)) {

            if(storeCard.NumberOfCards() == 0) {
                storeCard.AddCardToTop(selectedDeck.GetCard(cam.ScreenToWorldPoint(Input.mousePosition)));
            }
            else {
                selectedDeck.GiveCard(storeCard.TakeTopCard());
            }
        }


        if(storeCard.NumberOfCards() > 0) {
            render.renderedCard = new Card(storeCard.GetCardSuit(0), storeCard.GetCardRank(0), storeCard.GetCardBack(0));
        }
        else {
            render.renderedCard = null;
        }

        FlipCard(selectedDeck);
    }

    private void FlipCard(Interactable i) {
        //store previous rotation
        float initialRotX = transform.eulerAngles.x;
        float initialRotZ = transform.eulerAngles.z;

        float targetRotation;
        if (i.cardStack.IsFaceUp) {
            //clerp to rotation 0
            targetRotation = 0;
        }
        else {
            //clerp to rotation 180
            targetRotation = 180;
        }

        //interpolate flipping
        currentRotation = Interpolation.Clerp(currentRotation, targetRotation, flipRate);
        if(currentRotation < 90) {
            transform.eulerAngles = new Vector3(initialRotX, currentRotation, initialRotZ);
            render.isFlipped = false;
        }
        else {
            //if it's flipped more than halfway, don't mirror the image
            transform.eulerAngles = new Vector3(initialRotX, 180 - currentRotation, initialRotZ);
            render.isFlipped = true;
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
