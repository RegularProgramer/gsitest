using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontendBlazor.Controllers
{
    [SecurityModel]
    public class PolicyTypeController : Controller
    {


        private PolicyTypeHelper policyTypeHelper;
        private InsuranceCompanyHelper insuranceCompanyHelper;
        private PolicyClassHelper policyClassHelper;

        public PolicyTypeController()
        {
            policyTypeHelper = new PolicyTypeHelper();
            insuranceCompanyHelper = new InsuranceCompanyHelper();
            policyClassHelper = new PolicyClassHelper();
        }



        // GET: PolicyTypeController
        public ActionResult Index()
        {
            List<PolicyTypeViewModel> policyTypes = policyTypeHelper.GetAll();
            return View();
        }
        [HttpGet]
        [Route("PolizasEmpresas")]
        // GET: PolicyTypeController
        public ActionResult IndexForCompanies()
        {
            List<PolicyTypeViewModel> policyTypes = policyTypeHelper.GetAll();
            return View();
        }

        [HttpGet]
        [Route("PolizasPersonales")]
        // GET: PolicyTypeController
        public ActionResult IndexForPeople()
        {
            List<PolicyTypeViewModel> policyTypes = policyTypeHelper.GetAll();
            return View();
        }

        // GET: PolicyTypeController/Details/5
        public ActionResult Details(int id)
        {

    

            PolicyTypeViewModel type = policyTypeHelper.Get(id);
     

            return View(type);
        }

        // GET: PolicyController/Create
        public ActionResult Create()

        {

           
            return View();
        }

        [HttpPost]
        public async Task<List<PolicyTypeViewModel>> getByCompanyId(int id)
        {

            List<PolicyTypeViewModel> types = new List<PolicyTypeViewModel>();
            // types = (List<PolicyTypeViewModel>)policyTypeHelper.GetAll().Where(x => x.InsuraceCId == id);

            var Types = policyTypeHelper.GetAll().Where(p => p.InsuraceCId == id).ToList();

            Types.ForEach((p) =>
            {
                p.PolicyClasses = policyClassHelper.Get(p.InsuraceCId);

            }

            );

            return Types;

        }
        /*
        [HttpGet]
        public async Task<List<UserViewModel>> getClients()
        {

            var users = userHelper.GetAll().Where(x => x.UserRole == 1).ToList();



            return users;
        }*/



        // POST: PolicyTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PolicyTypeViewModel typeViewModel)
        {
            try
            {
                policyTypeHelper.Create(typeViewModel);

                


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
		// GET: PolicyTypeController/Edit/5
		public ActionResult Edit(int id)
		{
			PolicyTypeViewModel policyViewModel = policyTypeHelper.Get(id);


			return View(policyViewModel);
		}

		// POST: PolicyTypeController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: PolicyTypeController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: PolicyTypeController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}



}

