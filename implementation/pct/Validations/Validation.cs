
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;

namespace Your
{
    class Validation : ContentViewModel
    {
        private string _textBlock1;
        public string TextBlock1
        {
            get { return _textBlock1; }
            set
            {
                if (ChangeProperty(ref _textBlock1, value) == true)
                    IsValid = IsPropertyValid() ? "valid" : "invalid";
            }
        }

        private string _isValid;
        public string IsValid
        {
            get { return _isValid; }
            set { ChangeProperty(ref _isValid, value); }
        }

        public Validation()
        {
            // Add property validators in the constructor.
            Validator.AddRule(ValidateTextBlock1, () => TextBlock1);
            Validator.AddRule(() => ValidateTextBlock2(TextBlock1, 5, 10), () => TextBlock1);

            // Initialize the property here so ChangeProperty is raised and IsPropertyValid is determined.
            TextBlock1 = "An undefined value";
        }

        private RuleResult ValidateTextBlock1()
        {
            int value;
            return RuleResult.Assert(int.TryParse(TextBlock1, out value), "Value is not a number");
        }

        // This check is only executed when the first one succeeds.
        private RuleResult ValidateTextBlock2(string str, int min, int max)
        {
            int value = int.Parse(str);
            return RuleResult.Assert((value >= min) && (value <= max), string.Format("Value outside {0} - {1}", min, max));
        }
    }
}
