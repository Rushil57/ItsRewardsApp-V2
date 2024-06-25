using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using ItsRewardsApp.Server.Helper;

namespace ItsRewardsApp.Server.Services
{
    public class LoyaltyUserProfileManager : ILoyaltyUserProfile
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        public LoyaltyUserProfileManager(LoyaltyUserDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public LoyaltyUserProfileViewModel AddUser(LoyaltyUserProfileViewModel user)
        {
            try
            {
                byte[] password = null;
                CommonHelper commonHelp = new CommonHelper();
                commonHelp.encryptAES(user.Password, ref password);
                var encoding = Encoding.GetEncoding("iso-8859-1");
                LoyaltyUserProfile loyaltyUserProfile = new LoyaltyUserProfile();
                loyaltyUserProfile.RevenueCenterID = user.RevenueCenterID;
                loyaltyUserProfile.isDelete = user.isDelete;
                loyaltyUserProfile.isActive = user.isActive;
                loyaltyUserProfile.FirstName = user.FirstName;
                loyaltyUserProfile.LastName = user.LastName;
                loyaltyUserProfile.Title = user.Title;
                loyaltyUserProfile.Address1 = user.Address1;
                loyaltyUserProfile.Address2 = user.Address2;
                loyaltyUserProfile.State = user.State;
                loyaltyUserProfile.City = user.City;
                loyaltyUserProfile.ZipCode = user.ZipCode;
                loyaltyUserProfile.EMail = user.EMail;
                loyaltyUserProfile.HomePhone = user.HomePhone;
                loyaltyUserProfile.CellPhone = user.CellPhone;
                loyaltyUserProfile.Points = user.Points;
                loyaltyUserProfile.CustomerGroup = user.CustomerGroup;
                loyaltyUserProfile.CustomerStatus = user.CustomerStatus;
                loyaltyUserProfile.BirthDate = user.BirthDate;
                loyaltyUserProfile.PriceLevel = user.PriceLevel;
                loyaltyUserProfile.CustomerNumber = user.CustomerNumber;
                loyaltyUserProfile.AgeVerified = user.AgeVerified;
                loyaltyUserProfile.AppVerified = user.AppVerified;
                loyaltyUserProfile.UserPass = password;// encoding.GetBytes(password);//   user.UserPass;
                loyaltyUserProfile.UserSignature = user.UserSignature;
                //loyaltyUserProfile.EmailPromotions = user.EmailPromotions;
                //loyaltyUserProfile.TextPromotions = user.TextPromotions;
                //loyaltyUserProfile.UserPass = password;

                _dbContext.LoyaltyUserProfile.Add(loyaltyUserProfile);
                _dbContext.SaveChanges();

                if (loyaltyUserProfile.UserProfileID != 0 && user.StoreId != "0" && user.StoreId != null)
                {
                    UserLoyaltyStoreMappings userLoyaltyStoreMappings = new UserLoyaltyStoreMappings();
                    userLoyaltyStoreMappings.UserProfileID = loyaltyUserProfile.UserProfileID;
                    userLoyaltyStoreMappings.StoreID = Convert.ToInt32(user.StoreId);
                    userLoyaltyStoreMappings.CreateDate = DateTime.Now;
                    _dbContext.UserLoyaltyStoreMappings.Add(userLoyaltyStoreMappings);
                    _dbContext.SaveChanges();
                }

                // user.UserPass = loyaltyUserProfile.UserPass;
                Random generator = new Random();
                String pin = generator.Next(0, 1000000).ToString("D6");

                if (loyaltyUserProfile.UserProfileID != null)
                {
                    loyaltyUserProfile.Pinnumber = pin;
                    _dbContext.Entry(loyaltyUserProfile).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                var message = "ItsReward's Confirmation Pin:" + pin;
                CommonHelper commonHelper = new CommonHelper();
                commonHelper.SendToMobile(message, loyaltyUserProfile.CellPhone);
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                LoyaltyUserProfile? user = _dbContext.LoyaltyUserProfile.Find(id);
                if (user != null)
                {
                    _dbContext.LoyaltyUserProfile.Remove(user);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public ProfileViewModel GetUserData(string cellNumber)
        {
            try
            {

                LoyaltyUserProfile? user = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == cellNumber).FirstOrDefault();
                if (user != null)
                {
                    ProfileViewModel profileViewModel = new ProfileViewModel();
                    profileViewModel.FirstName = user.FirstName;
                    profileViewModel.LastName = user.LastName;
                    profileViewModel.Address1 = user.Address1;
                    profileViewModel.City = user.City;
                    profileViewModel.State = user.State;
                    profileViewModel.ZipCode = user.ZipCode;
                    profileViewModel.CellPhone = user.CellPhone;
                    profileViewModel.EMail = user.EMail;
                    profileViewModel.BirthDate = user.BirthDate;
                    profileViewModel.UserProfileID = user.UserProfileID;
                    return profileViewModel;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<LoyaltyUserProfile> GetUserDetails()
        {
            try
            {
                return _dbContext.LoyaltyUserProfile.ToList();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUserDetails(ProfileViewModel profileViewModel)
        {
            try
            {
                LoyaltyUserProfile? user = _dbContext.LoyaltyUserProfile.Find(profileViewModel.UserProfileID);
                if (user != null)
                {
                    user.FirstName = profileViewModel.FirstName;
                    user.LastName = profileViewModel.LastName;
                    user.Address1 = profileViewModel.Address1;
                    user.City = profileViewModel.City;
                    user.State = profileViewModel.State;
                    user.ZipCode = profileViewModel.ZipCode;
                    user.EMail = profileViewModel.EMail;
                    user.BirthDate = profileViewModel.BirthDate;


                    _dbContext.Entry(user).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        public string? GetUserDataByEmailAndPhone(string FieldValue, string PropertyName)
        {
            try
            {
                LoyaltyUserProfile user = new LoyaltyUserProfile();
                if (PropertyName.ToLower() == "phone")
                {
                    user = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == FieldValue).FirstOrDefault();

                }
                else if (PropertyName.ToLower() == "email")
                {
                    user = _dbContext.LoyaltyUserProfile.Where(x => x.EMail.ToLower() == FieldValue.ToLower()).FirstOrDefault();
                }
                //else if (PropertyName.ToLower() == "address")
                //{
                //    user = _dbContext.LoyaltyUserProfile.Where(x => x.Address1.ToLower() == FieldValue.ToLower()).FirstOrDefault();
                //}
                else
                {
                    user = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone.ToLower() == PropertyName.ToLower() && x.Pinnumber == FieldValue).FirstOrDefault();
                    if (user != null)
                    {
                        user.AppVerified = "Y";
                        _dbContext.Entry(user).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                }
                return user?.FirstName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
