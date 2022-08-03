
namespace UniversalFilter.Model
{
    internal class BoltGroupRight : ExpressionRight
    {
        public BoltGroupRight()
        : base("albl_BoltGroup")
        {

        }
    }
    internal class PartGroupRight : ExpressionRight
    {
        public PartGroupRight()
            : base("albl_Part")
        {

        }
    }
    internal class ReferenceGroupRight : ExpressionRight
    {
        public ReferenceGroupRight()
            : base("albl_Reference_object")
        {

        }
    }
    internal class FreeSrtingRight : ExpressionRight
    {
        public FreeSrtingRight(string value)
            : base(value)
        {

        }
    }
}
