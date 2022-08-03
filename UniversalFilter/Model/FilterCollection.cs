using System.Collections.Generic;
using System.Collections;

namespace UniversalFilter.Model
{
    public class FilterCollection : IEnumerable
    {
        public int Count
        {
            get { return GetCount(); }
        }

        public int CountGroups { get; }
        private List<ExpressionFilterGroup> Collection { get; set; }
        public FilterCollection()
        {
            Collection = new List<ExpressionFilterGroup>() { new ReferenceFilterGroupe(), new BoltFilterGroupe(), new PartFilterGroupe() };
        }


        private int GetCount()
        {
            int rezult = 0;
            foreach (var groupe in Collection)
            {
                rezult += groupe.Count;
            }

            return rezult;
        }

        public IEnumerator GetEnumerator() => Collection.GetEnumerator();

        public ExpressionFilterGroup GetGroupe(FilterGroupeType filterGroupeType)
        {
            return Collection[(int)filterGroupeType];
        }
    }

    public enum FilterGroupeType
    {
        Reference = 0,
        Bolt = 1,
        Part = 2
    }
}
