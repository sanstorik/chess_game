using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Shvetsov_Int_knowl_lab_4.Figures;
using System.Linq;
using Shvetsov_Int_knowl_lab_4;
using System;

public class MovingAlgorythms : MonoBehaviour {

    static MovingAlgorythms instance;

    HashSet<BoardCell> visitedCells;
    HashSet<ProductiveRule> workRules;

    public Queue<ProductiveRule> usedRules;

    private void Awake()
    {
        instance = this;
        visitedCells = new HashSet<BoardCell>();
        usedRules = new Queue<ProductiveRule>();
    }

    public void SolveTracingProblem(FigureController figure)
    {
        HashSet<ProductiveRule> rulesToRemove = new HashSet<ProductiveRule>();

        while(true)
        {
            workRules = new HashSet<ProductiveRule>(figure.GetWorkRules());

            foreach (ProductiveRule rule in workRules)
                if (visitedCells.Contains(rule.RightHandSideRule))
                    rulesToRemove.Add(rule);

            RemoveIntersectionFromList(rulesToRemove, workRules);

            var currentRule = WansfordRule(figure);
            if (currentRule == null)
                break;

            usedRules.Enqueue(currentRule);

            figure.MoveGameFigureToCell(Board.INSTANCE[currentRule.RightHandSideRule]);
        }

        MoveFigureInToShowRules(figure, usedRules);
    }

    ProductiveRule WansfordRule(FigureController figure)
    {
        int[] rulesMovesCount = new int[workRules.Count];
        HashSet<ProductiveRule> rulesToRemove = new HashSet<ProductiveRule>();
        HashSet<ProductiveRule> rulesMovesFromPosition;

        visitedCells.Add(figure.Figure.CurrentCell);

        for (int i = 0; i < workRules.Count; i++)
        {
            rulesMovesFromPosition = new HashSet<ProductiveRule>(
                FigureMoveRules.GetMoveRulesFromPosition(figure.Figure, workRules.ElementAt(i).RightHandSideRule));

            foreach (ProductiveRule rule in rulesMovesFromPosition)
                if (visitedCells.Contains(rule.RightHandSideRule))
                    rulesToRemove.Add(rule);

            RemoveIntersectionFromList(rulesToRemove, rulesMovesFromPosition);
            rulesMovesCount[i] = rulesMovesFromPosition.Count;
        }

        return FindRuleWithLeastMoves(rulesMovesCount);
    }

    void RemoveIntersectionFromList(HashSet<ProductiveRule> rulesToRemove, HashSet<ProductiveRule> rules)
    {
        foreach (var rule in rulesToRemove)
             rules.Remove(rule);

        rulesToRemove.Clear();
    }

    ProductiveRule FindRuleWithLeastMoves(int[] rulesCount)
    {
        if (rulesCount.Length <= 0)
            return null;

        int min = rulesCount[0];
        int minIndex = 0;

        for (int i = 1; i < rulesCount.Length; i++)
            if (rulesCount[i] < min)
            {
                min = rulesCount[i];
                minIndex = i;
            }

        //kind of WIDTH SEARCH
        return workRules.ElementAt(minIndex);
    }

    public void MoveFigureInToShowRules(FigureController figure, Queue<ProductiveRule> rules, bool reverse = false)
    {
        StartCoroutine(StartMove(figure, rules, reverse));
    }

    IEnumerator StartMove(FigureController figure, Queue<ProductiveRule> rules, bool reverse)
    {
        yield return new WaitForSeconds(2f);

        CellController cell;
        if (!reverse)
            cell = Board.INSTANCE[rules.Peek().LeftHandSideRule];
        else
            cell = Board.INSTANCE[rules.Peek().RightHandSideRule];

        ProductiveRule rule;
        figure.MoveGameFigureIgnoringRules(cell);
        cell.StartGlowing();

        yield return new WaitForSeconds(2f);

        while(rules.Count > 0)
        {
            rule = rules.Dequeue();
            if (!reverse)
                cell = Board.INSTANCE[rule.RightHandSideRule];
            else
                cell = Board.INSTANCE[rule.LeftHandSideRule];
            cell.StartGlowing();
            figure.MoveGameFigureIgnoringRules(cell);

            yield return new WaitForSeconds(1f);
        }
    }

    static public MovingAlgorythms INSTANCE
    {
        get { return instance; }
    }
}
