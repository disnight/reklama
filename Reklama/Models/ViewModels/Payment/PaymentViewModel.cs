using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Domain.Enums;

namespace Reklama.Models.ViewModels.Payment
{
    public class PaymentViewModel
    {
        public string Login { get; set; }
        public int InvId { get; set; }
        public decimal Sum { get; set; }
        public string Desc { get; set; }
        public string Currency { get; set; }
        public string Culture { get; set; }
        public string Encoding { get; set; }
        public int ShpAdId { get; set; }
        public int ShpPremiumId { get; set; }
        public int ShpSectionId { get; set; }
        public CategorySearch ShpSiteCategory { get; set; }

        private readonly string _password1;
        private readonly string _password2;

        public PaymentViewModel()
        {
            Login = ProjectConfiguration.Get.GetConfigValue("ROBOLogin").ToString();
            InvId = 0;
            _password1 = ProjectConfiguration.Get.GetConfigValue("ROBOPass1").ToString();
            _password2 = ProjectConfiguration.Get.GetConfigValue("ROBOPass2").ToString();
            Culture = "ru";
            Encoding = "utf-8";
        }

        /**
         * This constructor is uses for initialization the payment
         */
        public PaymentViewModel(string desc, decimal sum, CategorySearch siteCategory, int sectionId, int adId, int premiumId): this()
        {
            Desc = desc;
            Sum = sum;
            ShpSiteCategory = siteCategory;
            ShpAdId = adId;
            ShpPremiumId = premiumId;
            ShpSectionId = sectionId;
        }


        /**
         * This constructor uses for checking a payment
         */
        public PaymentViewModel(decimal sum, int invId, int adId, int premiumId, int sectionId, CategorySearch siteCategory): this()
        {
            Sum = sum;
            ShpAdId = adId;
            ShpSiteCategory = siteCategory;
            ShpSectionId = sectionId;
            InvId = invId;
            ShpPremiumId = premiumId;
        }


        public string GenerateHash1()
        {
            var encodedHash = string.Format(
                "{0}:{1}:{2}:{3}:ShpAdId={4}:ShpPremiumId={5}:ShpSectionId={6}:ShpSiteCategory={7}",
                Login, Sum, InvId, _password1, ShpAdId, ShpPremiumId, ShpSectionId, ShpSiteCategory);

            return GenerateMd5Hash(encodedHash);            
        }


        public string GenerateHash2()
        {
            var encodedHash = string.Format(
                "{0}:{1}:{2}:ShpAdId={3}:ShpPremiumId={4}:ShpSectionId={5}:ShpSiteCategory={6}",
                Sum, InvId, _password2, ShpAdId, ShpPremiumId, ShpSectionId, ShpSiteCategory);

            return GenerateMd5Hash(encodedHash);
        }

        private string GenerateMd5Hash(string str)
        {
            // build CRC value 
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //var hashBytes = ASCIIEncoding.Default.GetBytes(encodedHash);
            var hashBytes = ASCIIEncoding.UTF8.GetBytes(str);
            var encodedBytes = md5.ComputeHash(hashBytes);

            StringBuilder sbSignature = new StringBuilder(); foreach (byte b in encodedBytes) sbSignature.AppendFormat("{0:x2}", b);

            return sbSignature.ToString();
        }
    }
}


