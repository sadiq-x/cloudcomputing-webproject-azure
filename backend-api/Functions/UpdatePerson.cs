using System.Net;
using System.Text.Json;
using backend_api.Models;
using backend_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace backend_api.Functions
{
    public class UpdatePerson
    {
        private readonly ICPersonRepository _personRepository;
        public UpdatePerson(ICPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [Function("UpdatePerson")] //Function to do login
        [Produces("application/json")]
        public async Task<HttpResponseData> Run1(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "update/person")] HttpRequestData req) //Create the Http req and res
        {
            var responseBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatePersonModel = JsonSerializer.Deserialize<PersonModel>(responseBody);

            if (updatePersonModel == null)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Person don't received." });
                return BadResponse;
            }

            var updatedPerson = await _personRepository.UpdatePerson(updatePersonModel);

            if (updatedPerson)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Person not updated." });
                return BadResponse;
            }

            var OkResponse = req.CreateResponse(HttpStatusCode.OK);
            await OkResponse.WriteAsJsonAsync(new { message = "Person created." });
            return OkResponse;
        }
    }
}