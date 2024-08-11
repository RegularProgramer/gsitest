using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace FrontendBlazor.Controllers
{
    [SecurityModel]
    public class PolicyController : Controller
    {


        private PolicyHelper policyHelper;
        private PolicyStateHelper policyStateHelper;
        private PolicyTypeHelper policyTypeHelper;
        private InsuranceCompanyHelper insuranceCompanyHelper;
        private LogCambioHelper logCambioHelper;
        private UserHelper userHelper;
        private PolicyClassHelper policyClassHelper;
        private GoodHelper goodHelper;
        private GoodTypeHelper goodTypeHelper;
        public List<PolicyStateViewModel> states = new List<PolicyStateViewModel>();
        public List<PolicyTypeViewModel> types = new List<PolicyTypeViewModel>();
        public List<DniViewModel> dnis = new List<DniViewModel>();
        public List<QPolicyViewModel> policies = new List<QPolicyViewModel>();
        public PolicyController()
        {

            policyHelper = new PolicyHelper();
            policyStateHelper = new PolicyStateHelper();
            policyTypeHelper = new PolicyTypeHelper();
            insuranceCompanyHelper = new InsuranceCompanyHelper();
            logCambioHelper = new LogCambioHelper();
            policyClassHelper = new PolicyClassHelper();
            userHelper = new UserHelper();
            goodHelper = new GoodHelper();
            goodTypeHelper = new GoodTypeHelper();
            states = policyStateHelper.GetAll();
            types = policyTypeHelper.GetAll();
            dnis = policyHelper.GetAllDni();
            policies = policyHelper.GetAll();

        }



        // GET: PolicyController
        public ActionResult Index()
        {
            List<QPolicyViewModel> policies = policyHelper.GetAll();

            string roleId = HttpContext.Session.GetString("RoleId");
            string idNumber = HttpContext.Session.GetString("Identification");

            if (roleId == "2")
            {
                policies = policies.Where(p => p.Identification == idNumber).ToList();
            }

            foreach (var item in policies)
            {
                item.policyState = states.FirstOrDefault(x => x.IdPolicyState == item.IdPolicyState).StateName;
                item.policyType = types.FirstOrDefault(x => x.PolicyTypeId == item.PolicyTypeId).PolicyName;
                //item.policyState = states.FirstOrDefault(x => x.IdPolicyState == item.IdPolicyState).StateName;

            }

            return View(policies);
        }

        // GET: PolicyController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            QPolicyViewModel policy =  await policyHelper.Get(id);
            IEnumerable<LogCambioViewModel> history= logCambioHelper.GetAll().Where(x => x.Qpolicy == id);


           history = history.OrderByDescending(x => x.FechaAccion);

            

            policy.History = history;


           PolicyTypeViewModel type = policyTypeHelper.Get(policy.PolicyTypeId);
            InsuranceCompanyViewModel insurance = insuranceCompanyHelper.Get(type.InsuraceCId);
            policy.InsuranceCompany = insurance;
            PolicyClassViewModel policyClass = policyClassHelper.Get(type.PolicyClassId);
            List<QPolicyViewModel> policies = policyHelper.GetAll();

            policy.states = states;

            policy.policyState = states.FirstOrDefault(x => x.IdPolicyState == policy.IdPolicyState).StateName;
            policy.policyType = types.FirstOrDefault(x => x.PolicyTypeId == policy.PolicyTypeId).PolicyName;
                   //item.policyState = states.FirstOrDefault(x => x.IdPolicyState == item.IdPolicyState).StateName;

           foreach(var item in policy.History)
            {
                item.State = states.Where(x => x.IdPolicyState == item.IdPolicyState).FirstOrDefault().StateName;
            }

            policy.dniViewModels = dnis;

            

            policy.policyClass2 = policyClass;

            return View(policy);
        }

        // GET: PolicyController/Create
        public ActionResult Create()

        {
            
            QPolicyViewModel model = new QPolicyViewModel();


            ViewData["states"] = new SelectList(policyStateHelper.GetAll(), "IdPolicyState", "StateName");

            List<InsuranceCompanyViewModel> companies = insuranceCompanyHelper.GetAll();

            model.InsuranceCompanys = companies;

            ViewData["companies"] = new SelectList(insuranceCompanyHelper.GetAll(), "InsuranceCompanyId", "InsuranceCompanyName");


            GoodViewModel good = new GoodViewModel();

            model.Good = good;

            ViewData["clients"] = new SelectList(userHelper.GetAll(), "IdUser", "Dni");


            ViewData["types"] = new SelectList(policyTypeHelper.GetAll(), "PolicyTypeId", "PolicyTypeId");

            ViewData["classes"] = new SelectList(policyStateHelper.GetAll(), "PolicyclassId", "PolicyName");

            ViewData["types_good"] = new SelectList(goodTypeHelper.GetAll(), "GoodTypeId", "GoodName");


            


            return View(model);
        }

        [HttpPost]
        public async Task<List<PolicyTypeViewModel>> getByCompanyId(int id)
        {

            List<PolicyTypeViewModel> types = new List<PolicyTypeViewModel>();
            // types = (List<PolicyTypeViewModel>)policyTypeHelper.GetAll().Where(x => x.InsuraceCId == id);

            var Types = policyTypeHelper.GetAll().Where(p => p.InsuraceCId == id).ToList();

            Types.ForEach((p) =>
            {
                p.PolicyClasses = policyClassHelper.Get(p.PolicyClassId);

            }

            );

            return Types;

        }

        [HttpGet]
        public async Task<List<UserViewModel>> getClients()
        {

            var users = userHelper.GetAll().Where(x => x.UserRole == 1).ToList();



            return users;
        }



        // POST: PolicyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QPolicyViewModel qPolicyViewModel)
        {
            try
            {
                policyHelper.Create(qPolicyViewModel);

                generateLog(qPolicyViewModel.IdPolicy, "AGREGADO");
               
               


                return RedirectToAction("PolicySearched");
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            QPolicyViewModel policyViewModel =await policyHelper.Get(id);


            ViewData["states"] = new SelectList(policyStateHelper.GetAll(), "IdPolicyState", "StateName");

            List<InsuranceCompanyViewModel> companies = insuranceCompanyHelper.GetAll();

            policyViewModel.InsuranceCompanys = companies;

            ViewData["companies"] = new SelectList(insuranceCompanyHelper.GetAll(), "InsuranceCompanyId", "InsuranceCompanyName");


            GoodViewModel good = new GoodViewModel();

            policyViewModel.Good = good;

            policyViewModel.PolicyType2 = policyTypeHelper.Get(policyViewModel.PolicyTypeId);

            ViewData["clients"] = new SelectList(userHelper.GetAll().Where(x => x.UserRole == 1), "IdUser", "Dni");

            ViewData["employees"] = new SelectList(userHelper.GetAll().Where(x => x.UserRole == 2), "IdUser", "Dni");

            ViewData["types"] = new SelectList(policyTypeHelper.GetAll(), "PolicyTypeId", "PolicyTypeId");

            ViewData["classes"] = new SelectList(policyStateHelper.GetAll(), "PolicyclassId", "PolicyName");

            ViewData["types_good"] = new SelectList(goodTypeHelper.GetAll(), "GoodTypeId", "GoodName");


            return View(policyViewModel);
        }

        // POST: PolicyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QPolicyViewModel qPolicyViewModel)
        {
            try
            {
                qPolicyViewModel.lastUser = HttpContext.Session.GetString("Email").ToString();
                qPolicyViewModel.modHow = "Manual";
                policyHelper.Update(qPolicyViewModel);
                var id = qPolicyViewModel.IdPolicy;
                ViewData["Success"] = "true";
                ViewBag.Success = true;
                
                //  generateLog(qPolicyViewModel.IdPolicy, "MODIFICACION");
                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            QPolicyViewModel qPolicy = await policyHelper.Get(id);    
            return View(qPolicy);
        }

        // POST: PolicyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(QPolicyViewModel qPolicy)
        {
            try
            {
                policyHelper.Delete(qPolicy.IdPolicy);
                generateLog(qPolicy.IdPolicy, "BORRADO");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


            [HttpPost("DeletePolicy/{qpolicy}")]
       
            public  async Task<ActionResult> DeletePolicy(int qPolicy)
            {
                try
                {

                    policyHelper.Delete(qPolicy);
                    //generateLog(qPolicy, "BORRADO");
                    return StatusCode(200);
                }
                catch
                {
                    return StatusCode(402);
                }
            }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public   ActionResult Details(QPolicyViewModel qPolicyViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var newQpolicy =  policyHelper.Update(qPolicyViewModel);

                        return RedirectToAction("Details", newQpolicy);
                    }catch(Exception e)
                    {
                        var message = e.ToString();

                        return RedirectToAction("Details", qPolicyViewModel);
                    }
                        
                }
                else
                {
                    var a = ModelState.ValidationState;
                    return View(a);
                }
            }
            catch
            {
                return View();
            }
        }



        [HttpPost]
        public async Task<string> ChangeState(int idpolicy, int state)
        {
            try
            {
                QPolicyViewModel qPolicy = await policyHelper.Get(idpolicy);

                qPolicy.IdPolicyState = state;

                qPolicy.lastUser = HttpContext.Session.GetString("Email").ToString();
                qPolicy.modHow = "Manual";
                policyHelper.Update(qPolicy);
               // generateLog(idpolicy, "MODIFICACION");
                return new string("Su poliza fue actualizada con exito");

            }
            catch (Exception e)
            {
                return new string(e.Message);
            }


        }


        // GET: PolicyController/Delete/5
        [HttpGet]
        public async Task<IActionResult> GetPolicyJson(string id)
        {
            try
            {
                string userid = HttpContext.Session.Get("Email").ToString();
                QPolicyViewModel qPolicy = await policyHelper.GetSeacrhedPolicy(id,userid);
                if (qPolicy != null)
                {



                    qPolicy.Found = true;
                    return View("_PolicyPartialView", qPolicy);
                }

                else {
                    QPolicyViewModel qPolicy2 = new QPolicyViewModel();
                    qPolicy2.Found = false;
                    return View("_PolicyPartialView", qPolicy2);
                    
                        }
            }
            catch (Exception e)
            {
                QPolicyViewModel qPolicy = new QPolicyViewModel();
                qPolicy.Found = false;
                return View("_PolicyPartialView" ,qPolicy);   

            }
            
        }


        public void generateLog(int id ,string action)
        {
            var newLog = new LogViewModel
            {
                ActionMake = action,
                userId = HttpContext.Session.GetString("UserId"),
                modObject = id,
                TableReference = "QPolicy",
                LogTimestamp = DateTime.Now,
                IdUser = HttpContext.Session.GetString("UserId")

            };

            logCambioHelper.Create(newLog);

        }


        //Check if the policy Number is already in the collection
        [HttpGet]
        public async Task<bool> checkExist(string policyNumber)
        {
            bool confirmed = false; 
            try
            {
                QPolicyViewModel test = policyHelper.GetAll().Where(x => x.PolicyNumber == policyNumber).First();

                if(test != null) {

                    confirmed = true;
                    return confirmed;
                }
                else
                {
                    return confirmed;
                }

            }
            catch (Exception e)
            {

                return confirmed;
            }

        }

    }
}
