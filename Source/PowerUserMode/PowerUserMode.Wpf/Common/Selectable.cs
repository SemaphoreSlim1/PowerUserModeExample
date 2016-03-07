using System;
using Prism.Mvvm;

namespace PowerUserMode.Wpf.Common
{
    public class Selectable<T> : Selectable, ISelectable<T>
    {
        public T Value { get; private set; }

        public Selectable(string displayValue, T value)
            :base(displayValue)
        {
            this.Value = value;
        }
    }

    public class Selectable : BindableBase, ISelectable
    {
        private bool isSelected;
        private string displayText;

        /// <summary>
        /// Gets the text to display to the user
        /// </summary>
        public string DisplayText
        {
            get { return displayText; }
            private set { SetProperty(ref displayText, value); }
        }

        /// <summary>
        /// Gets and sets whether this item is selected
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        public Selectable(string displayValue)
        {
            this.displayText = displayValue;
        }

        public override string ToString()
        {
            return DisplayText + " " + IsSelected.ToString();
        }
    }
}
