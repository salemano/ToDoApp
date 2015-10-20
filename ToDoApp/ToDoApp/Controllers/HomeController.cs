using System.Linq;
using System.Web.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private const string CookieName = "Tasks";

        private const string StrSeparator = ",";

        private const char CharSeparator = ',';

        //
        // GET: /Home/
        public ActionResult Index()
        {
            var model = new TaskModel();
            var session = Session[CookieName];

            //read session or initialize
            if (session != null)
            {
                var data = session.ToString().Split(CharSeparator);
                model.Tasks = data;
            }
            else
            {
                Session[CookieName] = string.Empty;
                model.Tasks = new string[] { };
            }

            return this.View(model);
        }

        public ActionResult AddTask(string task)
        {
            if (!string.IsNullOrEmpty(task))
            {
                var session = Session[CookieName].ToString();

                if (string.IsNullOrEmpty(session))
                {
                    Session[CookieName] = task;
                }
                else
                {
                    Session[CookieName] = task + StrSeparator + session;
                }
            }

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveTask(string task)
        {
            var session = Session[CookieName].ToString().Split(CharSeparator).ToList();
            session.Remove(task);
            var newSession = string.Join(StrSeparator, session.ToArray());
            Session[CookieName] = newSession;
            return this.RedirectToAction("Index", "Home");
        }
    }
}