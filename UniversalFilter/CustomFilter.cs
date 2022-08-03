using System;
using UniversalFilter.Controller;
using UniversalFilter.Model;
using Tekla.Structures.Filtering;

namespace UniversalFilter
{
    public sealed class CustomFilter
    {
        private FilterCollection customFilterExpression;

        public FilterCollection CustomFilterExpression
        {
            get => this.customFilterExpression;
            set => this.customFilterExpression = value != null ? value : throw new NullReferenceException();
        }
        public CustomFilter(FilterCollection FilterExpression) => this.customFilterExpression = FilterExpression != null ? FilterExpression : throw new ArgumentNullException(nameof(FilterExpression));

        public string CreateFile(FilterExpressionFileType FilterExpressionFileType, string FullFileName) => new CustomFilterGenerator().Generate(this.customFilterExpression, FilterExpressionFileType, FullFileName);
    }
}
