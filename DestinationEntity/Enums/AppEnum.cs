using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FSSRC_DataMigration.DestinationEntity.Enums
{
    public enum Month
    {

        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public class AppEnum
    {
        public static IList<DropdownModel> EnumToDropdownList<T>()
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                return null;
            }

            return (Enum.GetValues(typeof(T)).Cast<T>()
                .Select(v => new DropdownModel
                {
                    Text = GetDisplayName(v),
                    Value = Convert.ToInt32(v)
                }).ToList());
        }

        public static string GetDisplayName(object value)
        {
            var type = value.GetType();

            // Get the enum field.
            var field = type.GetField(value.ToString());
            if (field == null)
            {
                return value.ToString();
            }

            // Gets the value of the Name property on the DisplayAttribute, this can be null.
            var attributes = field.GetCustomAttribute<DisplayAttribute>();
            return attributes != null ? attributes.Name : value.ToString();
        }
    }

    public class DropdownModel
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}
