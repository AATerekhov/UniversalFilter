
namespace UniversalFilter.Model
{
    internal abstract class ExpressionRight
    {
        protected ExpressionRight(string right)
        {
            Right = right;
        }

        public string Right { get; set; }
    }
}
