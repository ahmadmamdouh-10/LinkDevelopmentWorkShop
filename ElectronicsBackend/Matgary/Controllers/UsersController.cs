using Matgary.BLL;
using Matgary.BLL.Infra.User;
using Matgary.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using Matgary.BLL.Infra.Stores;

namespace Matgary.Controllers
{

    [RoutePrefix("api/user")]
    public class UsersController : ApiController
    {
        private readonly UserService _userServices = new UserService();
        private readonly Matgary.DAL.AppContext _db = new DAL.AppContext();


        [HttpPost, Route("UpdateUserToken")]
        public IHttpActionResult UpdateUserToken(int userId, string token, long storeId)
        {
            var userToken = _db.Devices.FirstOrDefault(d => d.UserId == userId && d.StoreId == storeId);
            if (userToken != null)
            {
                userToken.Token = token;
                _db.Entry(userToken).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                var userInDb = _db.Users.FirstOrDefault(u => u.Id == userId);
                if (userInDb == null)
                    return BadRequest("Not Valid User");

                _db.Devices.Add(new Device()
                {
                    UserId = userId,
                    Token = token,
                    DateTime = DateTime.Now,
                    StoreId = storeId
                });
                _db.SaveChanges();
            }
            return Ok("Updated");
        }

        [HttpPost, Route("registration")]
        public IHttpActionResult Registration([FromBody] UserRegisterViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values.ToList()[0].Errors[0].ErrorMessage);
                }
                return Created("", _userServices.Registration(request));
            }
            catch (Exception e)
            {
                return InternalServerError(_userServices.SetException(e));
            }
        }

        [HttpPost, Route("login")]
        public async Task<IHttpActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Keys.ToList()[0]);
                var user = await request.Validate();
                if (user != null) return Ok(_userServices.Login(request, user));
                ModelState.Add(new KeyValuePair<string, ModelState>("No User Exist", new ModelState { Errors = { "" /*Data.Resources.UserNotExist*/ } }));
                return BadRequest(ModelState.Keys.ToList()[0]);
            }
            catch (Exception e)
            {
                return InternalServerError(_userServices.SetException(e));
            }
        }

        [HttpPost, Route("logout")]
        public async Task<IHttpActionResult> Logout([FromBody] Request request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Keys.ToList()[0]);
                var res = await _userServices.Logout(request);
                if (!res) return NotFound();
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(_userServices.SetException(e));
            }
        }

        [HttpPost, Route("forget-password")]
        public async Task<IHttpActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Keys.ToList()[0]);
                var res = await _userServices.ForgetPassword(request);
                if (res == null) return BadRequest();
                return Ok(res);
            }
            catch (Exception e)
            {
                return InternalServerError(_userServices.SetException(e));
            }
        }

        [HttpPost, Route("confirm-code")]
        public async Task<IHttpActionResult> ConfirmCode([FromBody] ConfirmCodeRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Keys.ToList()[0]);
                return Ok(await _userServices.ConfirmCode(request));
            }
            catch (Exception e)
            {
                return InternalServerError(_userServices.SetException(e));
            }
        }


        [HttpPut, Route("update")]
        public async Task<IHttpActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            try
            {
                var res = await _userServices.UpdateUser(request);
                if (res == null) return NotFound();
                return Ok(res);
            }
            catch (Exception e)
            {
                return InternalServerError(_userServices.SetException(e));
            }
        }


        [HttpGet, Route("{id}/{storeId}")]
        public async Task<IHttpActionResult> Get([FromUri] long id, [FromUri] long storeId)
        {
            try
            {
                var user = await _userServices.UserExist(id);
                if (user == null)
                    return NotFound();

                var userStore = _db.UserStores
                    .Include(us => us.Store)
                    .Include(us => us.User)
                    .FirstOrDefault(us => us.UserId == id && us.StoreId == storeId);

                if (userStore == null)
                {
                    var uStore = _db.UserStores
                        .FirstOrDefault(us => us.UserId == id);

                    if (uStore == null)
                        return NotFound();

                    _db.UserStores.Add(new UserStore()
                    {
                        DateTime = uStore.DateTime,
                        FacebookAccessToken = uStore.FacebookAccessToken,
                        FacebookId = uStore.FacebookId,
                        Gender = uStore.Gender,
                        GoogleAccessToken = uStore.GoogleAccessToken,
                        GmailId = uStore.GmailId,
                        Name = uStore.Name,
                        Phone1 = uStore.Phone1,
                        Phone2 = uStore.Phone2,
                        UserId = id,
                        StoreId = storeId
                    });
                    _db.SaveChanges();

                    userStore = _db.UserStores
                        .Include(us => us.Store)
                        .Include(us => us.User)
                        .FirstOrDefault(us => us.UserId == id && us.StoreId == storeId);
                }
                if (userStore != null)
                {
                    var addressInDb = 
                        _db.Address.FirstOrDefault(a => a.UserId == userStore.UserId && a.IsDefault == true);

                    var deviceToken = _db.Devices
                        .FirstOrDefault(a => a.UserId == userStore.UserId &&
                                             a.StoreId == userStore.StoreId);

                    var result = new UserResponseViewModel
                    {
                        User = new UserViewModel
                        {
                            Id = userStore.User.Id,
                            Email = userStore.User.Email,
                            Password = userStore.User.Password,
                            Username = userStore.User.Email,
                            Phone = userStore.Phone1,
                            Gender = userStore.Gender,
                            Name = userStore.Name,
                            DeviceToken = deviceToken?.Token,
                            Address = new AddressViewModel()
                            {
                                Street = addressInDb.Street,
                                IsDefault = addressInDb.IsDefault
                            },
                            GoogleAccessToken = userStore.GoogleAccessToken,
                            FacebookAccessToken = userStore.FacebookAccessToken,
                            Store = new StoreDto()
                            {
                                Id = userStore.Store.Id,
                                Name = userStore.Store.Name,
                                Currency = userStore.Store.Currency,
                                Logo = userStore.Store.Logo == null ? null : ConfigurationManager.AppSettings["Image_Url"] + userStore.Store.Logo

                            }
                        },
                        IsExist = true,
                        Stores = null,
                        UserId = userStore.UserId
                    };

                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(_userServices.SetException(e));
            }
        }
    }

}
