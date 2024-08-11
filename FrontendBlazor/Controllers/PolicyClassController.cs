using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontendBlazor.Controllers
{
    [SecurityModel]
    public class PolicyClassController : Controller
    {

        private PolicyTypeHelper policyTypeHelper;
        private InsuranceCompanyHelper insuranceCompanyHelper;
        private PolicyClassHelper policyClassHelper;

        public PolicyClassController()
        {
            policyTypeHelper = new PolicyTypeHelper();
            insuranceCompanyHelper = new InsuranceCompanyHelper();
            policyClassHelper = new PolicyClassHelper();
        }

        public ActionResult Index()
        {
            List<PolicyClassViewModel> policyClasses = policyClassHelper.GetAll();
            return View(policyClasses);
        }
        [HttpGet]
        [Route("PolizasEmpresas")]


        // GET: PolicyClassController
        public ActionResult IndexForCompanies()
        {
            List<PolicyClassViewModel> policyTypes = policyClassHelper.GetAll();
            return View(policyTypes);
        }

        [HttpGet]
        [Route("PolizasPersonales")]
        // GET: PolicyClassController
        public ActionResult IndexForPeople()
        {
            List<PolicyClassViewModel> policyTypes = policyClassHelper.GetAll();
            return View(policyTypes);
        }

        // GET: PolicyClassController/Details/5
        public ActionResult Details(int id)
        {



            PolicyClassViewModel Policyclass = policyClassHelper.Get(id);


            return View(Policyclass);
        }
        // POST: PolicyClassController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: PolicyClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PolicyClassController/Edit/5
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

        // GET: PolicyClassController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PolicyClassController/Delete/5
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
