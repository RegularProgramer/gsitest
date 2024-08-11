using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontendBlazor.Controllers
{
    [SecurityModel]
    public class GoodController : Controller
    {
        


 
        private GoodHelper goodHelper;
        public GoodController()
        {
                goodHelper = new GoodHelper();
        }



        // GET: PolicyController
        public ActionResult Index()
        {
            List<GoodViewModel> goods = goodHelper.GetAll();
            return View(goods);
        }

        // GET: PolicyController/Details/5
        public ActionResult Details(int id)
        {

            GoodViewModel Good = goodHelper.Get(id);
         

            return View(Good);
        }

        // GET: PolicyController/Create
        public ActionResult Create()

        {
            GoodViewModel model = new GoodViewModel();


           

            return View(model);
        }
        /*
        [HttpPost]
        public async Task<List<PolicyTypeViewModel>> getByCompanyId(int id)
        {

            List<PolicyTypeViewModel> types = new List<PolicyTypeViewModel>();
            // types = (List<PolicyTypeViewModel>)policyTypeHelper.GetAll().Where(x => x.InsuraceCId == id);

            var Types = policyTypeHelper.GetAll().Where(p => p.InsuraceCId == id).ToList();

            Types.ForEach((p) =>
            {
                p.PolicyClasses = policyClassHelper.Get(p.PolicyclassId);

            }

            );

            return Types;

        }*/
        /*
        [HttpGet]
        public async Task<List<UserViewModel>> getClients()
        {

            var users = userHelper.GetAll().Where(x => x.UserRole == 1).ToList();



            return users;
        }
        */


        // POST: PolicyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GoodViewModel GoodViewModel)
        {
            try
            {
                goodHelper.Create(GoodViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyController/Edit/5
        public ActionResult Edit(int id)
        {
            GoodViewModel goodViewModel = goodHelper.Get(id);

            return View(goodViewModel);
        }

        // POST: PolicyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GoodViewModel goodViewModel)
        {
            try
            {
                goodHelper.Update(goodViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyController/Delete/5
        public ActionResult Delete(int id)
        {
            GoodViewModel good = goodHelper.Get(id);
            return View(good);
        }

        // POST: PolicyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(GoodViewModel good)
        {
            try
            {
                goodHelper.Delete(good.IdGood);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
