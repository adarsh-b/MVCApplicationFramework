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
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(ConfigurationManager.AppSettings["WebAPIBaseURL"] + "UserManager/GetUserList").Result;

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    var apiResponseObject = JsonConvert.DeserializeObject<APIResponse>(response.Content.ReadAsStringAsync().Result);
                    if (apiResponseObject.MessageType == EnumMessageType.OPERATION_SUCCESS)
                    {
                        var users = JsonConvert.DeserializeObject<List<User>>(apiResponseObject.Data[0].ToString());
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

        public ActionResult Edit(int Id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(ConfigurationManager.AppSettings["WebAPIBaseURL"] + "UserManager/GetUserByID?userId=" + Convert.ToString(Id)).Result;

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    var apiResponseObject = JsonConvert.DeserializeObject<APIResponse>(response.Content.ReadAsStringAsync().Result);
                    if (apiResponseObject.MessageType == EnumMessageType.OPERATION_SUCCESS)
                    {
                        var user = JsonConvert.DeserializeObject<Entities.User>(apiResponseObject.Data[0].ToString());
                        return View("Edit", user);
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

        [HttpPost]
        public ActionResult Edit(Entities.User model)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(ConfigurationManager.AppSettings["WebAPIBaseURL"] + "UserManager/Save", new StringContent(JsonConvert.SerializeObject(model))).Result;

                if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                {
                    var apiResponseObject = JsonConvert.DeserializeObject<APIResponse>(response.Content.ReadAsStringAsync().Result);
                    if (apiResponseObject.MessageType == EnumMessageType.OPERATION_SUCCESS)
                    {
                       
                        return View("Index");
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