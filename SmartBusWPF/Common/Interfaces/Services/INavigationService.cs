using System;
using System.Windows.Controls;
using System.Collections.Generic;

namespace SmartBusWPF.Common.Interfaces.Services
{
    public interface INavigationService
    {
        /// <summary>
        /// Returns a read only dictionary of the mapped view models to the views, if there is no registered view models
        /// it will return null.
        /// </summary>
        IReadOnlyDictionary<Type, Type> ViewMapping { get; }

        /// <summary>
        /// Registers a view in Dictionary by setting its view model as a key
        /// </summary>
        /// <param name="viewModelType">Represents the ViewModel type of the view.</param>
        /// <param name="pageType">Represents the Page view type.</param>
        void RegisterView(Type viewModelType, Type pageType);

        /// <summary>
        /// Unregisters the View from the dictionary by providing it's key.
        /// </summary>
        /// <param name="ViewModelType">Represents the ViewModel type that is the key for the View.</param>
        void Unregister(Type ViewModelType);

        /// <summary>
        /// Unregisters all of the Views from the dictionary.
        /// </summary>
        void UnregisterAll();

        /// <summary>
        /// Sets the main current frame that will handle all of the view navigations
        /// </summary>
        /// <param name="CurrentFrame">Represents the main frame of the application.</param>
        void SetCurrentFrame(Frame CurrentFrame);

        /// <summary>
        /// Returns true if it can navigate backwards to the previous page otherwise false.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Goes back to the previous page.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Navigates to a specific type page with a given arguments.
        /// </summary>
        /// <typeparam name="T">Represents the page type.</typeparam>
        /// <param name="args">Represents the arguments passed to the new page</param>
        void Navigate<T>(object args = null);
    }
}