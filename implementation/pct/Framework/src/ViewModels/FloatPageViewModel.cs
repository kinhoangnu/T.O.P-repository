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

using System.Windows;

namespace com.vanderlande.wpf
{
    internal class FloatPageViewModel : WindowViewModel
    {
        private ContentViewModel _viewModel;
        public ContentViewModel ViewModel
        {
            get { return _viewModel; }
            private set
            {
                if (ChangeProperty(ref _viewModel, value))
                {
                    AddChild(_viewModel);
                }
            }
        }

        private FrameworkElement _content;
        public FrameworkElement Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (ChangeProperty(ref _content, value))
                {
                    if (_content != null)
                    {
                        ViewModel = _content.DataContext as ContentViewModel;
                    }
                }
            }
        }

    }
}
