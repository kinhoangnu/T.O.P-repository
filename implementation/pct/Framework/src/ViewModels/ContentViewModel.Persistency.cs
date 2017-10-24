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
    // Methods for persistency in the ContentViewModel
    public partial class ContentViewModel 
    {
        #region Protected methods

        protected virtual void SaveCurrentSession(ISettingsPersistency sp)
        {
            LayoutPersistency lp = new LayoutPersistency(sp, Element);
            lp.Export();
            for (int i = 0; i < _childViewModels.Count; ++i)
            {
                using (new SettingsPersistencyGroup(sp, "ChildViewModel" + i))
                    _childViewModels[i].SaveCurrentSession(sp);
            }
        }

        
        protected virtual void LoadPreviousSession(ISettingsPersistency sp)
        {
            LayoutPersistency lp = new LayoutPersistency(sp, Element);
            lp.Import();
            for (int i = 0; i < _childViewModels.Count; ++i)
            {
                using (new SettingsPersistencyGroup(sp, "ChildViewModel" + i))
                    _childViewModels[i].LoadPreviousSession(sp);
            }
        }

        #endregion

        #region Private methods

        private string GetPersistencyGroup()
        {
            return (GetType().FullName.Replace(".", "_"));
        }

        private void SaveLayout()
        {
            using (ISettingsPersistency sp = new SettingsPersistencyXML())
               using (new SettingsPersistencyGroup(sp, "ContentLayout"))
                    using (new SettingsPersistencyGroup(sp, GetPersistencyGroup()))
                        SaveCurrentSession(sp);
        }

        private void LoadLayout()
        {
            using (ISettingsPersistency sp = new SettingsPersistencyXML())
                using (new SettingsPersistencyGroup(sp, "ContentLayout"))
                    using (new SettingsPersistencyGroup(sp, GetPersistencyGroup()))
                        LoadPreviousSession(sp);
        }

        #endregion

    }
}
