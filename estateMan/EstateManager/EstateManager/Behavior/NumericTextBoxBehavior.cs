using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EstateManager.Behavior
{
    class NumericTextBoxBehavior :Behavior<TextBox>
    {
        private string _lastText;

        public static readonly DependencyProperty AllowDecimalProperty =
            DependencyProperty.Register(nameof(AllowDecimal),
                                        typeof(bool),
                                        typeof(NumericTextBoxBehavior),
                                        new PropertyMetadata(false));
        public bool AllowDecimal
        {
            get { return (bool)GetValue(AllowDecimalProperty); }
            set { SetValue(AllowDecimalProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject == null)
            {
                throw new ArgumentException("NumericTextBoxBehavior can only be used with a TextBox.");
            }

            _lastText = AssociatedObject.Text;
            AssociatedObject.TextChanged += TbOnTextChanged;
            if (AssociatedObject.InputScope == null)
            {
                var inputScope = new InputScope();
                inputScope.Names.Add(new InputScopeName(InputScopeNameValue.Number));
                AssociatedObject.InputScope = inputScope;
            }
        }

        private void TbOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (AssociatedObject != null)
            {
                int carCount = AssociatedObject.Text.Length - _lastText.Length;
                int lastSelectionStart = AssociatedObject.SelectionStart - carCount;
                if (lastSelectionStart < 0) lastSelectionStart = 0;
                if (lastSelectionStart > _lastText.Length) lastSelectionStart = _lastText.Length;

                if (AllowDecimal)
                {
                    double value;
                    if (AssociatedObject.Text.Contains(","))
                        AssociatedObject.Text = AssociatedObject.Text.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    if (AssociatedObject.Text.Contains("."))
                        AssociatedObject.Text = AssociatedObject.Text.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    AssociatedObject.SelectionStart = lastSelectionStart + carCount;
                    if (string.IsNullOrEmpty(AssociatedObject.Text) ||
                        double.TryParse(AssociatedObject.Text, out value))
                    {
                        _lastText = AssociatedObject.Text;
                        return;
                    }
                }
                else
                {
                    long value;
                    if (string.IsNullOrEmpty(AssociatedObject.Text) ||
                        long.TryParse(AssociatedObject.Text, out value))
                    {
                        _lastText = AssociatedObject.Text;
                        return;
                    }
                }

                AssociatedObject.Text = _lastText;
                AssociatedObject.SelectionStart = lastSelectionStart;
            }
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
            {
                AssociatedObject.TextChanged -= TbOnTextChanged;
            }
        }
    }
}
