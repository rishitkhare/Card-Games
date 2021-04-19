using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Game : MonoBehaviour {

    public int numberOfPlayers;
    [HideInInspector] 
    public List<Player> players;
    [HideInInspector]
    public Player currentPlayer;
    [HideInInspector]
    public Deck deck;
    [HideInInspector] 
    public int turn;
    public int cardsDealt;
    public bool singleDeck;
    public int pointCap;

    public enum CardGame {
        Tunk,
        CrazyEights
    }

    [HideInInspector] 
    public CardGame game;


    // Start is called before the first frame update
    void Awake()
    {
        players = new List<Player>();
        AddPlayersToGame();
        currentPlayer = players[0];
        turn = 0;
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
            int i = 1;
            string message = $"Players {players[0].ID}";

            while (players[i] == players[i - 1]) {
                message += $", {players[i].ID}";
                i++;
            }

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
