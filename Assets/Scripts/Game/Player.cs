using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : IComparable {

    public CardStack Hand { get; set; }
    public int Wins { get; private set; }
    public string ID { get; private set; }
    public int Score { get; set; }

    public Player(string ID) {
        this.ID = ID;

        Wins = 0;
        Score = 0;
        Hand = new CardStack(true);
    }

    public void IncrementWin() {
        Wins++;
    }

    public int CompareTo(object obj) {
        Player other = obj as Player;

        if(other == null) {
            throw new ArgumentException("Comparing two incompatible types");
        }

        return other.Score - this.Score;
    }
}
