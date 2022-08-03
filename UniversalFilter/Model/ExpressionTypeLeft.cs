
namespace UniversalFilter.Model
{
    internal abstract class ExpressionTypeLeft
    {
        public ExpressionTypeLeft(string category, string property, string localizationKey)
        {
            Category = category;
            Property = property;
            LocalizationKey = localizationKey;
        }

        public string Category { get; set; }
        public string Property { get; set; }
        public string LocalizationKey { get; set; }

    }
}
