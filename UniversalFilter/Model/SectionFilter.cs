
using Tekla.Structures.Filtering;

namespace UniversalFilter.Model
{
    internal class SectionFilter
    {
        public SectionFilter(bool isVisible, ExpressionTypeLeft left, OperatorType @operator, ExpressionRight right)
        {
            StartParenthesis = 0;
            IsVisible = isVisible;
            Left = left;
            Operator = @operator;
            Right = right;
            EndParenthesis = 0;
            ExitOperator = BinaryFilterOperatorType.EMPTY;
        }
        public SectionFilter(ExpressionTypeLeft left, OperatorType @operator, ExpressionRight right)
        {
            StartParenthesis = 0;
            IsVisible = true;
            Left = left;
            Operator = @operator;
            Right = right;
            EndParenthesis = 0;
            ExitOperator = BinaryFilterOperatorType.EMPTY;
        }

        public int StartParenthesis { get; set; }//1
        public bool IsVisible { get; set; }//2
        public ExpressionTypeLeft Left { get; set; }//3,4,5
        public OperatorType Operator { get; set; }//6,7
        public ExpressionRight Right { get; set; }//8
        public int EndParenthesis { get; set; }//9
        public BinaryFilterOperatorType ExitOperator { get; set; }//10
    }
}
