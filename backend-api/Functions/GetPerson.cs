using System.Net;
using backend_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace backend_api.Functions
{
    public class GetPerson
    {
        private readonly ICPersonRepository _personRepository;
        public GetPerson(ICPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [Function("GetPerson")] //Function to do login
        [Produces("application/json")]
        public async Task<HttpResponseData> Run1(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "get/person")] HttpRequestData req) //Create the Http req and res
        {
            var personGet = await _personRepository.GetPersons();

            if (personGet == null)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.NotFound);
                await BadResponse.WriteAsJsonAsync(new { message = "Person not founds." });
                return BadResponse;
            }

            var OkResponse = req.CreateResponse(HttpStatusCode.OK);
            await OkResponse.WriteAsJsonAsync(new { personGet });
            return OkResponse;
        }
    }
}