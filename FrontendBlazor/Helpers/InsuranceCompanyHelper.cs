using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class InsuranceCompanyHelper
    {
        private ServiceRepository repository;


        public InsuranceCompanyHelper()
        {
            repository = new ServiceRepository();
        }




        public List<InsuranceCompanyViewModel> GetAll()
        {
            List<InsuranceCompanyViewModel> list = new List<InsuranceCompanyViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/InsuranceCompany");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<InsuranceCompanyViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }

        public InsuranceCompanyViewModel Get(int id)
        {
            InsuranceCompanyViewModel company;


            HttpResponseMessage message = repository.GetResponse("api/InsuranceCompany/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            company = JsonConvert.DeserializeObject<InsuranceCompanyViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return company;

        }

        public InsuranceCompanyViewModel Create(InsuranceCompanyViewModel company)
        {
            InsuranceCompanyViewModel companyViewModel;


            HttpResponseMessage message = repository.PostResponse("api/InsuranceCompany/", company);

            var content = message.Content.ReadAsStringAsync().Result;

            companyViewModel = JsonConvert.DeserializeObject<InsuranceCompanyViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return companyViewModel;

        }


        public InsuranceCompanyViewModel Update(InsuranceCompanyViewModel company)
        {
            InsuranceCompanyViewModel companyViewModel;


            HttpResponseMessage message = repository.PutResponse("api/InsuranceCompany/", company);

            var content = message.Content.ReadAsStringAsync().Result;

            companyViewModel = JsonConvert.DeserializeObject<InsuranceCompanyViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return companyViewModel;

        }

        public InsuranceCompanyViewModel Delete(int id)
        {
            InsuranceCompanyViewModel company;


            HttpResponseMessage message = repository.DeleteResponse("api/InsuranceCompany/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            company = JsonConvert.DeserializeObject<InsuranceCompanyViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return company;

        }


    }
}
