using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Shvetsov_Int_knowl_lab_4.Figures;
using System.Linq;
using Shvetsov_Int_knowl_lab_4;

public class ReverseInferenceAlgorythm : MonoBehaviour {

    static ReverseInferenceAlgorythm instance;

    HashSet<ProductiveRule> activeRules;
    HashSet<BoardCell> visitedCells;
    System.Random random;

    public Queue<ProductiveRule> usedRules;
    BoardCell currentGoToCell;

    private void Awake()
    {
        visitedCells = new HashSet<BoardCell>();
        usedRules = new Queue<ProductiveRule>();
        random = new System.Random();
        instance = this;
    }

    public void SolveTracingAlgorythm(FigureController figure, CellController goToCell)
    {
        HashSet<ProductiveRule> rulesToRemove = new HashSet<ProductiveRule>();
        figure.MoveGameFigureIgnoringRules(goToCell);
        currentGoToCell = goToCell.Cell;
        visitedCells.Add(currentGoToCell);

        while(true)
        {
            activeRules = new HashSet<ProductiveRule>(FigureMoveRules.GetMoveRules(figure.Figure)
                            .Where(rule => rule.RightHandSideRule == currentGoToCell));

            foreach (ProductiveRule rule in activeRules)
                if (visitedCells.Contains(rule.LeftHandSideRule))
                    rulesToRemove.Add(rule);

            RemoveIntersectionFromList(rulesToRemove, activeRules);

            var usedRule = RandomStrategy(activeRules);
            if (usedRule == null)
                break;

            figure.MoveGameFigureToCell(Board.INSTANCE[usedRule.LeftHandSideRule]);
            currentGoToCell = usedRule.LeftHandSideRule;
            visitedCells.Add(currentGoToCell);

            usedRules.Enqueue(usedRule);
        }

        MovingAlgorythms.INSTANCE.MoveFigureInToShowRules(figure, usedRules, true);
     }

    void RemoveIntersectionFromList(HashSet<ProductiveRule> rulesToRemove, HashSet<ProductiveRule> rules)
    {
        foreach (var rule in rulesToRemove)
            rules.Remove(rule);

        rulesToRemove.Clear();
    }

    ProductiveRule RandomStrategy(HashSet<ProductiveRule> rules)
    {
        int randomNumber = random.Next(0, rules.Count);
        if (rules.Count > 0)
            return rules.ElementAt(randomNumber);
        else
            return null;
    }

    static public ReverseInferenceAlgorythm INSTANCE
    {
        get { return instance; }
    }
}
