using EstateManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EstateManager.Model;

namespace EstateManager.Service
{
    public static class NavigationService
    {

        private static Dictionary<Type, object> _viewsCache = new Dictionary<Type, object>();
        private static Dictionary<Type, BaseNotifyPropertyChanged> _ViewModelCache =
            new Dictionary<Type, BaseNotifyPropertyChanged>();

        private static TViewModel GetViewModelInstance<TViewModel>(bool cache = true, params object[] viewModelParameters)
            where TViewModel : BaseNotifyPropertyChanged
        {
            return (TViewModel)GetViewModelInstance(typeof(TViewModel), cache, viewModelParameters);
        }
        private static object GetViewModelInstance(Type tViewModel,
                                                   bool cache = true,
                                                   params object[] viewModelParameters)
        {
            object vm = null;
            if (cache && _ViewModelCache.ContainsKey(tViewModel))
                vm = _ViewModelCache[tViewModel];
            else
            {
                vm = Activator.CreateInstance(tViewModel, viewModelParameters);
                if (cache)
                    _ViewModelCache[tViewModel] = (BaseNotifyPropertyChanged)vm;
            }
            return vm;
        }

        private static TView GetViewInstance<TView>(object viewModel)
            where TView : class
        {
            return (TView)GetViewInstance(typeof(TView), viewModel);
        }
        private static object GetViewInstance(Type tView, object viewModel)
        {
            object view = null;
            bool isWindow = tView.BaseType == typeof(Window);
            if (!isWindow && _viewsCache.ContainsKey(tView))
                view = _viewsCache[tView];
            else
            {
                view = Activator.CreateInstance(tView);
                var prop = tView.GetProperty("DataContext");
                prop?.SetValue(view, viewModel);
                if (!isWindow)
                    _viewsCache[tView] = view;
            }
            return view;
        }

        public static TView GetPage<TView, TViewModel>(params object[] viewModelParameters)
            where TView : Page
            where TViewModel : BaseNotifyPropertyChanged
        {
            return GetViewInstance<TView>(
                GetViewModelInstance<TViewModel>(true, viewModelParameters));
        }

        public static TView Show<TView, TViewModel>(params object[] viewModelParameters)
            where TView : Window
            where TViewModel : WindowNotifyPropertyChanged
        {
            var view = GetViewInstance<TView>(null);
            var lparams = new List<object>(viewModelParameters);
            lparams.Insert(0, view);
            var vm = GetViewModelInstance<TViewModel>(false, lparams.ToArray());
            view.DataContext = vm;
            view.Show();
            return view;
        }
        public static void Close(WindowNotifyPropertyChanged vm, bool? result = null)
        {
            if (result != null)
              //  vm.view.DialogResult = result;
            vm.view.Close();
        }

        public static bool? ShowDialog<TView, TViewModel>(params object[] viewModelParameters)
            where TView : Window
            where TViewModel : WindowNotifyPropertyChanged
        {
            var view = GetViewInstance<TView>(null);
            var lparams = new List<object>(viewModelParameters);
            lparams.Insert(0, view);
            var vm = GetViewModelInstance<TViewModel>(false, lparams.ToArray());
            view.DataContext = vm;
            return view.ShowDialog();
        }

    }
}
