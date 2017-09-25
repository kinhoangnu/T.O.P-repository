/*
*  Copyright (c) 2015 Vanderlande Industries
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
using com.vanderlande.wpf;
using com.vanderlande.frs.client.viewmodel;

namespace com.vanderlande.frs.desktop
{
    public class DesktopMainWindowViewModel : MainWindowViewModel
    {
        // Viewmodels that appear at startup.
        private readonly Type[] _primaryViewModels =
        {
            typeof(DashboardViewModel),
        };


        // Viewmodels that appear after user action (like Details).
        private readonly Type[] _secondaryViewModels =
        {
            typeof(SystemViewModel),
            typeof(OrdersViewModel),
            typeof(EventsViewModel),
            typeof(ServiceZonesViewModel),
            typeof(ShuttlesOverviewViewModel),
            typeof(LiftsViewModel),
            typeof(ThreeDViewModel),
            typeof(TwoDViewModel),
            typeof(ShuttleDetailViewModel),
            typeof(LiftDetailViewModel),
            typeof(MaintenanceLocationViewModel),
            typeof(HandheldViewModel),
            typeof(SystemDashboardViewModel),
            typeof(ShuttlesViewModel),
            typeof(StyleGuideViewModel)
        };


        public override void OnCreated()
        {
            foreach (Type t in _primaryViewModels)
            {
                ActivateContent(t);
            }
            foreach (Type t in _secondaryViewModels)
            {
                RegisterContent(t);
            }
            Mediator.Default.Register<ShuttleDetailsCommand>(this, OnShuttleDetails);
            Mediator.Default.Register<LiftDetailsCommand>(this, OnLiftDetails);
            Mediator.Default.Register<OrderDetailsCommand>(this, OnOrderDetails);
            Mediator.Default.Register<ShowLevelCommand>(this, OnShowLevel);
            Mediator.Default.Register<ShowServiceZoneCommand>(this, OnShowServiceZone);
            Mediator.Default.Register<ShowSystemCommand>(this, OnShowSystem);
            Mediator.Default.Register<ShowShuttlesCommand>(this, OnShowShuttles);
            Mediator.Default.Register<ShowLiftsCommand>(this, OnShowLifts);
            Mediator.Default.Register<ShowEventsCommand>(this, OnShowEvents);
            base.OnCreated();
        }


        public override void OnDestroy()
        {
            foreach (Type t in _primaryViewModels)
            {
                UnregisterContent(t);
            }
            foreach (Type t in _secondaryViewModels)
            {
                UnregisterContent(t);
            }
            Mediator.Default.Unregister(this);
            base.OnDestroy();
        }


        private void OnShowSystem(ShowSystemCommand cmd)
        {
            ShowViewModel(typeof(SystemViewModel), cmd);
        }

        private void OnShowServiceZone(ShowServiceZoneCommand cmd)
        {
            ShowViewModel(typeof(ServiceZonesViewModel), cmd);
        }

        private void OnShowLevel(ShowLevelCommand cmd)
        {
            ShowViewModel(typeof(TwoDViewModel), cmd);
        }

        private void OnOrderDetails(OrderDetailsCommand cmd)
        {
            ShowViewModel(typeof(OrdersViewModel), cmd);
        }

        private void OnLiftDetails(LiftDetailsCommand cmd)
        {
            ShowViewModel(typeof(LiftDetailViewModel), cmd);
        }

        private void OnShuttleDetails(ShuttleDetailsCommand cmd)
        {
            ShowViewModel(typeof(ShuttleDetailViewModel), cmd);
        }

        private void OnShowShuttles(ShowShuttlesCommand cmd)
        {
            ShowViewModel(typeof(ShuttlesOverviewViewModel), cmd);
        }

        private void OnShowLifts(ShowLiftsCommand cmd)
        {
            ShowViewModel(typeof(LiftsViewModel), cmd);
        }

        private void OnShowEvents(ShowEventsCommand cmd)
        {
            ShowViewModel(typeof(EventsViewModel), cmd);
        }


        /// <summary>
        /// If a view(model) has not been activated/selected/shown, activate it and re-raise the command.
        /// Otherwise, just activated it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="cmd"></param>
        private void ShowViewModel<T>(Type vm, T cmd)
        {
            if (ViewLocator.DoesViewExist(vm) == false)
            {
                return;
            }
            bool isSelected = IsContentSelected(vm);
            ActivateContent(vm);
            if (isSelected == false)
            {
                Mediator.Default.Raise(cmd);
            }
        }
    }
}

