/*
*  Copyright (c) 2016 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*  
*/

using System;
using System.Diagnostics;
using System.Windows;

namespace com.vanderlande.wpf
{
    public static partial class RuleResultFactory
    {
        public static RuleResult Valid()
        {
            return RuleResult.Valid();
        }

        public static RuleResult Invalid(Enum key, params object[] args)
        {
            var str = key.GetType().Name + "." + key;
            return Invalid(str, args);
        }

        public static RuleResult Invalid(string key, params object[] args)
        {
            var cnt = args.Length;
            Debug.Assert(cnt % 2 == 0, string.Format("{0} must have a key-value pair", key));

            var str = ("Validator." + key).ToResourceString();
            for (var i = 1; i < cnt; i += 2)
            {
                var arg = "{" + args[i - 1] + "}";
                var value = Translate(args[i].ToString());
                str = str.Replace(arg, value);
            }

            return RuleResult.Invalid(str);
        }

        private static string Translate(string key)
        {
            var resource = Application.Current.TryFindResource(key);
            if (resource != null)
            {
                return resource.ToString();
            }
            return key;
        }
    }
}