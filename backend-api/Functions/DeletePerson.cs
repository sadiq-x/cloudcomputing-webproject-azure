using System.Net;
using System.Text.Json;
using backend_api.Models;
using backend_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace backend_api.Functions
{
    public class DeletePerson
    {
        private readonly ICPersonRepository _personRepository;
        public DeletePerson(ICPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [Function("DeletePerson")] //Function to do login
        [Produces("application/json")]
        public async Task<HttpResponseData> Run1(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "delete/person")] HttpRequestData req) //Create the Http req and res
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var deletePersonModel = JsonSerializer.Deserialize<PersonModel>(requestBody);

            if (deletePersonModel == null || deletePersonModel.Id <= 0)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Id don't received." });
                return BadResponse;
            }

            bool personDeleted = await _personRepository.DeletePerson(deletePersonModel);

            if (!personDeleted)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.NotFound);
                await BadResponse.WriteAsJsonAsync(new { message = "Id not found." });
                return BadResponse;
            }

            var OkResponse = req.CreateResponse(HttpStatusCode.OK);
            await OkResponse.WriteAsJsonAsync(new { message = "Person deleted." });
            return OkResponse;
        }
    }
}