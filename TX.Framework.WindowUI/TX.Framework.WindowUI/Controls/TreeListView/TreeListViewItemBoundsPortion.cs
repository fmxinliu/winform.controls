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
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls {
    /// <summary>
    /// Specifies a portion of the tree list view item from which to retrieve the bounding rectangle
    /// </summary>
    [Serializable]
    public enum TreeListViewItemBoundsPortion {
        /// <summary>
        /// The bounding rectangle of the entire item, including the icon, the item text, and the subitem text (if displayed), should be retrieved
        /// </summary>
        Entire = (int) ItemBoundsPortion.Entire,
        /// <summary>
        /// The bounding rectangle of the icon or small icon should be retrieved
        /// </summary>
        Icon = (int) ItemBoundsPortion.Icon,
        /// <summary>
        /// The bounding rectangle specified by the Entire value without the subitems
        /// </summary>
        ItemOnly = (int) ItemBoundsPortion.ItemOnly,
        /// <summary>
        /// The bounding rectangle of the item text should be retrieved
        /// </summary>
        Label = (int) ItemBoundsPortion.Label,
        /// <summary>
        /// The bounding rectangle of the item plus minus
        /// </summary>
        PlusMinus = 4
    }
}