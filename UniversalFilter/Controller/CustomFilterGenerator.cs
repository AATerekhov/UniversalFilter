using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UniversalFilter.Model;
using TSF = Tekla.Structures.Filtering;

namespace UniversalFilter.Controller
{
    internal sealed class CustomFilterGenerator
    {
        public const string BASIC_FILTER_EXPRESSION_START_SPACES = "        ";
        public const string BASIC_FILTER_EXPRESSION_END_SPACES = " ";
        public const string BINARY_FILTER_EXPRESSION_START_SPACES = "    ";
        public const string BINARY_FILTER_EXPRESSION_END_SPACES = " ";
        public const string HEADER_FILE_VERSION = "Version= 1.05";
        public const int MAXIMUM_NESTED_EXPRESSION_NUMBER = 3;
        private readonly Stack<TSF.BinaryFilterOperatorType> _OperatorsStack = new Stack<TSF.BinaryFilterOperatorType>();
        private FilterCollection _RootFilterExpression;

        public CustomFilterGenerator()
        {

        }
        public string Generate(
      FilterCollection FilterExpression,
      TSF.FilterExpressionFileType FilterExpressionFileType,
      string FullFileName)
        {
            if (string.IsNullOrEmpty(FullFileName))
                throw new ArgumentNullException(nameof(FullFileName));
            if (FilterExpression == null)
                throw new ArgumentNullException(nameof(FilterExpression));
            FullFileName += this.GetFilterExpressionFileExtension(FilterExpressionFileType);
            using (StreamWriter streamWriter = new StreamWriter(FullFileName, false, Encoding.GetEncoding("windows-1251")))
                this.Generate(FilterExpression, (TextWriter)streamWriter);
            return FullFileName;
        }
        protected void Generate(FilterCollection filterCollection, TextWriter TextWriter)
        {
            this._RootFilterExpression = filterCollection;
            int Count = filterCollection.Count;

            TextWriter.WriteLine(string.Format("{0}{1}", (object)"TITLE_OBJECT_GROUP", (object)" "));
            TextWriter.WriteLine("{");
            TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"    ", (object)"Version= 1.05", (object)" "));
            TextWriter.WriteLine(string.Format("{0}{1}{2}{3}", (object)"    ", (object)"Count= ", (object)Count, (object)" "));
            GenerateExpression(filterCollection, TextWriter);
            TextWriter.WriteLine(string.Format("{0}{1}", (object)"    ", (object)"}"));
        }

        protected void GenerateBinaryFilterExpression(
           SectionFilter BinaryFilterExpression,
          TextWriter TextWriter)
        {
            TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"    ", (object)"SECTION_OBJECT_GROUP", (object)" "));
            TextWriter.WriteLine(string.Format("{0}{1}", (object)"    ", (object)"{"));
            TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)BinaryFilterExpression.StartParenthesis, (object)" "));
            TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)(BinaryFilterExpression.IsVisible ? 1 : 0), (object)" "));
            TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)(BinaryFilterExpression.Left.Category), (object)" "));
            TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)(BinaryFilterExpression.Left.Property), (object)" "));
            TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)(BinaryFilterExpression.Left.LocalizationKey), (object)" "));
            this.GenerateOperatorType(BinaryFilterExpression.Operator, TextWriter);
            TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)(BinaryFilterExpression.Right.Right), (object)" "));
            TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)BinaryFilterExpression.EndParenthesis, (object)" "));
            this.GenerateBinaryFilterItemOperatorType(BinaryFilterExpression.ExitOperator, TextWriter);
            TextWriter.WriteLine(string.Format("{0}{1}", (object)"        ", (object)"}"));
        }

        protected void GenerateExpression(FilterCollection Expression, TextWriter TextWriter)
        {
            foreach (ExpressionFilterGroup groupe in Expression)
            {
                foreach (SectionFilter secton in groupe)
                {
                    GenerateBinaryFilterExpression(secton, TextWriter);
                }
            }
        }

        //Запись в TextWriter строк Типов операторов. Можно использоваться без изменений.
        protected void GenerateOperatorType(TSF.OperatorType OperatorType, TextWriter TextWriter)
        {
            switch (OperatorType)
            {
                case TSF.OperatorType.IS_EQUAL:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"==", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_Equals", (object)" "));
                    break;
                case TSF.OperatorType.IS_NOT_EQUAL:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"!=", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_DoesNotEqual", (object)" "));
                    break;
                case TSF.OperatorType.BOOLEAN_OR:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"||", (object)" "));
                    break;
                case TSF.OperatorType.BOOLEAN_AND:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"&&", (object)" "));
                    break;
                case TSF.OperatorType.SMALLER_THAN:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"<", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_LessThan", (object)" "));
                    break;
                case TSF.OperatorType.SMALLER_OR_EQUAL:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"<=", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_LessOrEqual", (object)" "));
                    break;
                case TSF.OperatorType.GREATER_THAN:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)">", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_GreaterThan", (object)" "));
                    break;
                case TSF.OperatorType.GREATER_OR_EQUAL:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)">=", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_GreaterOrEqual", (object)" "));
                    break;
                case TSF.OperatorType.CONTAINS:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"*CONTAINS*", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_Contains", (object)" "));
                    break;
                case TSF.OperatorType.NOT_CONTAINS:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"*NOTCONTAINS*", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_DoesNotContain", (object)" "));
                    break;
                case TSF.OperatorType.STARTS_WITH:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"*BEGINS*", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_BeginsWith", (object)" "));
                    break;
                case TSF.OperatorType.NOT_STARTS_WITH:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"*NOTBEGINS*", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_DoesNotBeginW", (object)" "));
                    break;
                case TSF.OperatorType.ENDS_WITH:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"*ENDS*", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_EndsWith", (object)" "));
                    break;
                case TSF.OperatorType.NOT_ENDS_WITH:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"*NOTENDS*", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_DoesNotEnd", (object)" "));
                    break;
                case TSF.OperatorType.EARLIER_THAN:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"<", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_EarlierThan", (object)" "));
                    break;
                case TSF.OperatorType.EARLIER_OR_EQUAL:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"<=", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_EarlierOrEqual", (object)" "));
                    break;
                case TSF.OperatorType.LATER_THAN:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)">", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_LaterThan", (object)" "));
                    break;
                case TSF.OperatorType.LATER_OR_EQUAL:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)">=", (object)" "));
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"albl_LaterOrEqual", (object)" "));
                    break;
                default:
                    throw new NotSupportedException(string.Format("{0} is not supported.", (object)OperatorType));
            }
        }

        protected void GenerateBinaryFilterItemOperatorType(
           TSF.BinaryFilterOperatorType BinaryFilterItemOperatorType,
          TextWriter TextWriter)
        {
            switch (BinaryFilterItemOperatorType)
            {
                case TSF.BinaryFilterOperatorType.BOOLEAN_OR:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"||", (object)" "));
                    break;
                case TSF.BinaryFilterOperatorType.BOOLEAN_AND:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"&&", (object)" "));
                    break;
                case TSF.BinaryFilterOperatorType.EMPTY:
                    TextWriter.WriteLine(string.Format("{0}{1}{2}", (object)"        ", (object)"Empty", (object)" "));
                    break;
                default:
                    throw new NotSupportedException(string.Format("{0} is not supported.", (object)BinaryFilterItemOperatorType));
            }
        }

        /// <summary>
        /// Назначение расширения файла в зависимости от типа фильтра.
        /// </summary>
        /// <param name="FilterExpressionFileType"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        protected string GetFilterExpressionFileExtension(
           TSF.FilterExpressionFileType FilterExpressionFileType)
        {
            switch (FilterExpressionFileType)
            {
                case TSF.FilterExpressionFileType.OBJECT_GROUP_SELECTION:
                    return ".SObjGrp";
                case TSF.FilterExpressionFileType.OBJECT_GROUP_VIEW:
                    return ".VObjGrp";
                case TSF.FilterExpressionFileType.DRAWING_SINGLE_PART:
                    return ".wdf";
                case TSF.FilterExpressionFileType.DRAWING_ASSEMBLY:
                    return ".adf";
                case TSF.FilterExpressionFileType.DRAWING_CAST_UNIT:
                    return ".cuf";
                case TSF.FilterExpressionFileType.DRAWING_GENERAL:
                    return ".gdf";
                default:
                    throw new NotSupportedException(FilterExpressionFileType.ToString());
            }
        }
    }
}
