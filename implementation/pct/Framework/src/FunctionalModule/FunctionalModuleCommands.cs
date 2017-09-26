/*
*  Copyright (c) 2017 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*  
*/
namespace com.vanderlande.wpf
{
    public class ActivateFunctionalModule : CommandBaseWithResult<bool>
    {
        private readonly FunctionalModule _module;

        public ActivateFunctionalModule(FunctionalModule mod)
        {
            _module = mod;
        }

        public override void Execute()
        {
            Result = ViApplication.Instance.ActivateFunctionalModule(_module);
        }
    }


    public class DeActivateFunctionalModule : CommandBaseWithResult<bool>
    {
        private readonly FunctionalModule _module;

        public DeActivateFunctionalModule(FunctionalModule mod)
        {
            _module = mod;
        }

        public override void Execute()
        {
            Result = ViApplication.Instance.DeActivateFunctionalModule(_module);
        }
    }

}
