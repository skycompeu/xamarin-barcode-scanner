using OmniReader.Core.View;
using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace OmniReader.GUI.ViewModel.Config.User
{
    public class UserHomeViewModel : BaseViewModel
    {
        Repository<AppUser> Database;
        
        private ObservableCollection<AppUser> m_UserList;
        public ObservableCollection<AppUser> UserList
        {
            get => m_UserList;
            set => SetProperty(ref m_UserList, value);
        }

        public UserHomeViewModel()
        {
            Setup();
        }

        private void Setup()
        {
            Title = "Użytkownicy";
            Database = new Repository<AppUser>();
            //UserList = new ObservableCollection<AppUser>(Database.GetAll());
            UserList = new ObservableCollection<AppUser>(Database.Find(w => w.UniqueId > 1 && w.UniqueId > 1 && w.Name != "App"));
        }
    }
}

