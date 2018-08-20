using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLib
{
    public static class Extensions
    {
        // Возвращает объект перечисления, по строке
        public static Enum StringToEnum(this string value, Enum enumeration)
        {
            var type = enumeration.GetType();
            var result = Enum.GetNames(type).Where(n => n == value);
            if (result.Count() == 0)
                return null;
            else
                return (Enum)Enum.Parse(type, result.ToArray()[0]);
        }
        public static string[] EnumToStringArr(this Enum enumeration)
        {
            var type = enumeration.GetType();
            return Enum.GetNames(type).ToArray(); //Добавить проверку на пустое перечисление (может быть)
        }

    }
}
