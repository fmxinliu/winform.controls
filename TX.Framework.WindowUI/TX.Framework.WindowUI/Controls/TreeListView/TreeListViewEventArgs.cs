#region COPYRIGHT
//
//     THIS IS GENERATED BY TEMPLATE
//     
//     AUTHOR  :     ROYE
//     DATE       :     2010
//
//     COPYRIGHT (C) 2010, TIANXIAHOTEL TECHNOLOGIES CO., LTD. ALL RIGHTS RESERVED.
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TX.Framework.WindowUI.Controls {
    public delegate void TreeListViewEventHandler(object sender, TreeListViewEventArgs e);
    public delegate void TreeListViewCancelEventHandler(object sender, TreeListViewCancelEventArgs e);

    [Serializable]
    public class TreeListViewEventArgs : EventArgs {
        private TreeListViewItem _Item;
        private TreeListViewAction _Action;

        public TreeListViewEventArgs(TreeListViewItem item, TreeListViewAction action) {
            _Item = item;
            _Action = action;
        }

        public TreeListViewItem Item {
            get { return _Item; }
        }

        public TreeListViewAction Action {
            get { return _Action; }
        }
    }

    [Serializable]
    public class TreeListViewCancelEventArgs : TreeListViewEventArgs {
        private bool _Cancel = false;

        public TreeListViewCancelEventArgs(TreeListViewItem item, TreeListViewAction action) : base(item, action) { }

        public bool Cancel {
            get { return _Cancel; }
            set { _Cancel = value; }
        }
    }
}
