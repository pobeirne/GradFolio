namespace GradFolio.Infrastructure.Services.Helpers
{
    public class ModelCompareChecker
    {
        public static bool Compare<T>(T object1, T object2)
        {
            //Get the type of the object
            var type = typeof (T);

            //return false if any of the object is false
            if (object1 == null || object2 == null)
                return false;

            //Loop through each properties inside class and get values for the property from both the objects and compare
            foreach (var property in type.GetProperties())
            {
                if (property.Name == "ExtensionData") continue;
                var object1Value = string.Empty;
                var object2Value = string.Empty;
                if (type.GetProperty(property.Name).GetValue(object1, null) != null)
                    object1Value = type.GetProperty(property.Name).GetValue(object1, null).ToString();
                if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                    object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                if (object1Value.Trim() != object2Value.Trim())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
