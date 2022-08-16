using CRUD_Assignment.DB_Connection;
using CRUD_Assignment.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRUD_Assignment.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //This CRUD_Assignment Intigrated with Web_API.
        Uri APIurl = new Uri("https://localhost:44361/");

        HttpClient Client;
        public HomeController()
        {
            Client = new HttpClient();
            Client.BaseAddress = APIurl;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(ClientModel mdlclient)
        //{
        //    Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

        //    Client tblobj = new Client();

        //    tblobj.ID = mdlclient.ID;
        //    tblobj.Name = mdlclient.Name;
        //    tblobj.Phone = mdlclient.Phone;
        //    tblobj.Email = mdlclient.Email;
        //    tblobj.Address = mdlclient.Address;
        //    tblobj.Salary = mdlclient.Salary;
        //    tblobj.Designation = mdlclient.Designation;
        //    tblobj.Department = mdlclient.Department;


        //    if(mdlclient.ID != 0)
        //    {
        //        dbobj.Entry(tblobj).State = System.Data.Entity.EntityState.Modified;
        //        dbobj.SaveChanges();
        //        return RedirectToAction("Read");
        //    }
        //    else
        //    {
        //        dbobj.Client.Add(tblobj);
        //        dbobj.SaveChanges();
        //    }
            
        //    return RedirectToAction("Create");
        //}

        [HttpGet]
         public ActionResult CreateAPI()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAPI(ClientModel mdlobj)
        {
            String Jsondata = JsonConvert.SerializeObject(mdlobj);

            StringContent Content = new StringContent(Jsondata, Encoding.UTF8, "application/json");

            HttpResponseMessage hrm = Client.PostAsync(Client.BaseAddress + "Post/Create", Content).Result;

            if (hrm.IsSuccessStatusCode)
            {
                return RedirectToAction("CreateAPI");
            }
            return View();
        }

        //public ActionResult Read()
        //{
        //    Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

        //    var tbldata = dbobj.Client.ToList();

        //    List<ClientModel> mdlclient = new List<ClientModel>();

        //    foreach (var item in tbldata)
        //    {
        //        mdlclient.Add(new ClientModel 
        //        {
        //            ID = item.ID,
        //            Name = item.Name,
        //            Phone = item.Phone,
        //            Email = item.Email,
        //            Address = item.Address,
        //            Salary = item.Salary,
        //            Designation = item.Designation,
        //            Department = item.Department
        //        });
        //    }
        //    return View(mdlclient);
        //}

        public ActionResult ReadAPI()
        {
            List<ClientModel> mdlobj = new List<ClientModel>();

            HttpResponseMessage hrm = Client.GetAsync(Client.BaseAddress + "Get/Read").Result;

            if (hrm.IsSuccessStatusCode)
            {
                //fetching data in json formate
                String JsontData = hrm.Content.ReadAsStringAsync().Result;
                //converting data json to boject type
                mdlobj = JsonConvert.DeserializeObject<List<ClientModel>>(JsontData);
            }

            return View(mdlobj);
        }

        //public ActionResult Update(int ID)
        //{
        //    Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

        //    var updatedata = dbobj.Client.Where(m => m.ID == ID).First();

        //    ClientModel mdlobj = new ClientModel();

        //    mdlobj.Name = updatedata.Name;
        //    mdlobj.Phone = updatedata.Phone;
        //    mdlobj.Email = updatedata.Email;
        //    mdlobj.Address = updatedata.Address;
        //    mdlobj.Salary = updatedata.Salary;
        //    mdlobj.Designation = updatedata.Designation;
        //    mdlobj.Department = updatedata.Department;

        //    ViewBag.Update = "Update";

        //    return View("Create", mdlobj);
        //}

        [HttpGet]
        public ActionResult UpdateAPI(int ID)
        {
            ClientModel mdlobj = new ClientModel();

            HttpResponseMessage hrm = Client.GetAsync(Client.BaseAddress + "Get/Update" + "?" + "ID" + "=" + ID.ToString()).Result;
            if (hrm.IsSuccessStatusCode)
            {
                //fetching data in json formate
                String JsontData = hrm.Content.ReadAsStringAsync().Result;
                //converting data json to boject type
                mdlobj = JsonConvert.DeserializeObject<ClientModel>(JsontData);
            }
            return View("CreateAPI", mdlobj);
        }

        //public ActionResult Delete(int ID)
        //{
        //    Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

        //    var Deletedata = dbobj.Client.Where(m => m.ID == ID).First();

        //    dbobj.Client.Remove(Deletedata);

        //    dbobj.SaveChanges();
        //    return RedirectToAction("Read");
        //}
        [HttpGet]
        public ActionResult DeleteAPI(int ID)
        {
            HttpResponseMessage hrm = Client.GetAsync(Client.BaseAddress + "Get/Delete" + "?" + "ID" + "=" + ID.ToString()).Result;

            if (hrm.IsSuccessStatusCode)
            {
                return RedirectToAction("ReadAPI");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(AdminUserModel mdlobj)
        {
            Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

            AdminUser tblobj = new AdminUser();

            tblobj.ID = mdlobj.ID;
            tblobj.Name = mdlobj.Name;
            tblobj.Email = mdlobj.Email;
            tblobj.Password = mdlobj.Password;
            tblobj.V_Code = mdlobj.V_Code;

            dbobj.AdminUser.Add(tblobj);
            dbobj.SaveChanges();

            return View("SignIn");
        }

        public ActionResult UserList()
        {
            Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

            var tbldata = dbobj.AdminUser.ToList();

            List<AdminUserModel> mdlobj = new List<AdminUserModel>();

            foreach (var item in tbldata)
            {
                mdlobj.Add(new AdminUserModel 
                {
                    ID = item.ID,
                    Name = item.Name,
                    Email = item.Email,
                    Password = item.Password,
                    V_Code = (int)item.V_Code,
                });
            }
            return View(mdlobj);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(AdminUserModel mdladmin)
        {
            Alok_KushwahaEntities dbobj = new Alok_KushwahaEntities();

            var AdminData = dbobj.AdminUser.Where(m => m.Email == mdladmin.Email).FirstOrDefault();

            if(AdminData == null)
            {
                TempData["Email"] = "Email Not Found or Invalid User";
            }
            else
            {
                if(AdminData.Email == mdladmin.Email && AdminData.Password == mdladmin.Password)
                {
                    FormsAuthentication.SetAuthCookie(AdminData.Email, false);
                    Session["Email"] = AdminData.Email;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Password"] = "Wrong Email or Password";
                }
            }

            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["Email"] = null;
            return RedirectToAction("Index");
        }

    }
}