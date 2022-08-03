
namespace UniversalFilter.Model
{
    internal class TemplateTypeLeft : ExpressionTypeLeft
    {
        public TemplateTypeLeft(string propertyExpression)
            : base("TemplateCategory", propertyExpression, propertyExpression)
        {

        }

    }
    internal class ObjectTypeTypeLeft : ExpressionTypeLeft
    {
        public ObjectTypeTypeLeft()
            : base("co_object", "proOBJECT_TYPE", "albl_ObjectType")
        {

        }
    }

    internal class PartPhaseTypeTypeLeft : ExpressionTypeLeft
    {
        public PartPhaseTypeTypeLeft()
            : base("co_part", "proPHASE", "albl_Phase")
        {

        }
    }

    internal class PartClassTypeTypeLeft : ExpressionTypeLeft
    {
        public PartClassTypeTypeLeft()
            : base("co_part", "proCLASS", "albl_Class")
        {

        }
    }
    internal class BoltStandardTypeTypeLeft : ExpressionTypeLeft
    {
        public BoltStandardTypeTypeLeft()
            : base("co_bolt", "proSTANDARD", "albl_Standard")
        {

        }
    }
}
