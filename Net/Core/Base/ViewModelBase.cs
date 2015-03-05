using System;
using System.ComponentModel;

namespace Primavera.Platform.CloudServices900.Base
{
    /// <summary>
    /// Base class of view model classes.
    /// </summary>
    internal abstract class ViewModelBase
        : DisposableBase, IDisposable, INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        public ViewModelBase()
            : base() // Track disposed state
        {            
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases unmanaged and managed resources (optionally).
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called

            if (!this.Disposed)
            {
                // Dispose managed resources

                if (disposing)
                {
                    this.PropertyChanged = null;
                }

                // Dispose unmanaged resources

                // do nothing
            }

            // Dispose on base class

            base.Dispose(disposing);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called to invoke the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property whose value
        /// has been changed.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
