using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanOut : MonoBehaviour {
    public int fanOutIndex;
    public int totalNumberOfCards;
    public float fanWidth = 3f;
    public float maxRotation = 120f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
       transform.position = new Vector3(fanOutIndex * ( fanWidth / totalNumberOfCards ), 0, 0);
    }
}
