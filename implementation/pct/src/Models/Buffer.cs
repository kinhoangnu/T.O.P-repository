using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;
using System.Xml.Serialization;

namespace Your
{
    public class Buffer : ContentViewModel
    {
        private string uuid;
        private string b_name;
        private string b_comID;
        private string b_unit;
        private string b_description;
        private bool isSelected;
        private string _isValid;

        public string IsValid
        {
            get { return _isValid; }
            set { ChangeProperty(ref _isValid, value); }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }
                
        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        public string B_description
        {
            get
            {
                return b_description;
            }
            set
            {
                b_description = value;
            }
        }
        public string B_name
        {
            get
            {
                return b_name;
            }
            set
            {
                if (ChangeProperty(ref b_name, value) == true)
                    IsValid = IsPropertyValid() ? "valid" : "invalid";
            }
        }
        public string B_comID
        {
            get
            {
                return b_comID;
            }
            set
            {
                b_comID= value;
            }
        }
        public string B_unit
        {
            get
            {
                return b_unit;
            }
            set
            {
                b_unit= value;
            }
        }

        public Buffer()
        {
            // Add property validators in the constructor.
            Validator.AddRule(ValidateTextBlock1, () => B_name);
            Validator.AddRule(() => ValidateTextBlock2(B_name), () => B_name);

            // Initialize the property here so ChangeProperty is raised and IsPropertyValid is determined.
            B_name = "";
        }

        private RuleResult ValidateTextBlock1()
        {
            int value;
            return RuleResult.Assert(!(int.TryParse(B_name, out value)), "*Number is not allowed");
        }

        // This check is only executed when the first one succeeds.
        private RuleResult ValidateTextBlock2(string str)
        {
            return RuleResult.Assert(CheckDuplicateName(B_name), string.Format("Duplicated name!"));
        }

        bool CheckDuplicateName(string s)
        {
            int i = 0;
            foreach (Buffer b in BufferList.Buffers)
            {
                if (s == b.B_name)
                    i++;
            }
            if (i > 1)
            {
                return false;
            }
            return true;
        }
    }

}
