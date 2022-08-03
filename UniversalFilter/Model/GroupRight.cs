
namespace UniversalFilter.Model
{
    public class BoltGroupRight : ExpressionRight
    {
        public BoltGroupRight()
        : base("albl_BoltGroup")
        {

        }
    }
    public class PartGroupRight : ExpressionRight
    {
        public PartGroupRight()
            : base("albl_Part")
        {

        }
    }
    public class ReferenceGroupRight : ExpressionRight
    {
        public ReferenceGroupRight()
            : base("albl_Reference_object")
        {

        }
    }
    public class FreeSrtingRight : ExpressionRight
    {
        public FreeSrtingRight(string value)
            : base(value)
        {

        }
    }
}
