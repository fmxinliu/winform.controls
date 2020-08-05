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
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TX.Framework.WindowUI.Controls {
    [ToolboxItem(false)]
    public class MultiselectComboBoxItemList : List<MultiselectComboBoxItem> {
        public event EventHandler CheckBoxCheckedChanged;

        protected void OnCheckBoxCheckedChanged(object sender, EventArgs e) {
            EventHandler handler = CheckBoxCheckedChanged;
            if (handler != null) {
                handler(sender, e);
            }
        }

        private void Item_CheckedChanged(object sender, EventArgs e) {
            OnCheckBoxCheckedChanged(sender, e);
        }

        [Obsolete("Do not add items to this list directly. Use the ComboBox items instead.", false)]
        public new void Add(MultiselectComboBoxItem item) {
            item.CheckedChanged += new EventHandler(Item_CheckedChanged);
            base.Add(item);
        }

        public new void AddRange(IEnumerable<MultiselectComboBoxItem> collection) {
            foreach (MultiselectComboBoxItem Item in collection)
                Item.CheckedChanged += new EventHandler(Item_CheckedChanged);
            base.AddRange(collection);
        }

        public new void Clear() {
            foreach (MultiselectComboBoxItem Item in this) {
                Item.CheckedChanged -= Item_CheckedChanged;
            }
            base.Clear();
        }

        [Obsolete("Do not remove items from this list directly. Use the ComboBox items instead.", false)]
        public new bool Remove(MultiselectComboBoxItem item) {
            item.CheckedChanged -= Item_CheckedChanged;
            return base.Remove(item);
        }
    }
}