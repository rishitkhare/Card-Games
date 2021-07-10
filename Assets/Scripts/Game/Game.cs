using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Game : MonoBehaviour {

    //SERIALIZED FIELDS DO NOT TOUCH I'M TALKING TO YOU
    public int numberOfPlayers = 2;
    public int cardsDealt = 5;
    public bool singleDeck = true;
    public int pointCap = 100;


    protected List<Player> players;
    protected Player currentPlayer;
    protected Deck deck;
    protected Deck output;
    protected int turn;
    public enum CardGame {
        Tunk,
        CrazyEights,
        BS,
    }

    [HideInInspector] 
    public CardGame game;


    // Start is called before the first frame update
    void Awake() {
        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        output = GameObject.FindGameObjectWithTag("Output").GetComponent<Deck>();
        players = new List<Player>();
        AddPlayersToGame();
        turn = 0;
        currentPlayer = players[turn];
        GameManager.gm.cardSelector.onCardPickup.AddListener(PickUp);
        GameManager.gm.cardSelector.onCardPlace.AddListener(Place);
    }

    void AddPlayersToGame() {
        for(int i = 0; i < numberOfPlayers; i ++) {
            players.Add(new Player($"{i + 1}"));
        }
    }

    public void Deal() {
        foreach (Player player in players) {
            for (int i = 0; i < cardsDealt; i++) {
                player.Hand.AddCardToTop(deck.GetCard());
            }
        }
    }

    public void SortByHandWorth() {
        Player sort = null;
        int currentMin = 0;

        for (int i = 0; i < players.Count - 1; i++) {
            currentMin = i;
            sort = players[i];

            for (int j = i + 1; j < players.Count; j++) {
                if (game == CardGame.Tunk) {
                    if (players[j].Hand.TotalWorthTunk() < players[i].Hand.TotalWorthTunk()) {
                        currentMin = j;
                    }
                }

                else if (game == CardGame.CrazyEights) {
                    if (players[j].Hand.TotalWorthCrazyEights() < players[i].Hand.TotalWorthCrazyEights()) {
                        currentMin = j;
                    }
                }
            }

            players[i] = players[currentMin];
            players[currentMin] = sort;
        }

        //players.OrderBy(p => p.Hand.TotalWorthTunk()).ToList();
    }

    public void LogTheLeader(bool gameEnded) {
        //sort based on total score

        if (players[0].Score == players[1].Score) {
            int i = 0;
            string message = $"Players ";

            while (players[i].Score == players[i + 1].Score) {
                message += $"{players[i].ID}, ";
                i++;
                if(i + 1 >= players.Count) {
                    break;
                }
            }

            message += $"{players[i].ID}, ";

            message = message.Substring(0, message.Length - 2);

            message += gameEnded ? " won!" : " are in the lead!";
            Debug.Log(message);

        }
        else {
            string message = $"Player {players[0].ID} ";
            message += gameEnded ? " won!" : " is in the lead!";
            Debug.Log(message);
        }
    }

    public abstract void PickUp(Interactable selectedDeck, Card play);
    public abstract void Place(Interactable selectedDeck, Interactable prevDeck);
    public abstract void SetUp();
    public abstract void OnTurnEnd();
    public abstract void OnNewTurn();
}
