using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class PacketProtocol {

    public class PlayerTurnData {
        int playerturn;
        List<Move> moves;

        override
        public string ToString() {
            string output = "" + playerturn;
            moves.ForEach(m => output += m);

            return output.Substring(0, output.Length - 1);
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

        public Move(int giver, int taker, List<int> iGive, List<int> iTake) {
            Giver = (CardStackID) giver;
            Taker = (CardStackID) taker;
            indexGive = iGive;
            indexTake = iTake;
        }

        public Move(string packet) {
            ParsePacket(packet);
        }

        override
        public string ToString() {
            return $"{CardStackIDToString(Giver)} " +
                $"{CardStackIDToString(Taker)} " +
                $"{IndexArrayToString(indexGive)} " +
                $"{IndexArrayToString(indexTake)}\n";

            // "[GiverID] [TakerID] { {indexGiveList} }  { {indexTakeList} }"
        }

        private string CardStackIDToString(CardStackID ud) => /*p*/ud.ToString();

        private string IndexArrayToString(List<int> pud2) {
            string output = "";
            pud2.ForEach(i => output += i + ",");

            return output.Substring(0, output.Length - 1);
        }

        private void ParsePacket(string packet) {
            string[] terms = packet.Split(' ');

            Giver = (CardStackID) Enum.Parse(typeof(CardStackID), terms[0]);
            Taker = (CardStackID)Enum.Parse(typeof(CardStackID), terms[1]);
            indexGive = ParseIndexList(terms[2]);
            indexTake = ParseIndexList(terms[3]);
        }

        private List<int> ParseIndexList(string list) =>
            list.Split(',').ToList().Select(s => int.Parse(s)).ToList();
    }

}
