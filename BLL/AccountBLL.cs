using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using DAL.RespositoryPattern;
using System.IO;
using System.Web;

namespace BLL
{
    public class AccountBLL
    {
        IGenericRepository<Account> _accountDal = new GenericRepository<Account>();

        public string CreateAccount(Account account)
        {
            if(_accountDal.IsExists(x=>x.Email==account.Email))
            {
                return "Email Already Exists";
            }
            if(_accountDal.IsExists(x=>x.Username==account.Username))
            {
                return "Username already Exists";
            }
            account.Salt = CommonBLL.CreateSalt();
            account.Password = CommonBLL.CreatePasswordHash(account.Password, account.Salt);
            if(account.File!=null)
            {
                account.ImagePath = CommonBLL.UploadImage(account.File, "DP");
            }
            
            account.IsActive = true;
            int returnValue = _accountDal.Insert(account);
            if(returnValue>0)
            {
                return "Success";
            }
            return "There is some issue please try Again";
        }

        public Account Login(LoginVM login)
        {
            Account account = _accountDal.FindBy(x => x.Username == login.UserName).FirstOrDefault();
            if(account!=null)
            {
                string encPassword = CommonBLL.CreatePasswordHash(login.Password, account.Salt);
                if(encPassword==account.Password)
                {
                    return account;
                }
                return null;
            }
            return null;
        }

        public List<Account> GetAccounts()
        {
            return _accountDal.GetAll();
        }

        public string GetRole(String username)
        {
           return _accountDal.FindBy(x => x.Username == username).SingleOrDefault().UserRole;
        }

        public int BlockUser(long id)
        {
            Account user = _accountDal.GetById(id);
            user.IsActive = !user.IsActive;
            return _accountDal.Edit(user);
            
        }
        public Account Profile(long id)
        {
            return _accountDal.FindBy(x => x.Id == id).SingleOrDefault();
        }
        public int EditProfile(Account account)
        {

            if (account.File != null)
            {
                File.Delete(HttpContext.Current.Server.MapPath(account.ImagePath));
                
                account.ImagePath = CommonBLL.UploadImage(account.File, "DP");
            }
            return _accountDal.Edit(account);
        }

        
     
        public string ForgetPassword(string userName)
        {
            Account userAccount = _accountDal.FindBy(x => x.Username == userName).FirstOrDefault();
            if (userAccount != null)
            {
                string guid = Guid.NewGuid().ToString();
                userAccount.ResetCode = guid;
                _accountDal.Edit(userAccount);
                CommonBLL.PasswordRecovery(guid, userAccount.Email, userName);
                return "success";
            }
            else
            {
                return "NotExist";
            }
        }
        public string ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            Account userAccount = _accountDal.FindBy(x => x.ResetCode == resetPasswordViewModel.GUID).FirstOrDefault();
            if (userAccount != null)
            {
                userAccount.Salt = CommonBLL.CreateSalt();
                userAccount.Password = CommonBLL.CreatePasswordHash(resetPasswordViewModel.NewPassword, userAccount.Salt);
                userAccount.ResetCode = null;
                _accountDal.Edit(userAccount);
                return "success";
            }
            else
            {
                return "expires";
            }
        }
        public string ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {


            Account userAccount = _accountDal.FindBy(x => x.Username == changePasswordViewModel.UserName).FirstOrDefault();
            if (userAccount != null)
            {
                string dbSalt = userAccount.Salt;
                string dbPassword = userAccount.Password;
                string oldPass = CommonBLL.CreatePasswordHash(changePasswordViewModel.OldPassword, dbSalt);
                if (oldPass.Equals(dbPassword))
                {
                    userAccount.Salt = CommonBLL.CreateSalt();
                    userAccount.Password = CommonBLL.CreatePasswordHash(changePasswordViewModel.NewPassword, userAccount.Salt);
                    _accountDal.Edit(userAccount);
                    return "success";
                }
                else
                {
                    return "Old Password is not Correct";
                }
            }
            else
            {
                return "Username is not Correct";
            }

        }
        
    }
}
