using System;
using System.Windows;
using System.Windows.Media;

namespace WpfTinyUtils.Infrastructure
{
    public static class VisualTreeParentFinder
    {
        public static DependencyObject? FindParent(Type parentType, DependencyObject child)
        {
            return FindParent(parentType, child, out int _);
        }

        public static DependencyObject? FindParent(Type parentType, DependencyObject child, out int level)
        {
            level = -1;
            if (child == null)
                return null;
            DependencyObject? foundParent = null;
            int i = 1;
            var currentParent = VisualTreeHelper.GetParent(child);
            do
            {
                var frameworkElement = currentParent as FrameworkElement;
                if (frameworkElement.GetType().IsAssignableTo(parentType))
                {
                    foundParent = currentParent;
                    level = i;
                    break;
                }
                i++;
                currentParent = VisualTreeHelper.GetParent(currentParent);
            } while (currentParent != null);
            return foundParent;
        }

        public static DependencyObject? FindParentWithName(Type parentType, DependencyObject child, string name)
        {
            return FindParentWithName(parentType, child, name, out int _);
        }

        public static DependencyObject? FindParentWithName(Type parentType, DependencyObject child, string name, out int level)
        {
            level = -1;
            if (child is null || name is null || name.Trim() == "")
                return null;
            DependencyObject? foundParent = null;
            int i = 1;
            var currentParent = VisualTreeHelper.GetParent(child);
            if (currentParent != null)
                do
                {
                    var frameworkElement = currentParent as FrameworkElement;
                    if (frameworkElement.Name == name && frameworkElement.GetType().IsAssignableTo(parentType))
                    {
                        foundParent = currentParent;
                        level = i;
                        break;
                    }
                    i++;
                    currentParent = VisualTreeHelper.GetParent(currentParent);
                } while (currentParent != null);
            return foundParent;
        }

        public static T? FindParent<T>(DependencyObject child)
        where T : DependencyObject
        {
            return (T?)FindParent(typeof(T), child);
        }

        public static T? FindParent<T>(DependencyObject child, out int level)
        where T : DependencyObject
        {
            return (T?)FindParent(typeof(T), child, out level);
        }

        public static DependencyObject? FindParentWithName(DependencyObject child, string name)
        {
            return FindParentWithName(child, name, out int _);
        }

        public static DependencyObject? FindParentWithName(DependencyObject child, string name, out int level)
        {
            level = -1;
            if (child is null || name is null || name.Trim() == "")
                return null;
            DependencyObject? foundParent = null;
            int i = 1;
            var currentParent = VisualTreeHelper.GetParent(child);
            do
            {
                var frameworkElement = currentParent as FrameworkElement;
                if (frameworkElement.Name == name)
                {
                    foundParent = (FrameworkElement?)currentParent;
                    level = i;
                    break;
                }
                i++;
                currentParent = VisualTreeHelper.GetParent(currentParent);
            } while (currentParent != null);
            return foundParent;
        }

        public static T? FindParentWithName<T>(DependencyObject child, string name)
            where T : DependencyObject
        {
            return (T?)FindParentWithName(typeof(T), child, name);
        }

        public static T? FindParentWithName<T>(DependencyObject child, string name, out int level)
            where T : DependencyObject
        {
            return (T?)FindParentWithName(typeof(T), child, name, out level);
        }
    }
}
