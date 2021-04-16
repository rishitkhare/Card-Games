using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RenderCard))]
public class CardSelector : MonoBehaviour {
    RenderCard render;
    CardStack storeCard;
    Camera cam;

    public UnityEvent<Interactable, Card> onCardPickup;
    public UnityEvent<Interactable, Interactable> onCardPlace;

    [HideInInspector]
    public Interactable selectedDeck;
    [HideInInspector]
    public Interactable prevInteractable;

    public float flipRate = 0.3f;
    public float minClickingDistance = 5f;

    float currentRotation = 0f;
    float targetRotation;

    // Start is called before the first frame update
    void Start() {
        render = gameObject.GetComponent<RenderCard>();
        storeCard = new CardStack(true);
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        selectedDeck = null;
        prevInteractable = null;
        
        if (onCardPickup == null) {
            onCardPickup = new UnityEvent<Interactable, Card>();
        }

        if (onCardPlace == null) {
            onCardPlace = new UnityEvent<Interactable, Interactable>();
        }
    }

    void Update() {
        selectedDeck = GetSelectedInteractable();
        if (Input.GetMouseButtonDown(0) && selectedDeck != null) {

            if(storeCard.NumberOfCards() == 0 && selectedDeck.lockPickup == false) {
                try {
                    storeCard.AddCardToTop(selectedDeck.GetCard(cam.ScreenToWorldPoint(Input.mousePosition)));

                    Card selectedCard = new Card(storeCard.GetCardSuit(0), storeCard.GetCardRank(0));
                    onCardPickup?.Invoke(selectedDeck, selectedCard);
                }
                catch { }

                prevInteractable = selectedDeck;
            }

            else if (storeCard.NumberOfCards() != 0 && selectedDeck.lockPlace == false) {
                selectedDeck.GiveCard(storeCard.TakeTopCard());
                onCardPlace?.Invoke(selectedDeck, prevInteractable);

                prevInteractable = selectedDeck;
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

        if(i != null) {
            if (i.cardStack.IsFaceUp) {
                //clerp to rotation 0
                targetRotation = 0;
            }
            else {
                //clerp to rotation 180
                targetRotation = 180;
            }
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
        float minDistanceToDeck = 0f;

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

        if(result is Hand) {
            return result;
        }

        if(minDistanceToDeck <= minClickingDistance) {
            return result;
        }
        else {
            return null;
        }
    }

    public Card GetSelectedCard() {
        return new Card(storeCard.GetCardSuit(0), storeCard.GetCardRank(0), storeCard.GetCardBack(0));
    }

    
}
