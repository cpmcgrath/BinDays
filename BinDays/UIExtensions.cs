﻿using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace CMcG.BinDays
{
    public static class UIExtensions
    {
        public static void UpdateBinding(this FrameworkElement instance, DependencyProperty prop)
        {
            var bindingExpression = instance.GetBindingExpression(prop);

            if (bindingExpression != null)
                bindingExpression.UpdateSource();
        }

        public static void FinishBinding(this PhoneApplicationPage instance)
        {
            var element = FocusManager.GetFocusedElement();

            if (element is TextBox)
                UpdateBinding((TextBox)element, TextBox.TextProperty);
            else if (element is PasswordBox)
                UpdateBinding((PasswordBox)element, PasswordBox.PasswordProperty);

            instance.Focus();
        }
    }
}
