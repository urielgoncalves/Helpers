using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace KeyValueEnum
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get one description
            string description = EnumHelper<ArchitectureEnum>.GetEnumDescription(ArchitectureEnum.x86.ToString());

            //Get all enum descriptions
            IDictionary<string, string> allEnumDescription = EnumHelper<ArchitectureEnum>.GetAllEnumDescription();

            foreach (var item in allEnumDescription)
                Console.WriteLine(item);

            Console.ReadKey();
        }
    }

    public enum ArchitectureEnum
    {
        [Description("It's x86")]
        x86,
        [Description("It's x64")]
        x64
    }
    
    public static class EnumHelper<T>
    {
        //http://www.extensionmethod.net/1779/csharp/enum/getenumdescription
        public static string GetEnumDescription(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
                return string.Empty;

            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static IDictionary<string, string> GetAllEnumDescription()
        {
            var enumDescriptionDictionary = new Dictionary<string, string>();

            foreach (T value in Enum.GetValues(typeof(T)))
            {
                enumDescriptionDictionary.Add(value.ToString(), GetEnumDescription(value.ToString()));
            }

            return enumDescriptionDictionary;
        }
    }
}