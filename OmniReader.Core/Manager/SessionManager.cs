using OmniReader.Data.Database.Model;
using OmniReader.Data.Database.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.Manager
{
    public class SessionManager
    {
        public  AppUser User { get; set; }
        private Repository<AppUser> m_AppUserRepo;
        
        public SessionManager()
        {
            m_AppUserRepo = new Repository<AppUser>();
        }
        
        public bool Login(string pinInput)
        {
            bool Auth = false;
            List<AppUser> UserList = m_AppUserRepo.Find(f => f.Pin.ToLower() == pinInput.ToLower());
            
            if (UserList == null) return Auth;
            if (UserList.Count > 1) return Auth;
            if (UserList.Count == 0) return Auth;
            
            AppUser User = UserList[0];

            if (User.Active == false) return Auth;
            
            if (User.Pin.ToLower() == pinInput.ToLower())
            {
                Auth = true;
                this.User = User;
            }
            
            return Auth;
        }

        public bool LoginV2(string pinInput, string name, string surname, ref LoginMessageEnum lme)
        {
            bool Auth = false;
            List<AppUser> UserList = m_AppUserRepo.Find(f => f.Pin.ToLower() == pinInput.ToLower() && f.Name == name && f.Surname == surname);

            if (UserList == null || UserList.Count > 1 || UserList.Count == 0) {
                lme = LoginMessageEnum.BadPIN;
                return Auth;
            };
            
            AppUser User = UserList[0];

            if (User.Active == false) {
                lme = LoginMessageEnum.Inactive;
                return Auth;
            };

            if (User.Pin.ToLower() == pinInput.ToLower())
            {
                Auth = true;
                this.User = User;
            }

            return Auth;
        }

        public void Logout() {
            User = null;
        }
        
        public bool IsUserLogged() {
            return (User == null) ? false : true;
        }
    }
}
