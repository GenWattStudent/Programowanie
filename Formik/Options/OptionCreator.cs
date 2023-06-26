using System.Collections.Generic;
using System.Reflection;

namespace Formik.Options
{
    public partial class OptionCreator
    {
        public FormikOption CreateOption<T>(PropertyInfo propInfo, T entity, Dictionary<string, FormikOption>? options)
        {
            if (options is not null && options.ContainsKey(propInfo.Name) && options[propInfo.Name] is FormikOption option)
            {
                return option;
            }

            return new OptionFactory().CreateOption<T>(propInfo, entity);
        }
    }
}