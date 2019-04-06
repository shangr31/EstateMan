using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EstateManager.Behavior
{
    class AlphabeticalTextBoxBehavior : Behavior<TextBox>
    {
        private string _lastText;



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
                inputScope.Names.Add(new InputScopeName(InputScopeNameValue.Default));
                AssociatedObject.InputScope = inputScope;
            }
        }

        private void TbOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex(@"[1-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (AssociatedObject != null)
            {
                int carCount = AssociatedObject.Text.Length - _lastText.Length;
                int lastSelectionStart = AssociatedObject.SelectionStart - carCount;
                if (lastSelectionStart < 0) lastSelectionStart = 0;
                if (lastSelectionStart > _lastText.Length) lastSelectionStart = _lastText.Length;


                if (string.IsNullOrEmpty(AssociatedObject.Text)||!rx.IsMatch(AssociatedObject.Text))
                {
                        _lastText = AssociatedObject.Text ;
                        return;
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

