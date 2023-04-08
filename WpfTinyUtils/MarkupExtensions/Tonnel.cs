using Pather.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;
using WpfTinyUtils.Infrastructure;

namespace WpfTinyUtils.MarkupExtensions
{
    /// <summary>
    /// Extends x:Reference functionality by adding possibility to find something relatively to reference
    /// </summary>
    public class Tonnel : MarkupExtension
    {
        private uint ancestorLevel = 1;
        #region Public Properties

        public TonnelMode Mode { get; set; } = TonnelMode.RelativeSourceLike;
        public Type? AncestorType { get; set; } = null;
        public string? AncestorName { get; set; } = null;
        public FrameworkElement? SearchInitialPoint { get; set; } = null;
        public string? Path { get; set; }
        public IValueConverter? Converter { get; set; }
        public object? ConverterParameter { get; set; }
        public uint AncestorLevel
        {
            get => ancestorLevel;
            set
            {
                if (value > 0)
                    ancestorLevel = value;
            }
        }

        #endregion

        public Tonnel()
        { }

        #region Overridden Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (SearchInitialPoint == null)
            {
                var isConstructed = false;
                var target = serviceProvider.TryGetTarget(out isConstructed);
                if (!isConstructed)
                    throw new Exception("Unable to get SearchInitial point");
                if (!(target!.TargetObject is FrameworkElement))
                    throw new Exception("Implisitly setted SearchInitialPoint is not framework element. " +
                        "In this case you should explicitly set SearchInitialPoint using x:Reference Markup Extention " +
                        "or any other way provide value that derrived from FrameworkElement to SearchInitialPoint property.");
                SearchInitialPoint = (FrameworkElement)target.TargetObject!;
            }
            var resolver = new Resolver();
            object? targetObj;
            switch (Mode)
            {
                case TonnelMode.FindName:
                    if (AncestorName == null)
                        throw new Exception("AncestorName shouldn't be null");
                    targetObj = FindAncestorByName(AncestorName, SearchInitialPoint);
                    break;
                case TonnelMode.RelativeSourceLike:
                    if (AncestorType == null)
                        throw new Exception("AncestorType shouldn't be null");
                    targetObj = FindAncestor(AncestorLevel, AncestorType, SearchInitialPoint);
                    break;
                default:
                    throw new Exception("Unknown Mode");
            }
            object? rawValue;
            if (Path == null || targetObj == null)
                rawValue = targetObj!;
            else
                rawValue = resolver.Resolve(targetObj, Path);
            if (Converter == null)
                return rawValue;
            return Converter.Convert(rawValue, typeof(object), ConverterParameter, null);
        }

        #endregion

        protected object FindAncestor(uint ancestorLevel, Type ancestorType, FrameworkElement searchInitialPoint)
        {
            FrameworkElement? result = null;
            while (result == null && (searchInitialPoint.Parent != null || searchInitialPoint.TemplatedParent != null))
            {
                if (searchInitialPoint.Parent == null)
                {
                    searchInitialPoint = (FrameworkElement)searchInitialPoint.TemplatedParent;
                    if (searchInitialPoint.GetType() == ancestorType)
                    {
                        ancestorLevel--;
                        if (ancestorLevel == 0)
                        {
                            result = searchInitialPoint;
                            break;
                        }
                    }
                    continue;
                }
                searchInitialPoint = (FrameworkElement)searchInitialPoint.Parent;
                if (searchInitialPoint.GetType() == ancestorType)
                {
                    ancestorLevel--;
                    if (ancestorLevel == 0)
                    {
                        result = searchInitialPoint;
                        break;
                    }
                }
            }
            return result!;
        }

        protected object FindAncestorByName(string AncestorName, FrameworkElement searchInitialPoint)
        {
            FrameworkElement? result = null;
            while (result == null && (searchInitialPoint.Parent != null || searchInitialPoint.TemplatedParent != null))
            {
                if (searchInitialPoint.Parent == null)
                {
                    searchInitialPoint = (FrameworkElement)searchInitialPoint.TemplatedParent;
                    if (searchInitialPoint.Name == AncestorName)
                    {
                        result = searchInitialPoint;
                        break;
                    }
                }
                searchInitialPoint = (FrameworkElement)searchInitialPoint.Parent;
                if (searchInitialPoint.Name == AncestorName)
                {
                    result = searchInitialPoint;
                    break;
                }
            }
            return result!;
        }
    }

    public enum TonnelMode
    {
        FindName,
        RelativeSourceLike
    }
}
