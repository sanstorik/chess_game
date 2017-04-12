using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TracingAlgorythm {
    static TracingAlgorythm instance;
    List<MoveExample> moves;

    private TracingAlgorythm()
    {
        moves = new List<MoveExample>();
    }

    public void Solve() 
    {
        BoardExample mainBoard = new BoardExample();

        //first RED
        foreach (var move in mainBoard.GetPossibleRedMoves())
            moves.Add(move);

        //second BLUE
        HashSet<MoveExample> childMoves = moves.ElementAt(0).FindChilds(!moves.ElementAt(0).IsRedMove());


        //THIRD RED
        foreach (var move in childMoves)
        {
            //FOURTH BLUE
            foreach (var moveSecond in move.FindChilds(!move.IsRedMove()))
                //FIFTH RED
                foreach (var moveThird in moveSecond.FindChilds(!moveSecond.IsRedMove()))
                    //SIXTH BLUE
                    foreach (var moveFourth in moveSecond.FindChilds(!moveThird.IsRedMove()));
        }
    }

    public static TracingAlgorythm GetInstance()
    {
        if (instance == null)
            instance = new TracingAlgorythm();

        return instance;
    }
}
