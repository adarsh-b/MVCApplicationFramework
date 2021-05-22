using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVCApplicationFramework.Entities;
using Newtonsoft.Json;

namespace MVCApplicationFramework.WebUI.Controllers
{
    public class UserManagerController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            List<User> users = new List<Entities.User>();
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(ConfigurationManager.AppSettings["WebAPIBaseURL"] + "UserManager/GetUserList").Result;

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    var apiResponseObject = JsonConvert.DeserializeObject<APIResponse>(response.Content.ReadAsStringAsync().Result);
                    if (apiResponseObject.MessageType == EnumMessageType.OPERATION_SUCCESS)
                    {
                        users = JsonConvert.DeserializeObject<List<User>>(apiResponseObject.Data[0].ToString());
                        return View(users);
                    }
                    else
                    {
                        throw new Exception(apiResponseObject.Exception);
                    }
                }
                else
                {
                    log.Error("Error StatusCode:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase);
                    throw new Exception(response.StatusCode.ToString());
                }
            }
        }
    }
}