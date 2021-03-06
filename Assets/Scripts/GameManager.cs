﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Rendering;
using Framework;

public class GameManager : MonoBehaviour
{
    public List<Interactable> interactableDecks;
    public CardSpriteArray spriteArray;
    public GameObject CanvasGameObject;
    public static GameManager gm;

    [HideInInspector]
    public Camera mainCamera;

    [HideInInspector]
    public CardSelector cardSelector;
    

    public Hand handDisplay;

    void Awake() {
        if(gm != null) {
            Debug.Log("Failed Singleton Design");
        }
        else {
            gm = this;
        }

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cardSelector = GameObject.FindGameObjectWithTag("CardSelector").GetComponent<CardSelector>();
    }
}
