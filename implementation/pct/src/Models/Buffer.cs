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
        private string bName;
        private string bComId;
        private string bUnit;
        private string bDescription;
        private bool isSelected;
        private string isValid;

        public string IsValid
        {
            get { return isValid; }
            set { ChangeProperty(ref isValid, value); }
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
        public string BDescription
        {
            get
            {
                return bDescription;
            }
            set
            {
                bDescription = value;
            }
        }
        public string BName
        {
            get
            {
                return bName;
            }
            set
            {
                ChangeProperty(ref bName, value);
                //if (ChangeProperty(ref bName, value) == true)
                //    IsValid = IsPropertyValid() ? "valid" : "invalid";
            }
        }
        public string BComId
        {
            get
            {
                return bComId;
            }
            set
            {
                bComId= value;
            }
        }
        public string BUnit
        {
            get
            {
                return bUnit;
            }
            set
            {
                bUnit= value;
            }
        }

        public Buffer()
        {
            // Add property validators in the constructor.
            //Validator.AddRule(ValidateTextBlock1, () => BName);
            //Validator.AddRule(() => ValidateTextBlock2(BName), () => BName);

            //// Initialize the property here so ChangeProperty is raised and IsPropertyValid is determined.
            //BName = "";
        }

        //private RuleResult ValidateTextBlock1()
        //{
        //    int value;
        //    return RuleResult.Assert(!(int.TryParse(BName, out value)), "*Number is not allowed");
        //}

        //// This check is only executed when the first one succeeds.
        //private RuleResult ValidateTextBlock2(string str)
        //{
        //    return RuleResult.Assert(CheckDuplicateName(BName), string.Format("Duplicated name!"));
        //}

        //bool CheckDuplicateName(string s)
        //{
        //    int i = 0;
        //    foreach (Buffer b in BufferList.Buffers)
        //    {
        //        if (s == b.BName)
        //            i++;
        //    }
        //    if (i > 1)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }

}
