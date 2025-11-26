using FitSammenDekstopClient.Model;
using FitSammenDekstopClient.ServiceLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSammenDekstopClient.BusinessLogicLayer
{
    public class ClassLogic
    {
        private IClassService _classService;

        public ClassLogic(IConfiguration inConfiguration)
        {
            _classService = new ClassService(inConfiguration);
        }

        public async Task<IEnumerable<Class>?> GetAllClassesAsync()
        {
            return await _classService.GetAllClassesAsync();
        }

        public async Task<CreateClassResponse?> CreateClassAsync(CreateClassRequest request)
        {
            // Måske skal der valideres input fra requesten her
            // Bla bla bla
            var response = await _classService.CreateClassAsync(request);

            if (response == null)
            {
                return new CreateClassResponse
                {
                    Status = CreateClassStatus.Error,
                    Message = "Holdet kunne ikke oprettes. Ukendt fejl"
                };
            }

            return response;
        }
    }
}
