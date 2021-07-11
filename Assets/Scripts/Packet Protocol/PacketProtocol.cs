using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Networking {
    public class PlayerTurnData {
        int playerturn;
        List<Move> moves;

        public PlayerTurnData(int playerTurn, List<Move> moves) => (this.playerturn, this.moves) = (playerTurn, moves);

        public PlayerTurnData(string packet) {
            var listOfMoves = packet.Split('\n').ToList();
            (playerturn, moves) = (
                Convert.ToInt32(listOfMoves[0]),
                listOfMoves.GetRange(1, listOfMoves.Count - 1).Select(s => new Move(s)).ToList()
            );
        }

        override
        public string ToString() {
            string output = playerturn + "\n";
            return (output += String.Join<Move>("", moves)).Substring(0, output.Length - 1);
        }
    }

    public enum CardStackID {
        d = -2, // "d" is for Deck
        o = -1, // "o" is for Output
        // All other numbers will be treated as an index of the player list
    }

    public class Move {
        CardStackID Giver;
        CardStackID Taker;

        //these two should be same length
        List<int> indexGive;
        List<int> indexTake;

        public Move(int giver, int taker, List<int> iGive, List<int> iTake) =>
            (Giver, Taker, indexGive, indexTake) = ((CardStackID)giver, (CardStackID)taker, iGive, iTake);

        public Move(string packet) => ParsePacket(packet);

        override
        public string ToString() => 
            $"{CardStackIDToString(Giver)} " +
            $"{CardStackIDToString(Taker)} " +
            $"{IndexArrayToString(indexGive)} " +
            $"{IndexArrayToString(indexTake)}\n";

        private string CardStackIDToString(CardStackID ud) => /*p*/ud.ToString();

        private string IndexArrayToString(List<int> pud2) {
            string output = String.Join<int>(",", pud2);
            return output.Substring(0, output.Length - 1);
        }

        private void ParsePacket(string packet) {
            string[] terms = packet.Split(' ');
            (Giver, Taker, indexGive, indexTake) = (
                (CardStackID)Enum.Parse(typeof(CardStackID), terms[0]),
                (CardStackID)Enum.Parse(typeof(CardStackID), terms[1]),
                ParseIndexList(terms[2]),
                ParseIndexList(terms[3])
            );
        }

        private List<int> ParseIndexList(string list) =>
            list.Split(',').Select(s => int.Parse(s)).ToList();
    }

}
