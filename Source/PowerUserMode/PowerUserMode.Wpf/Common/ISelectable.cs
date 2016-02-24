namespace PowerUserMode.Wpf.Common
{
    public interface ISelectable<T> : ISelectable
    {
        /// <summary>
        /// Gets the payload value of this <see cref="ISelectable{T}"/>
        /// </summary>
        T Value { get; }
    }

    public interface ISelectable
    {
        /// <summary>
        /// Gets and sets whether this item is selected
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Gets the text to display to the user
        /// </summary>
        string DisplayText { get;}
    }
}
