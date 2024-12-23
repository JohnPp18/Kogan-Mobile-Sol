using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Runtime.Serialization;

namespace Kogan.ErpSync.IntegrationData.ValueConverters
{
    /// <summary>
    /// Converts the string representation of an enum (stored in the <see cref="EnumMemberAttribute"/>) back and forth.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public class EnumMemberToStringConverter<TEnum> : ValueConverter<TEnum, string>
        where TEnum : struct, Enum
    {
        public EnumMemberToStringConverter()
            : base(
                enumVal => EnumsNET.Enums.AsString<TEnum>(enumVal, EnumsNET.EnumFormat.EnumMemberValue),  // Convert enum to string
                stringVal => ToEnum(stringVal) // Convert string back to enum
        )
        { 
            
        }

        private static TEnum ToEnum(string str)
        {
            var enumType = typeof(TEnum);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (enumMemberAttribute.Value == str) return (TEnum)Enum.Parse(enumType, name);
            }
            //throw exception or whatever handling you want or
            return default(TEnum);
        }
    }
}
