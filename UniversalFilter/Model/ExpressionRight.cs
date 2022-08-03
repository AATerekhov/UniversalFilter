
namespace UniversalFilter.Model
{
    public abstract class ExpressionRight
    {
        protected ExpressionRight(string right)
        {
            Right = right;
        }

        public string Right { get; set; }
    }
}
