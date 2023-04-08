﻿using DevExpress.Mvvm.UI.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfTinyUtils.Infrastructure;

namespace WpfTinyUtils.Behaviors
{
    public class MinimizeButtonBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += OnClick;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnClick;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var window = VisualTreeParentFinder.FindParent<Window>(AssociatedObject);
            if(window != null)
                window.WindowState = WindowState.Minimized;
        }
    }
}
