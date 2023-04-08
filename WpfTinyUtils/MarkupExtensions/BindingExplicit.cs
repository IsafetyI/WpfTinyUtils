using WpfTinyUtils.Infrastructure;
using Pather.CSharp;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfTinyUtils.MarkupExtensions
{
    /// <summary>
    /// Behaves like default binding, but you can explicitly set binding source through xaml markup.
    /// </summary>
    public class BindingExplicit : MarkupExtension
    {
        private Binding binding;
        private object converterParameter;

        public string Path { get; set; }
        public BindingMode Mode { get; set; }
        public IValueConverter Converter { get; set; }
        public object ConverterParameter
        {
            get => converterParameter;
            set => converterParameter = value;
        }
        public object Source { get; set; }



        public BindingExplicit()
        {
            binding = new Binding();
            binding.Source = Source;
            binding.ConverterParameter = ConverterParameter;
            binding.Mode = Mode;
            binding.Converter = Converter;
            binding.Path = new PropertyPath(Path, new object[] { });
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (binding.Source == null)
                binding.Source = serviceProvider.TryGetTarget(out bool _)?.TargetObject;
            return binding.ProvideValue(serviceProvider);
        }
    }
}
