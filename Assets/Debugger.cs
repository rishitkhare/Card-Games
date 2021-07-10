using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Main();
    }

    void Main() {
        Debug.Log("Hello World!");

        List<int> iGive = new List<int> {0,0,0,0 };
        List<int> iTake = new List<int> {0,0,0,0 };

        PacketProtocol.Move testMove = new PacketProtocol.Move(-2, -1, iGive, iTake);

        Debug.Log(new PacketProtocol.Move(testMove.ToString()));
    }
}
