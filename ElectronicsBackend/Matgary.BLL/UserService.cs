using Matgary.BLL.Infra.User;
using Matgary.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Matgary.BLL.Infra.Stores;

namespace Matgary.BLL
{
    public class UserService : DefaultService
    {
        public UserResponseViewModel Registration(UserRegisterViewModel request)
        {
            //:ToDo send welcome/confirmation email
            var response = new UserResponse
            {
                User = new User
                {
                    Email = request.Email,
                    Password = request.Password,
                    Username = request.Email
                }
            };

            var userInDb = Db.Users
                .FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

            if (userInDb != null)
                response.User.Id = userInDb.Id;

            var address = new Address()
            {
                Street = request.Address.Street,
                IsDefault = true,
                UserId = response.User.Id,
                StoreId = request.StoreId
            };

            var userStore = new UserStore()
            {
                StoreId = request.StoreId,
                UserId = response.User.Id,
                Phone1 = request.Phone,
                Gender = request.Gender,
                Name = request.Name,
                GoogleAccessToken = request.GoogleAccessToken,
                FacebookAccessToken = request.FacebookAccessToken
            };

            if (userInDb == null)
                Db.Users.Add(response.User);

            Db.Address.Add(address);
            Db.UserStores.Add(userStore);
            Db.SaveChanges();

            var addressInDb = Db.Address
                            .FirstOrDefault(a => a.UserId == response.User.Id && a.IsDefault == true);


            var userResponse = Db.UserStores
                .Include(ur=>ur.Store)
                .Where(us => us.UserId == response.User.Id).ToList();

            if (userResponse.Count() != 0 && userResponse.Count() == 1)
            {
                var result = new UserResponseViewModel
                {
                    User = userResponse.Select(ur => new UserViewModel
                    {
                        Id = response.User.Id,
                        Email = request.Email,
                        Password = request.Password,
                        Username = request.Email,
                        Phone = request.Phone,
                        Gender = request.Gender,
                        Name = request.Name,
                        DeviceToken = request.DeviceToken,
                        Address = new AddressViewModel()
                        {
                            Street = addressInDb.Street,                      
                            IsDefault = addressInDb.IsDefault
                        },
                        GoogleAccessToken = request.GoogleAccessToken,
                        FacebookAccessToken = request.FacebookAccessToken,
                        Store = new StoreDto()
                        {
                            Id = userResponse.FirstOrDefault().Store.Id,
                            Name = userResponse.FirstOrDefault().Store.Name,
                            Currency = userResponse.FirstOrDefault().Store.Currency,
                            Logo = userResponse.FirstOrDefault().Store.Logo == null ? null : ConfigurationManager.AppSettings["Image_Url"] + userResponse.FirstOrDefault().Store.Logo
                        }
                    }).FirstOrDefault(),
                    IsExist = true,
                    Stores = null,
                    UserId = response.User.Id
                };

                if (!string.IsNullOrWhiteSpace(request.DeviceToken))
                    CreateToken(response.User.Id, request.DeviceToken, request.StoreId);

                return result;
            }
            else
            {
                var result = new UserResponseViewModel
                {
                    User = null,
                    IsExist = true,
                    Stores = userResponse.Select(ur => new StoreDto()
                    {
                        Id = ur.Store.Id,
                        Name = ur.Store.Name,
                        Currency = ur.Store.Currency,
                        Logo = ur.Store.Logo == null ? null : ConfigurationManager.AppSettings["Image_Url"] + ur.Store.Logo
                    }).ToList(),
                    UserId = response.User.Id
                };
                return result;
            }

        }

        public UserResponseViewModel Login(LoginRequest request, List<UserStore> userStores)
        {
            if (userStores.Count() != 0 && userStores.Count() == 1)
            {
                var userStore = userStores.FirstOrDefault();

                var addressInDb = Db.Address
                                  .FirstOrDefault(a => a.UserId == userStore.UserId && a.IsDefault == true);

                var result = new UserResponseViewModel
                {
                    User = userStores.Select(ur => new UserViewModel
                    {
                        Id = ur.User.Id,
                        Email = request.Email,
                        Password = request.Password,
                        Username = request.Email,
                        Phone = ur.Phone1,
                        Gender = ur.Gender,
                        Name = ur.Name,
                        DeviceToken = request.DeviceToken,
                        Address = new AddressViewModel()
                        {
                            Street = addressInDb.Street,                        
                            IsDefault = addressInDb.IsDefault
                        },
                        GoogleAccessToken = ur.GoogleAccessToken,
                        FacebookAccessToken = ur.FacebookAccessToken,
                        Store = new StoreDto()
                        {
                            Id = userStore.Store.Id,
                            Name = userStore.Store.Name,
                            Currency = userStore.Store.Currency,
                            Logo = userStore.Store.Logo == null ? null : ConfigurationManager.AppSettings["Image_Url"] + userStore.Store.Logo
                        }
                    }).FirstOrDefault(),
                    IsExist = true,
                    Stores = null,
                    UserId = userStore.UserId
                };

                if (!string.IsNullOrWhiteSpace(request.DeviceToken))
                    CreateToken(userStore.UserId, request.DeviceToken, userStore.StoreId);
                return result;
            }
            else
            {
                var result = new UserResponseViewModel
                {
                    User = null,
                    IsExist = true,
                    Stores = userStores.Select(ur => new StoreDto()
                    {
                        Id = ur.Store.Id,
                        Name = ur.Store.Name,
                        Currency = ur.Store.Currency,
                        Logo = ur.Store.Logo == null ? null : ConfigurationManager.AppSettings["Image_Url"] + ur.Store.Logo
                    }).ToList(),
                    UserId = userStores.FirstOrDefault().UserId
                };
                return result;

            }

        }


        public async System.Threading.Tasks.Task<bool> Logout(Request request)
        {
            var entity =
                await Db.Devices
                    .FirstOrDefaultAsync(u => u.UserId == request.UserId && request.StoreId == u.StoreId && u.Token == request.DeviceToken);
            if (entity != null)
            {
                Db.Devices.Remove(entity);
                await Db.SaveChangesAsync();
            }

            return true;
        }

        public async System.Threading.Tasks.Task<ForgetPasswordResponse> ForgetPassword
            (ForgetPasswordRequest request)
        {

            var user = await Db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null) return null;
            var code = RandomString();

            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;

            string support_emailFrom = ConfigurationManager.AppSettings["SupportMail.From"];//"beinsan.toystore@gmail.com";
            string support_password = ConfigurationManager.AppSettings["SupportMail.Password"];//"b123t456";
            string support_emailTo = ConfigurationManager.AppSettings["SupportMail.From"];

            string client_emailFrom = ConfigurationManager.AppSettings["ClientMail.From"];
            string client_password = ConfigurationManager.AppSettings["ClientMail.Password"];
            string client_emailTo = ConfigurationManager.AppSettings["ClientMail.From"];

            string emailTo = request.Email;
            string subject = "Talent Store";
            string body = $"Your code is <b> {code} </b> use it to reset your password";
            MailMessage mail1 = new MailMessage();
            mail1.From = new MailAddress(support_emailFrom);
            mail1.To.Add(support_emailTo);
            mail1.Subject = subject;
            mail1.Body = body;
            mail1.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(support_emailFrom, support_password);
                smtp.EnableSsl = enableSSL;
                try
                {
                    smtp.Send(mail1);
                }
                catch (Exception ex)
                {

                }
            }

            MailMessage mail2 = new MailMessage();
            mail2.From = new MailAddress(client_emailFrom);
            mail2.To.Add(client_emailTo);
            mail2.Subject = subject;
            mail2.Body = body;
            mail2.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(client_emailFrom, client_password);
                smtp.EnableSsl = enableSSL;
                try
                {
                    smtp.Send(mail2);
                }
                catch (Exception ex)
                {

                }
            }
            user.Code = code;
            await Db.SaveChangesAsync();
            var res = new ForgetPasswordResponse
            {
                Code = code,
                Email = user.Email,
                UserId = user.Id
            };

            return res;
        }

        public async System.Threading.Tasks.Task<ConfirmCodeResponse> ConfirmCode(ConfirmCodeRequest request)
        {
            var response = new ConfirmCodeResponse();
            var user = await Db.Users.
                FirstOrDefaultAsync(uc => uc.Code == request.Code && uc.Email == request.Email);
            user.Code = "";
            user.Password = request.NewPassword;
            await Db.SaveChangesAsync();

            response.User = user;
            return response;
        }


        public async System.Threading.Tasks.Task<UserResponseViewModel> UpdateUser(
            UserUpdateRequest request)
        {
            var dataUser = await Db.UserStores
                .FirstOrDefaultAsync(us => us.UserId == request.Id && us.StoreId == request.StoreId);

            //var currentAddress = Db.Address
            //    .Include(a => a.City)
            //    .FirstOrDefault(a => a.IsDefault == true && dataUser.UserId == a.UserId &&
            //                         a.StoreId == dataUser.StoreId);
            var currentAddress = Db.Address
                            .FirstOrDefault(a => a.IsDefault == true && dataUser.UserId == a.UserId);

            if (dataUser == null) return null;

            if (!string.IsNullOrEmpty(request.Code))
                dataUser.User.Code = request.Code;

            if (!string.IsNullOrEmpty(request.Phone1))
                dataUser.Phone1 = request.Phone1;
            if (!string.IsNullOrEmpty(request.Phone2))
                dataUser.Phone2 = request.Phone2;
            if (!string.IsNullOrEmpty(request.Name))
                dataUser.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Gender.ToString()))
                dataUser.Gender = request.Gender;
            if (!string.IsNullOrEmpty(request.Password))
                dataUser.User.Password = request.Password;

            if (request.Address != null)
            {
                currentAddress.Street = request.Address.Street;
                Db.Entry(currentAddress).State = EntityState.Modified;
            }

            Db.Entry(dataUser).State = EntityState.Modified;
            await Db.SaveChangesAsync();

            return new UserResponseViewModel()
            {
                User = new UserViewModel()
                {
                    Id = dataUser.User.Id,
                    Name = dataUser.Name,
                    Address = new AddressViewModel()
                    {                  
                        Street = currentAddress.Street,
                        IsDefault = currentAddress.IsDefault
                    },
                    Email = dataUser.User.Email,
                    Username = dataUser.User.Username,
                    Phone = dataUser.Phone1,
                    Gender = dataUser.Gender,
                    Password = dataUser.User.Password,
                    Store = new StoreDto()
                    {
                        Id = dataUser.Store.Id,
                        Name = dataUser.Store.Name,
                        Currency = dataUser.Store.Currency,
                        Logo = dataUser.Store.Logo == null ? null : ConfigurationManager.AppSettings["Image_Url"] + dataUser.Store.Logo
                    }
                }

            };
            //return dataUser;
        }


        private static string RandomString(int length = 6)
        {
            var random = new Random();
            return new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void CreateToken(long userId, string token, long storeId)
        {
            var device = Db.Devices.FirstOrDefault(ud => ud.Token == token && ud.UserId == userId && ud.StoreId == storeId);
            var entity = new Device
            {
                UserId = userId,
                Token = token,
                StoreId = storeId
            };
            Db.Devices.Add(entity);
            Db.SaveChanges();
        }
    }
}
