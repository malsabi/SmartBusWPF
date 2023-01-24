﻿using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    public class NavigationService : INavigationService
    {
        #region "Fields"
        private Frame currentFrame;
        private Dictionary<Type, Page> viewMapping;
        #endregion

        #region "Properties"
        public Frame CurrentFrame
        {
            get
            {
                return currentFrame;
            }
        }

        public bool CanGoBack
        {
            get
            {
                return CurrentFrame != null && CurrentFrame.CanGoBack;
            }
        }

        public IReadOnlyDictionary<Type, Page> ViewMapping
        {
            get
            {
                return viewMapping ?? null;
            }
        }
        #endregion

        #region "Constructors / Destructors"
        public NavigationService()
        {
            Initialize();
        }
        public NavigationService(Frame currentFrame)
        {
            this.currentFrame = currentFrame;
            Initialize();
        }

        ~NavigationService()
        {
            UnregisterAll();
        }
        #endregion

        #region "Initialization"
        private void Initialize()
        {
            viewMapping = new Dictionary<Type, Page>();
        }
        #endregion

        #region "Navigation"
        public void RegisterView(Type ViewModelType, Page PageView)
        {
            if (viewMapping != null && !viewMapping.ContainsKey(ViewModelType))
            {
                viewMapping.Add(ViewModelType, PageView);
            }
        }

        public void Unregister(Type ViewModelType)
        {
            if (viewMapping != null && viewMapping.ContainsKey(ViewModelType))
            {
                viewMapping.Remove(ViewModelType);
            }
        }

        public void UnregisterAll()
        {
            if (viewMapping != null)
            {
                foreach (Type ViewModelType in viewMapping.Keys.ToArray())
                {
                    viewMapping.Remove(ViewModelType);
                }
            }
        }

        public void SetCurrentFrame(Frame currentFrame)
        {
            this.currentFrame = currentFrame;
        }

        public void GoBack()
        {
            CurrentFrame.GoBack();
        }

        public void Navigate<ViewModelType>(object args = null)
        {
            CurrentFrame.Navigate(viewMapping[typeof(ViewModelType)], args);
        }
        #endregion
    }
}