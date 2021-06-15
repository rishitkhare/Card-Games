using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CrazyEights : Game {
    public Deck output;
    private bool reverse;
    private Suit chosenSuit = Suit.None;
    private int chosenPlayerIndex = -1;

    public GameObject ButtonPrefab;
    List<ButtonData> suitButtons;
    List<ButtonData> playerButtons;

    void Start() {
        game = CardGame.CrazyEights;

        playerButtons = new List<ButtonData>();
        suitButtons = new List<ButtonData>();

        for(int i = 0; i < numberOfPlayers; i++) {
            //set up button for each player
            GameObject buttonObject = Instantiate(ButtonPrefab, GameManager.gm.CanvasGameObject.transform);
            playerButtons.Add(buttonObject.GetComponent<ButtonData>());
            playerButtons[i].textComponent.text = $"Player {i + 1}";
            playerButtons[i].rectTransform.anchoredPosition = new Vector3(-300f, -40f * (i + 1));
            playerButtons[i].button.enabled = false;
            int fuckingeventsbro = i;
            playerButtons[i].button.onClick.AddListener(() => ChoosePlayer(fuckingeventsbro));
        }

        for (int i = 0; i < 4; i++) {
            GameObject buttonObject = Instantiate(ButtonPrefab, GameManager.gm.CanvasGameObject.transform);
            suitButtons.Add(buttonObject.GetComponent<ButtonData>());
            Suit currentSuit = (Suit)(1 << i);
            suitButtons[i].textComponent.text = "" + currentSuit;
            suitButtons[i].rectTransform.anchoredPosition = new Vector3(300f, -300f + -40f * (i + 1));
            suitButtons[i].button.enabled = false;
            suitButtons[i].button.onClick.AddListener(() => ChooseSuit(currentSuit));
        }

        SetUp();
    }

    #region overrides
    override
    public void PickUp(Interactable selectedDeck, Card play) {
        if (selectedDeck.Equals(GameManager.gm.handDisplay)) {
            PickUpFromHand(play);
        }

        if (selectedDeck.Equals(deck)) {
            PickUpFromDeck(play);
        }
    }

    override
    public void Place(Interactable selectedDeck, Interactable prevDeck) {
        if (selectedDeck.Equals(output)) {
            deck.lockPickup = true;
            output.lockPlace = true;

            if (!CardEffect()) {
                OnTurnEnd();
            }
        }
    }

    override
    public void SetUp() {
        deck.lockPlace = true;
        output.lockPickup = true;
        reverse = false;

        deck.cardStack.ClearCardStack();
        output.cardStack.ClearCardStack();

        players.ForEach(p => p.Hand.ClearCardStack());

        deck.GenerateDeck(true, true);

        if (!singleDeck) {
            deck.GenerateDeck(false, true);
        }

        deck.cardStack.Shuffle();
        output.cardStack.IsFaceUp = true;
        Deal();
        ReplaceSpecial();

        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
    }

    override
    public void OnTurnEnd() {
        bool roundOver = false;
        Debug.Log(turn);
        foreach (Player p in players) {
            if (p.Hand.NumberOfCards() == 0) {
                roundOver = true;
                OnRoundOver();
                break;
            }
        }

        if (!roundOver) {
            OnNewTurn();
        }
    }

    override
    public void OnNewTurn() {
        deck.lockPickup = false;
        output.lockPlace = false;

        currentPlayer = players[turn % players.Count];
        GameManager.gm.handDisplay.cardStack = currentPlayer.Hand;
    }

    #endregion overrides

    private void OnRoundOver() {
        players.ForEach(p => p.Score += p.Hand.TotalWorthCrazyEights());

        players.Sort();
        if (players[players.Count - 1].Score >= pointCap) {
            LogTheLeader(true);
            // end game
        }

        else {
            LogTheLeader(false);
            SortByHandWorth();
            SetUp();
        }
    }

    private void ReplaceSpecial() {
        while (output.cardStack.NumberOfCards() == 0 ||
            output.cardStack.GetCardRank(0) == Rank.Eight ||
            output.cardStack.GetCardRank(0) == Rank.Two ||
            output.cardStack.GetCardRank(0) == Rank.Queen ||
            output.cardStack.GetCardRank(0) == Rank.Joker) {

            if (output.cardStack.NumberOfCards() != 0) {
                deck.cardStack.AddCardToBottom(output.GetCard());
            }

            output.cardStack.AddCardToTop(deck.GetCard());
        }
    }

    private void PickUpFromDeck(Card play) {
        output.lockPlace = InvalidPlace(play);

        if (deck.cardStack.NumberOfCards() == 0) {
            for (int i = 1; i < output.cardStack.NumberOfCards(); i++) {
                deck.cardStack.AddCardToTop(output.cardStack.TakeBottomCard());
            }

            deck.cardStack.Shuffle();
        }
    }

    private void PickUpFromHand(Card play) {
        output.lockPlace = InvalidPlace(play);
    }

    private void TurnIncrement() => turn = reverse ? (turn == 0 ? players.Count - 1 : --turn) : (turn == players.Count - 1 ? 0 : ++turn);

    private bool CardEffect() {
        switch(output.cardStack.GetCardRank(0)) {
            case Rank.Eight:
                chosenSuit = Suit.None;
                suitButtons.ForEach(b => b.button.enabled = true);
                break;

            case Rank.Joker:
                chosenPlayerIndex = -1;
                playerButtons.ForEach(b => b.button.enabled = true);
                break;

            case Rank.Two:
                TwoEffect();
                return false;

            case Rank.Queen:
                reverse = !reverse;
                TurnIncrement();
                return false;

            default:
                TurnIncrement();
                return false;
        }

        return true;
    }

    private void TwoEffect() {
        TurnIncrement();
        for (int i = 1; i <= 2; i++) {
            players[turn % players.Count].Hand.AddCardToBottom(deck.GetCard());
        }

        TurnIncrement();
    }

    private void ChooseSuit(Suit suit) {
        Debug.Log(chosenSuit = suit);

        suitButtons.ForEach(b => b.button.enabled = false);

        TurnIncrement();

        OnTurnEnd();
    }

    private void ChoosePlayer(int playerIndex) {
        Debug.Log(chosenPlayerIndex = playerIndex);

        suitButtons.ForEach(b => b.button.enabled = false);

        turn = chosenPlayerIndex; 
        // reverse ? turn - Math.Abs(turn % players.Count - chosenPlayerIndex) : turn + Math.Abs(turn % players.Count - chosenPlayerIndex);

        for (int i = 1; i <= 4; i++) {
            players[turn % players.Count].Hand.AddCardToBottom(deck.GetCard());
        }

        OnTurnEnd();
    }


    private bool InvalidPlace(Card play) {

        if (play.Rank != Rank.Eight) {

            if (output.cardStack.GetCardRank(0) == Rank.Eight) {
                if ((chosenSuit & (Suit) SuitCombo.Reds) != Suit.None) {

                    // returns if card is not chosen red suit or joker
                    return (play.Suit & (chosenSuit | Suit.RedJoker)) == Suit.None;
                }

                else if ((chosenSuit & (Suit)SuitCombo.Blacks) != Suit.None) {

                    // returns if card is not chosen black suit or joker
                    return (play.Suit & (chosenSuit | Suit.BlackJoker)) == Suit.None;
                }
            }

            if (output.cardStack.GetCardRank(0) == Rank.Joker) {

                // returns if card is not the same color as output joker
                return ( output.cardStack.GetCardSuit(0) == Suit.BlackJoker && (play.Suit & (Suit) SuitCombo.AllBlacks) == Suit.None ) ||
                       ( output.cardStack.GetCardSuit(0) == Suit.RedJoker && (play.Suit & (Suit) SuitCombo.AllReds) == Suit.None );
            }

            if (play.Rank != Rank.Joker) {

                // returns if card is not same rank and not same suit
                return play.Rank != output.cardStack.GetCardRank(0) && play.Suit != output.cardStack.GetCardSuit(0);
            }

            else {

                // returns if card joker is not the same color as output
                return ( play.Suit == Suit.BlackJoker && (output.cardStack.GetCardSuit(0) & (Suit) SuitCombo.AllBlacks) == Suit.None ) ||
                       ( play.Suit == Suit.RedJoker && (output.cardStack.GetCardSuit(0) & (Suit) SuitCombo.AllReds) == Suit.None );
            }
        }

        // if card is eight
        return false;
    }
}
