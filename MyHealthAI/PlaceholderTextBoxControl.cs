﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace MyHealthAI
{
    public class PlaceholderTextBoxControl : TextBox
    {

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText",
                typeof(string),
                typeof(PlaceholderTextBoxControl),
                new PropertyMetadata(string.Empty));


        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty IsEmptyProperty =
            DependencyProperty.Register("IsEmpty", typeof(bool), typeof(PlaceholderTextBoxControl),
                new PropertyMetadata(false));

        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            private set { SetValue(IsEmptyProperty, value); }
        }

        static PlaceholderTextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBoxControl), new FrameworkPropertyMetadata(typeof(PlaceholderTextBoxControl)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            UpdateIsEmpty();
            base.OnInitialized(e);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            UpdateIsEmpty();
            base.OnTextChanged(e);
        }

        private void UpdateIsEmpty()
        {
            IsEmpty = string.IsNullOrEmpty(Text);
        }
    }
}
