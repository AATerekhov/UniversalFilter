
namespace UniversalFilter.Model
{
    public class TemplateTypeLeft : ExpressionTypeLeft
    {
        public TemplateTypeLeft(string propertyExpression)
            : base("TemplateCategory", propertyExpression, propertyExpression)
        {

        }

    }
    public class ObjectTypeTypeLeft : ExpressionTypeLeft
    {
        public ObjectTypeTypeLeft()
            : base("co_object", "proOBJECT_TYPE", "albl_ObjectType")
        {

        }
    }

    public class PartPhaseTypeTypeLeft : ExpressionTypeLeft
    {
        public PartPhaseTypeTypeLeft()
            : base("co_part", "proPHASE", "albl_Phase")
        {

        }
    }

    public class PartClassTypeTypeLeft : ExpressionTypeLeft
    {
        public PartClassTypeTypeLeft()
            : base("co_part", "proCLASS", "albl_Class")
        {

        }
    }
    public class BoltStandardTypeTypeLeft : ExpressionTypeLeft
    {
        public BoltStandardTypeTypeLeft()
            : base("co_bolt", "proSTANDARD", "albl_Standard")
        {

        }
    }
}
