using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking;

namespace Test {
    public class Debugger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Main();
        }

        void Main()
        {
            Debug.Log("Hello World!");

            List<int> iGive = new List<int> { 0, 0, 0, 0 };
            List<int> iTake = new List<int> { 0, 0, 0, 0 };

            var testMove = new Move(-2, -1, iGive, iTake);

            Debug.Log(new Move(testMove.ToString()));

            var move1 = new Move(-1, -2, new List<int>() { 0, 0 }, new List<int>() { 0, 0 });
            var move2 = new Move(-1, 1, new List<int>() { 0, 0 }, new List<int>() { 0, 0 });
            var move3 = new Move(3, -1, new List<int>() { 0, 0 }, new List<int>() { 0, 0 });

            var testPlayerTurn = new PlayerTurnData(1, new List<Move>() { move1, move2, move3 });
            Debug.Log(testPlayerTurn);

            Debug.Log(new PlayerTurnData(testPlayerTurn.ToString()));
        }
    }
}
