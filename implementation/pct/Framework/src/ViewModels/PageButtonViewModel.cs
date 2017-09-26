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
namespace com.vanderlande.wpf
{
    public abstract class PageButtonViewModel : ContentViewModel 
    {
        public override sealed void OnCreated()
        {
            base.OnCreated();
        }

        public override sealed void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void OnLoaded()
        {
            OnCreated();
            base.OnLoaded();
        }

        public override void OnUnloaded()
        {
            base.OnUnloaded();
            OnDestroy();
        }

    }
}
