using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using EstateManager.Model;
using EstateManager.View;
using EstateManager.Service;


namespace EstateManager.ViewModel
{
    public class MainViewModel : BaseNotifyPropertyChanged
    {
        public Page CurrentPage
        {
            get { return GetProperty<Page>(); }
            set { SetProperty(value); }
        }

        public Command ShowHomePageCommand
        {
            get
            {
                return new Command(() =>
                {
                    CurrentPage = Service.NavigationService.GetPage<View.HomePage, HomeViewModel>();
                });
            }
        }
        public Command ShowListPageCommand
        {
            get
            {
                return new Command(
                    () =>
                    {
                        CurrentPage = Service.NavigationService.GetPage<View.ListPage, EstateListManager>();
                    });
            }
        }
        public Command ShowSettingsWindowCommand
        {
            get
            {
                return null;
            }
        }

        //private void ShowHomePage()
        //{
        //    CurrentPage = Service.NavigationService.GetPage<View.HomePage, HomeViewModel>();
        //}
        private async Task LoadDatabase()
        {
            await DataAccess.EstateDbContext.Initialize();
            //await Task.Delay(5000);
            ShowHomePageCommand.Execute(null);
        }


        public MainViewModel()
        {
            CurrentPage = Service.NavigationService.GetPage<View.LoaderView, LoaderViewModel>();
            LoadDatabase();
        }
    }
}
