using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace WebApplication1.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            Service.DatabaseService db = new Service.DatabaseService();
            var list = db.LoadAllMember();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult Create(Models.Member newMember)
        {
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                var File = Request.Files[0];
                string dir = string.Format("~/Content/Image/");
                var TrueDir = System.Web.Hosting.HostingEnvironment.MapPath(dir);
                if (!System.IO.Directory.Exists(TrueDir))
                {
                    System.IO.Directory.CreateDirectory(TrueDir);
                }
                var SaveDir = System.IO.Path.Combine(TrueDir, File.FileName);

                File.SaveAs(SaveDir);

                newMember.ImageUrl = this.Url.Content(System.IO.Path.Combine(dir, File.FileName));



            }


            Service.DatabaseService db = new Service.DatabaseService();
            newMember.ID = Guid.NewGuid().ToString();
            db.CreateMember(newMember);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            Service.DatabaseService db = new Service.DatabaseService();
            var model = db.GetMemberByID(id);




            return View(model);
        }
        [HttpPost]
        public RedirectToRouteResult Update(Models.Member newMember)
        {
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                var File = Request.Files[0];
                string dir = string.Format("~/Content/Image/");
                var TrueDir = System.Web.Hosting.HostingEnvironment.MapPath(dir);
                if (!System.IO.Directory.Exists(TrueDir))
                {
                    System.IO.Directory.CreateDirectory(TrueDir);
                }
                var SaveDir = System.IO.Path.Combine(TrueDir, File.FileName);

                File.SaveAs(SaveDir);

                newMember.ImageUrl = this.Url.Content(System.IO.Path.Combine(dir, File.FileName));

                //System.Drawing.Image image = System.Drawing.Image.FromStream(File.InputStream);


            }


            Service.DatabaseService db = new Service.DatabaseService();
            db.UpdateMember(newMember);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            Service.DatabaseService db = new Service.DatabaseService();
            db.DeleteMember(id);
            return RedirectToAction("Index");
        }




    }
}