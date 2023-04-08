using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WpfTinyUtils.Infrastructure
{
    public static class IServiceProviderExtensions
    {
        public static IProvideValueTarget? TryGetTarget(this IServiceProvider serviceProvider, out bool IsCompletelyConstructed) 
        {
            IProvideValueTarget? provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (provideValueTarget == null)
            {
                IsCompletelyConstructed = false;
                return provideValueTarget;
            }
            object targetObject = provideValueTarget.TargetObject;
            if (targetObject == null)
            {
                IsCompletelyConstructed = false;
                return provideValueTarget;
            }
            object targetProperty = provideValueTarget.TargetProperty;
            if (targetProperty == null)
            {
                IsCompletelyConstructed = false;
                return provideValueTarget;
            }
            IsCompletelyConstructed = true;
            return provideValueTarget;
        }
    }
}
