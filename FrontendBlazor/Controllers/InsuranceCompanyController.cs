using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontendBlazor.Controllers
{
    [SecurityModel]
    public class InsuranceCompanyController : Controller
    {
        private PolicyTypeHelper policyTypeHelper;
        private PolicyClassHelper policyClassHelper;
        private InsuranceCompanyHelper InsuranceCompanyHelper;

        public InsuranceCompanyController()
        {
            policyTypeHelper = new PolicyTypeHelper();
			policyClassHelper = new PolicyClassHelper();
            InsuranceCompanyHelper = new InsuranceCompanyHelper();
        }

        public ActionResult Index()
        {
            List<InsuranceCompanyViewModel> InsuranceCompanyes = InsuranceCompanyHelper.GetAll();
            return View(InsuranceCompanyes);
        }
       

        // GET: InsuranceCompanyController/Details/5
        public ActionResult Details(int id)
        {
			InsuranceCompanyViewModel InsuranceCompany = InsuranceCompanyHelper.Get(id);

            
			List<int> ids = new List<int>();  
			List<PolicyTypeViewModel> types = policyTypeHelper.GetAll().Where(x => x.InsuraceCId == id).ToList();
            InsuranceCompany.types = types;

			foreach (var data in types)
            {
                ids.Add(data.PolicyClassId);
            }

			List<PolicyClassViewModel> classes = policyClassHelper.GetAll().Where(x => ids.Contains(x.PolicyclassId)).ToList();

            InsuranceCompany.classes = classes;
            


            return View(InsuranceCompany);
        }
        // POST: InsuranceCompanyController/Create
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

        // GET: InsuranceCompanyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InsuranceCompanyController/Edit/5
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

        // GET: InsuranceCompanyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InsuranceCompanyController/Delete/5
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
