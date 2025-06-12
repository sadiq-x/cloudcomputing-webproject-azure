using System.Net;
using System.Text.Json;
using backend_api.Models;
using backend_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace backend_api.Functions
{
    public class CreatePerson
    {
        private readonly ICPersonRepository _personRepository;
        public CreatePerson(ICPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [Function("CreatePerson")] //Function to do login
        [Produces("application/json")]
        public async Task<HttpResponseData> Run1(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "create/person")] HttpRequestData req) //Create the Http req and res
        {

            var responseBody = await new StreamReader(req.Body).ReadToEndAsync();
            var createPersonModel = JsonSerializer.Deserialize<PersonModel>(responseBody);

            if (createPersonModel == null)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Person don't received." });
                return BadResponse;
            }

            var createdPerson = await _personRepository.CreatePerson(createPersonModel);

            if (createdPerson == null)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Person not created." });
                return BadResponse;
            }

            var OkResponse = req.CreateResponse(HttpStatusCode.OK);
            await OkResponse.WriteAsJsonAsync(new { createdPerson });
            return OkResponse;
        }
    }
}