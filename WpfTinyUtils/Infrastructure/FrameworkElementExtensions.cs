using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfTinyUtils.Infrastructure
{
    public static class FrameworkElementExtensions
    {
        public static void SetValueBindingAffected(this FrameworkElement element, DependencyProperty dp, object value)
        {
            var bindingExpr = element.GetBindingExpression(dp);
            if(bindingExpr == null)
            {
                element.SetValue(dp, value);
                return;
            }
            var binding = bindingExpr.ParentBinding;
            var converter = binding.Converter;
            var data = bindingExpr.DataItem;
            if (data == null)
            {
                element.SetValue(dp, value);
                return;
            }
            var prop = data.GetType().GetProperty(bindingExpr.ResolvedSourcePropertyName);
            if (converter != null)
                value = converter.ConvertBack(value, prop.PropertyType, null, null);
            prop?.SetValue(data, value);
            bindingExpr.UpdateTarget();
        }
    }
}
