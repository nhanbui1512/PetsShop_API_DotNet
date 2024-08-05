using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsShop_API_DotNet.Dtos.Helpers
{
    public static class PropertySetterHelper
    {
        public static void SetPropertiesToNull<T>(this T targetObject, string[] propertyNames)
        {
            if (targetObject == null || propertyNames == null || propertyNames.Length == 0)
            {
                return;
            }

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                // Kiểm tra nếu tên thuộc tính có trong mảng propertyNames và thuộc tính có thể ghi được (settable)
                if (propertyNames.Contains(property.Name) && property.CanWrite)
                {
                    var propertyType = property.PropertyType;

                    // Kiểm tra nếu thuộc tính là kiểu giá trị (struct) hoặc kiểu tham chiếu
                    if (propertyType.IsValueType && Nullable.GetUnderlyingType(propertyType) == null)
                    {
                        // Gán giá trị mặc định (default) cho kiểu giá trị
                        var defaultValue = Activator.CreateInstance(propertyType);
                        property.SetValue(targetObject, defaultValue);
                    }
                    else
                    {
                        // Gán giá trị null cho kiểu tham chiếu
                        property.SetValue(targetObject, null);
                    }
                }
            }
        }


    }
}