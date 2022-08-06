using Matgary.DAL;
using System;
using System.Data.Entity;

namespace Matgary.BLL
{
    public class DefaultService
    {
        protected DAL.AppContext Db;
        public DefaultService()
        {
            Db = new DAL.AppContext();
        }

        public Exception SetException(Exception exception)
        {
            while (exception.InnerException != null) exception = exception.InnerException;
            return exception;
        }
        public async System.Threading.Tasks.Task<User> UserExist(long userId)
        {
            return await Db.Users.Include(u => u.UserStores)
                .FirstOrDefaultAsync(us => us.Id == userId);
        }

        //protected void SendNotification(long userId, string deviceToken)
        //{
        //    try
        //    {
        //        var devices =                   
        //                Db.Devices.Where(device => device.UserId == userId && device.Token != deviceToken)
        //                    .Select(device => device.Token)
        //                    .ToList();
        //        if (!devices.Any()) return;
        //        var body = new NotificationBody();
        //        foreach (var device in devices)
        //        {
        //            body.DevicesList.Add(new Device
        //            {
        //                DeviceToken = device,
        //                Badg = 0
        //            });
        //        }
        //        body.P12FileName = @"C:\bitbucket\ahead\Distr_Certificates.p12";
        //        body.P12Password = "12345";
        //        body.GcmSenderId = "AIzaSyAwGeMSV7VDObL3gA2whZnhNJt2wm6HGow";
        //        body.AuthToken = "AIzaSyC5af_wFxmZ3u9HPJ9oElcTqqyXTznXVzA";
        //        body.Message = new Message
        //        {
        //            Title = "",
        //            Description = ""
        //        };
        //        Notification.SendNotification(body);
        //    }
        //    catch (Exception)
        //    {
        //        // ignored
        //    }
        //}
    }
}
