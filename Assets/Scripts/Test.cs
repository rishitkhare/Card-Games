using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        CardStack a = new CardStack(true);
        a.GenerateDeck52();


        Debug.Log(a);

        a.Shuffle();

        Debug.Log(a);

        a.SortByRank();
        Debug.Log(a);
    }
}
