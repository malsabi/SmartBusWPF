using System;
using System.Windows.Controls;
using System.Collections.Generic;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    public class NavigationService : INavigationService
    {
        private Frame currentFrame;
        private readonly Dictionary<Type, Type> viewMapping;

        public NavigationService()
        {
            viewMapping = new Dictionary<Type, Type>();
        }

        ~NavigationService()
        {
            UnregisterAll();
        }

        public bool CanGoBack
        {
            get
            {
                return currentFrame != null && currentFrame.CanGoBack;
            }
        }

        public IReadOnlyDictionary<Type, Type> ViewMapping
        {
            get
            {
                return viewMapping ?? null;
            }
        }

        public void RegisterView(Type viewModelType, Type pageType)
        {
            if (viewMapping.ContainsKey(viewModelType))
            {
                viewMapping[viewModelType] = pageType;
            }
            else
            {
                viewMapping.Add(viewModelType, pageType);
            }
        }

        public void Unregister(Type viewModelType)
        {
            if (viewMapping.ContainsKey(viewModelType))
            {
                viewMapping.Remove(viewModelType);
            }
        }

        public void UnregisterAll()
        {
            viewMapping.Clear();
        }

        public void SetCurrentFrame(Frame currentFrame)
        {
            this.currentFrame = currentFrame;
        }

        public void GoBack()
        {
            currentFrame.GoBack();
        }

        public void Navigate<ViewModelType>(object args = null)
        {
            Type viewModelType = typeof(ViewModelType);
            Type pageType = viewMapping[viewModelType];

            if (pageType == null)
            {
                throw new ArgumentException($"No view is registered for ViewModelType '{viewModelType.FullName}'");
            }

            if (Activator.CreateInstance(pageType) is not Page page)
            {
                throw new ArgumentException($"Registered pageType for ViewModelType '{viewModelType.FullName}' is not a Page");
            }
            if (currentFrame == null)
            {
                throw new InvalidOperationException("CurrentFrame is not set");
            }
            currentFrame.Navigate(page, args);
        }
    }
}