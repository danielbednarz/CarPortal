using API.Helpers;
using System;
using System.Collections.Generic;

namespace API.Extensions
{
    public class EnumExtensions
    {
        public static List<EnumValue> GetValues<T>()
        {
            var values = new List<EnumValue>();

            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(new EnumValue()
                {
                    Text = Enum.GetName(typeof(T), itemType),
                    Value = (int)itemType
                });
            }
            return values;
        }
        
    }
}
