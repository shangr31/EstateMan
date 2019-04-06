using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace EstateManager
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
         
    public partial class App : Application
    {
     /*
        protected async override void OnStartup(StartupEventArgs e)
        {
            /*
            base.OnStartup(e);
            await DataAccess.EstateDbContext.Initialize();           
            

            var c = new Model.Contract()
            {
                Price = 2000,
            };

            DataAccess.EstateDbContext.Current.Add(c);
            DataAccess.EstateDbContext.Current.SaveChanges();
            
            var monbien = DataAccess.EstateDbContext.Current.Estates.Where(a => a.City == "Lyon")
                .OrderBy(a => a.Address)
                .Include(a=>a.Photos)
                .Where(a => a.MainPhoto.Title == "coucou");
            
        }
    */

    }
}
