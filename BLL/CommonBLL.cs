using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class CommonBLL
    {
        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[20];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            SHA256Managed sha = new SHA256Managed();
            byte[] result = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltAndPwd));
            return Convert.ToBase64String(result);
        }

        public static string UploadImage(HttpPostedFileBase file, string folder)
        {
            string dirPath = "/Images/" + folder + "/";
            string extension = Path.GetExtension(file.FileName);
            string fullPath = dirPath+DateTime.Now.ToString("ddMMyyyyhhmmssffff") + extension;
        
                    file.SaveAs(HttpContext.Current.Server.MapPath(fullPath));
            
                         return fullPath;

        }


        public static void PasswordRecovery(string guid, string Email, string userName)
        {
            MailMessage mail = new MailMessage("Email@gmail.com", Email);
            mail.Subject = "Change password for Workforce";
            string link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "/Account/ResetPassword?guid=" + guid);
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append("Dear " + userName + "<br/><br/>");
            sbBody.Append("Please Click on the following Link To change your password <br/><br/>");

            sbBody.Append(link + " <br/><br/>");


            mail.Body = sbBody.ToString();
            mail.IsBodyHtml = true;


            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("Email@gmail.com", "password");
            client.EnableSsl = true;
            client.Send(mail);



        }

    }
}
