using System;
using MVCApplicationFramework.Business;
using MVCApplicationFramework.Entities;
using System.Web.Http;

namespace MVCApplicationFramework.WebAPI.Controllers
{
    //[RoutePrefix("UserManager")]
    public class UserManagerController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        //[Route("GetUserByID")]
        public APIResponse GetUserByID(int userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                UserManager objUserManager = new UserManager();
                var result = objUserManager.GetUserByID(userId);

                response.MessageCode = "200";
                response.MessageText = "Operation Successfull";
                response.MessageType = EnumMessageType.OPERATION_SUCCESS;
                response.Add(result);
                response.HasException = false;

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message + ex.StackTrace);
                response.MessageCode = "500";
                response.HasException = true;
                response.MessageText = ex.Message;
                response.MessageType = EnumMessageType.OPERATION_APPLICATION_ERROR;
                response.Exception = ex.ToString();
            }

            return response;
        }

        [HttpGet]
        //[Route("GetUserList")]
        public APIResponse GetUserList()
        {
            APIResponse response = new APIResponse();
            try
            {
                UserManager objUserManager = new UserManager();
                var result = objUserManager.GetUserList();

                response.MessageCode = "200";
                response.MessageText = "Operation Successfull";
                response.MessageType = EnumMessageType.OPERATION_SUCCESS;
                response.Add(result);
                response.HasException = false;

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message + ex.StackTrace);
                response.MessageCode = "500";
                response.HasException = true;
                response.MessageText = ex.Message;
                response.MessageType = EnumMessageType.OPERATION_APPLICATION_ERROR;
                response.Exception = ex.ToString();
            }

            return response;
        }

        [HttpPost]
        //[Route("SaveUser")]
        public APIResponse SaveUser(User user)
        {
            APIResponse response = new APIResponse();
            try
            {
                UserManager objUserManager = new UserManager();
                var result = objUserManager.Save(user);

                response.MessageCode = "200";
                response.MessageText = "User Record Saved Successfull";
                response.MessageType = EnumMessageType.OPERATION_SUCCESS;
                response.Add(result);
                response.HasException = false;

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message + ex.StackTrace);
                response.MessageCode = "500";
                response.HasException = true;
                response.MessageText = ex.Message;
                response.MessageType = EnumMessageType.OPERATION_APPLICATION_ERROR;
                response.Exception = ex.ToString();
            }

            return response;
        }
    }
}