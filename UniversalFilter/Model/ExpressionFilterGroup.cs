using System.Collections;
using System.Collections.Generic;
using Tekla.Structures.Filtering;

namespace UniversalFilter.Model
{
    public abstract class ExpressionFilterGroup : IEnumerable
    {
        public ExpressionFilterGroup(SectionFilter startSection)
        {
            Sections = new List<SectionFilter>();
            this.Add(startSection);
        }

        protected List<SectionFilter> Sections { get; set; }

        public int Count
        {
            get { return Sections.Count; }
        }

        public IEnumerator GetEnumerator() => Sections.GetEnumerator();

        public virtual void Add(SectionFilter sectionFilter)
        {
            if (Sections?.Count == 0) Sections.Add(CloseSection(sectionFilter));
            else
            {
                СleaningParenthesis();
                StartCloseSection(Sections[0]);
                Sections.Add(EndCloseSection(sectionFilter));
            }
        }

        public virtual void Remuve(SectionFilter sectionFilter)
        {
            Sections.Remove(sectionFilter);
            if (Sections?.Count == 1) CloseSection(Sections[0]);
            else if (Sections?.Count != 0)
            {
                СleaningParenthesis();
                StartCloseSection(Sections[0]);
                EndCloseSection(Sections[Sections.Count]);
            }
        }

        protected void СleaningParenthesis()
        {
            foreach (var item in Sections)
            {
                OpenSection(item);
            }
        }

        internal virtual void GetIsVisible(bool isVisible)
        {
            foreach (var item in Sections)
            {
                item.IsVisible = isVisible;
            }
        }

        protected virtual SectionFilter CloseSection(SectionFilter sectionFilter)
        {
            sectionFilter.StartParenthesis = 1;
            sectionFilter.EndParenthesis = 1;
            sectionFilter.ExitOperator = BinaryFilterOperatorType.BOOLEAN_OR;
            return sectionFilter;
        }
        protected SectionFilter OpenSection(SectionFilter sectionFilter)
        {
            sectionFilter.StartParenthesis = 0;
            sectionFilter.EndParenthesis = 0;
            sectionFilter.ExitOperator = BinaryFilterOperatorType.BOOLEAN_AND;
            return sectionFilter;
        }
        protected SectionFilter StartCloseSection(SectionFilter sectionFilter)
        {
            sectionFilter.StartParenthesis = 1;
            sectionFilter.EndParenthesis = 0;
            sectionFilter.ExitOperator = BinaryFilterOperatorType.BOOLEAN_AND;
            return sectionFilter;
        }
        protected virtual SectionFilter EndCloseSection(SectionFilter sectionFilter)
        {
            sectionFilter.StartParenthesis = 0;
            sectionFilter.EndParenthesis = 1;
            sectionFilter.ExitOperator = BinaryFilterOperatorType.BOOLEAN_OR;
            return sectionFilter;
        }
    }
}
