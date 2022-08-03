using Tekla.Structures.Filtering;

namespace UniversalFilter.Model
{
    internal class ReferenceFilterGroupe : ExpressionFilterGroup
    {
        public ReferenceFilterGroupe()
            : base(new SectionFilter(false, new ObjectTypeTypeLeft(), OperatorType.IS_EQUAL, new ReferenceGroupRight()))
        {

        }
    }
    internal class BoltFilterGroupe : ExpressionFilterGroup
    {
        public BoltFilterGroupe()
            : base(new SectionFilter(new ObjectTypeTypeLeft(), OperatorType.IS_EQUAL, new BoltGroupRight()))
        {

        }
    }
    internal class PartFilterGroupe : ExpressionFilterGroup
    {
        public PartFilterGroupe()
            : base(new SectionFilter(new ObjectTypeTypeLeft(), OperatorType.IS_EQUAL, new PartGroupRight()))
        {

        }

        protected override SectionFilter EndCloseSection(SectionFilter sectionFilter)
        {
            sectionFilter.StartParenthesis = 0;
            sectionFilter.EndParenthesis = 1;
            sectionFilter.ExitOperator = BinaryFilterOperatorType.EMPTY;
            return sectionFilter;
        }
        protected override SectionFilter CloseSection(SectionFilter sectionFilter)
        {
            sectionFilter.StartParenthesis = 1;
            sectionFilter.EndParenthesis = 1;
            sectionFilter.ExitOperator = BinaryFilterOperatorType.EMPTY;
            return sectionFilter;
        }

    }
}
