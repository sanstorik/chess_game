
namespace Shvetsov_Int_knowl_lab_4.Figures
{
    public class ProductiveRule
    {
        int priority;
        BoardCell leftHandSideRule;
        BoardCell rightHandSideRule;


        /// <summary>
        /// Creates a common rule of valid figure move
        /// </summary>
        /// <param name="leftHandSideRule"> move FROM  (if rule)</param>
        /// <param name="rightHandSideRule">move TO  (then rule)</param>
        /// <param name="priority"> priority of rule in list</param>
        public ProductiveRule(BoardCell leftHandSideRule, BoardCell rightHandSideRule, int priority)
        {
            this.leftHandSideRule = leftHandSideRule;
            this.rightHandSideRule = rightHandSideRule;
            this.priority = priority;
        }

        public BoardCell LeftHandSideRule
        {
            get { return leftHandSideRule; }
        }

        public BoardCell RightHandSideRule
        {
            get { return rightHandSideRule; }
        }

        public int Priority
        {
            get { return priority; }
        }

        public override bool Equals(object obj)
        {
            ProductiveRule rule = (ProductiveRule)obj;
            return rule.leftHandSideRule.Equals(leftHandSideRule) && 
                rule.rightHandSideRule.Equals(rightHandSideRule);
        }

        public override int GetHashCode()
        {
            return leftHandSideRule.GetHashCode() ^ rightHandSideRule.GetHashCode() * 500;
        }
    }
}
