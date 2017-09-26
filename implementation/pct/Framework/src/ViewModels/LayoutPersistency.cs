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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace com.vanderlande.wpf
{
    internal abstract class LayoutCommand
    {
        internal abstract void Execute();
    }

    internal class GridColumnLayoutCommand : LayoutCommand
    {
        private readonly ColumnDefinition _element;
        private readonly double _width;
        private readonly GridUnitType _type;

        internal GridColumnLayoutCommand(ColumnDefinition el, double width, GridUnitType type)
        {
            _element = el;
            _width = width;
            _type = type;
        }

        internal override void Execute()
        {
            _element.Width = new GridLength(_width, _type);
        }
    }

    internal class GridRowLayoutCommand : LayoutCommand
    {
        private readonly RowDefinition _element;
        private readonly double _height;
        private readonly GridUnitType _type;

        internal GridRowLayoutCommand(RowDefinition el, double height, GridUnitType type)
        {
            _element = el;
            _height = height;
            _type = type;
        }

        internal override void Execute()
        {
            _element.Height = new GridLength(_height, _type);
        }
    }

    internal class TabItemLayoutCommand : LayoutCommand
    {
        private readonly TabItem _element;
        private readonly bool _selected;

        internal TabItemLayoutCommand(TabItem el, bool sel)
        {
            _element = el;
            _selected = sel;
        }

        internal override void Execute()
        {
            _element.IsSelected = _selected;
        }

    }

    internal class ExpanderLayoutCommand : LayoutCommand
    {
        private readonly Expander _element;
        private readonly bool _open;

        internal ExpanderLayoutCommand(Expander el, bool open)
        {
            _element = el;
            _open = open;
        }

        internal override void Execute()
        {
            _element.IsExpanded = _open;
        }

    }


    internal class LayoutPersistency
    {
        private readonly ISettingsPersistency _persistency;
        private readonly DependencyObject _rootElement;
        private List<LayoutCommand> _layoutCommands;

        internal LayoutPersistency(ISettingsPersistency sp, DependencyObject depObj)
        {
            _persistency = sp;
            _rootElement = depObj;
        }

        internal void Save()
        {
            int index = 1;
            SaveLayout(ref index, 1, _rootElement);
        }


        internal void Load()
        {
            int index = 1;
            _layoutCommands = new List<LayoutCommand>();
            if (LoadLayout(ref index, 1, _rootElement) == true)
            {
                foreach (LayoutCommand cmd in _layoutCommands)
                {
                    cmd.Execute();
                }
            }
            _layoutCommands = null;
        }

        /// <summary>
        /// Save a dependancy object using the settings persistency interface.
        /// If saved, the index is increased.
        /// The depth is a check to indicate if the layout of the XAML has changed.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="depth"></param>
        /// <param name="depObj"></param>
        private void SaveLayout(ref int index, int depth, DependencyObject depObj)
        {
            if ((SaveLayout(index, depth, depObj as GridSplitter) == true) ||
                (SaveLayout(index, depth, depObj as TabItem) == true) ||
                (SaveLayout(index, depth, depObj as Expander) == true))
                ++index;
            if (depObj != null)
                foreach(object child in LogicalTreeHelper.GetChildren(depObj))
                    SaveLayout(ref index, depth + 1, child as DependencyObject);
        }

        // When a GridSplitter is detected, check the parent (Grid) Row/Column values and save them.
        private bool SaveLayout(int index, int depth, GridSplitter el)
        {
            if (el == null)
            {
                return false;
            }
            Grid grid = el.Parent as Grid;
            if (grid == null)
            {
                return false;
            }
            using (new SettingsPersistencyGroup(_persistency, "ElementLayout" + index))
            {
                int i = 1;
                if (el.ResizeDirection == GridResizeDirection.Columns)
                {
                    foreach (ColumnDefinition col in grid.ColumnDefinitions)
                    {
                        _persistency.Write("Size" + i, col.ActualWidth);
                        _persistency.Write("UnitType" + i, col.Width.GridUnitType);
                        ++i;
                    }
                }
                else if (el.ResizeDirection == GridResizeDirection.Rows)
                {
                    foreach (RowDefinition row in grid.RowDefinitions)
                    {
                        _persistency.Write("Size" + i, row.ActualHeight);
                        _persistency.Write("UnitType" + i, row.Height.GridUnitType);
                        ++i;
                    }
                }
                else
                {   
                    return false;                   // Can not save/load splitters with Auto direction
                }
                _persistency.Write("Type", el.GetType().Name);
                _persistency.Write("Depth", depth);
                _persistency.Write("ResizeDirection", el.ResizeDirection);
            }
            return true;
        }


        private bool SaveLayout(int index, int depth, TabItem el)
        {
            if (el == null)
            {
                return false;
            }
            using (new SettingsPersistencyGroup(_persistency, "ElementLayout" + index))
            {
                _persistency.Write("Type", el.GetType().Name);
                _persistency.Write("Depth", depth);
                _persistency.Write("IsSelected", el.IsSelected);
            }
            return true;
        }


        private bool SaveLayout(int index, int depth, Expander el)
        {
            if (el == null)
            {
                return false;
            }
            using (new SettingsPersistencyGroup(_persistency, "ElementLayout" + index))
            {
                _persistency.Write("Type", el.GetType().Name);
                _persistency.Write("Depth", depth);
                _persistency.Write("IsExpanded", el.IsExpanded);
            }
            return true;
        }


        private bool LoadLayout(ref int index, int depth, DependencyObject depObj)
        {
            if (depObj == null)
            {
                return true;
            }
            if (depth > 100)            // To avoid stack overflow on unknown items (or programming error)
            {
                return false;
            }
            bool valid = true;
            valid &= LoadLayout(ref index, depth, depObj as GridSplitter);
            valid &= LoadLayout(ref index, depth, depObj as TabItem);
            valid &= LoadLayout(ref index, depth, depObj as Expander);
            foreach (object child in LogicalTreeHelper.GetChildren(depObj))
            {
                if (valid == false)
                {
                    break;
                }
                valid &= LoadLayout(ref index, depth + 1, child as DependencyObject);
            }
            return valid;
        }


        // When a GridSplitter is detected, check the parent (Grid) Row/Column values and load them.
        private bool LoadLayout(ref int index, int depth, GridSplitter el)
        {
            if (el == null)
            {
                return true;
            }
            Grid grid = el.Parent as Grid;
            if (grid == null)
            {
                return true;
            }
            using (new SettingsPersistencyGroup(_persistency, "ElementLayout" + index))
            {
                if (IsDepthCorrect(depth) == false)
                {
                    return false;
                }
                string str;
                if (_persistency.Read("Type", out str) == false)
                {
                    return false;
                }
                if (str != el.GetType().Name)
                {
                    return false;
                }
                GridResizeDirection dir;
                if (_persistency.Read("ResizeDirection", out dir) == false)
                {
                    return false;
                }
                if (dir != el.ResizeDirection)
                {
                    return false;
                }
                double size;
                GridUnitType unitType;
                int i = 1;
                if (dir == GridResizeDirection.Columns)
                {
                    foreach (ColumnDefinition col in grid.ColumnDefinitions)
                    {
                        if ((_persistency.Read("Size" + i, out size) == false) ||
                            (_persistency.Read("UnitType" + i, out unitType) == false))
                        {
                            return false;
                        }
                        _layoutCommands.Add(new GridColumnLayoutCommand(col, Math.Max(size, el.Width), unitType));
                        ++i;
                    }
                }
                else if (dir == GridResizeDirection.Rows)
                {
                    foreach (RowDefinition row in grid.RowDefinitions)
                    {
                        if ((_persistency.Read("Size" + i, out size) == false) ||
                            (_persistency.Read("UnitType" + i, out unitType) == false))
                        {
                            return false;
                        }
                        _layoutCommands.Add(new GridRowLayoutCommand(row, Math.Max(size, el.Height), unitType));
                        ++i;
                    }
                }
                else
                {
                    return false;                   // Can not save/load splitters with Auto direction
                }
            }
            ++index;
            return true;
        }


        private bool LoadLayout(ref int index, int depth, TabItem el)
        {
            if (el == null)
            {
                return true;
            }
            using (new SettingsPersistencyGroup(_persistency, "ElementLayout" + index))
            {
                if (IsDepthCorrect(depth) == false)
                {
                    return false;
                }
                string str;
                if (_persistency.Read("Type", out str) == false)
                {
                    return false;
                }
                if (str != el.GetType().Name)
                {
                    return false;
                }
                bool value;
                if (_persistency.Read("IsSelected", out value) == false)
                {
                    return false;
                }
                _layoutCommands.Add(new TabItemLayoutCommand(el, value));
            }
            ++index;
            return true;
        }


        private bool LoadLayout(ref int index, int depth, Expander el)
        {
            if (el == null)
            {
                return true;
            }
            using (new SettingsPersistencyGroup(_persistency, "ElementLayout" + index))
            {
                if (IsDepthCorrect(depth) == false)
                {
                    return false;
                }
                string str;
                if (_persistency.Read("Type", out str) == false)
                {
                    return false;
                }
                if (str != el.GetType().Name)
                {
                    return false;
                }
                bool value;
                if (_persistency.Read("IsExpanded", out value) == false)
                {
                    return false;
                }
                _layoutCommands.Add(new ExpanderLayoutCommand(el, value));
            }
            ++index;
            return true;
        }


        private bool IsDepthCorrect(int depth)
        {
            int tmp;
            if (_persistency.Read("Depth", out tmp) == false)
                return false;
            return tmp == depth;
        }

    }
}
