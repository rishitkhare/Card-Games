using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : IComparable {

    public CardStack Hand { get; set; }
    public string ID { get; private set; }
    public int Score { get; set; }

    public Player(string ID) {
        this.ID = ID;
        Score = 0;
        Hand = new CardStack(true);
    }

    public int CompareTo(object obj) {
        Player other = obj as Player;

        if(other == null) {
            throw new ArgumentException("Comparing two incompatible types");
        }

        return this.Score.CompareTo(other.Score);
    }
}
