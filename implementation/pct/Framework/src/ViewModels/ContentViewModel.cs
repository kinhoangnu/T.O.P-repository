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
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace com.vanderlande.wpf
{
    /// <summary>
    /// Base class for all ContentViewModels
    /// </summary>
    public partial class ContentViewModel : ValidationViewModel
    {
        #region Properties

        // In the extreme rare situations where the UI framework element is required, it can be obtained from the ViewModel
        // An example could be where the 3D camera position needs to be adjusted, based on (non) visual objects.
        public FrameworkElement Element { get; private set; }

        public bool IsVisible { get; private set; }

        public static bool IsDeveloper
        {
            get { return User.Current.Role.CurrentRole == UserRole.Developer; }
        }

        public ContentViewModel Parent { get; private set; }
        private readonly List<ContentViewModel> _childViewModels = new List<ContentViewModel>();
        public IReadOnlyList<ContentViewModel> Children { get { return _childViewModels.AsReadOnly(); } }
        
        #endregion

        #region Public methods

        /// <summary>
        /// Attach this viewmodel to the supplied framework element.
        /// A.k.a. set the DataContext of the window, grid, ... to this viewmodel.
        /// </summary>
        /// <param name="element"></param>
        public virtual void Attach(FrameworkElement element)
        {
            if (Element != element)
            {
                DetachEventHandlers();
                Element = element;
                AttachEventHandlers();
            }
            if (element != null)
            {
                element.DataContext = this;
            }
        }



        /// <summary>
        /// Can the viewmodel be unloaded.
        /// </summary>
        /// <param name="closing">True when the application want to close.
        /// False when another viewmodel wants to be displayed.</param>
        /// <returns>True when it is valid to unload this viewmodel and children.</returns>
        public virtual bool CanUnload(bool closing)
        {
            return _childViewModels.All(cv => cv.CanUnload(closing) != false);
        }

        /// <summary>
        /// Can the user close the page.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanClosePage()
        {
            return (ViApplication.Instance.CanClosePages == true) && (CanUnload(false));
        }


        /// <summary>
        /// Simulate as if the user presses the close page button.
        /// There is no check if the page can be closed.
        /// </summary>
        public virtual void Close()
        {
            ViApplication.Instance.MainWindowViewModel.ClosePageCommand.Execute(null);
        }


        public void AddChild(ContentViewModel vm)
        {
            if ((vm == null) || (vm.Parent == this))
                return;
            if (vm.Parent != null)
                vm.Parent.RemoveChild(vm);
            vm.Parent = this;
            _childViewModels.Add(vm);
        }


        public void RemoveChild(ContentViewModel vm)
        {
            if ((vm == null) || (vm.Parent != this))
                return;
            vm.Parent = null;
            _childViewModels.Remove(vm);
        }


        #endregion

        #region Protected methods

        protected ContentViewModel()
        {
            InitializeValidation();
        }


        // Update the properties in the element (e.g. language has changed).
        protected override void RefreshProperties()
        {
            base.RefreshProperties();
            RefreshProperties(Element);
        }


        #endregion

    }
}
