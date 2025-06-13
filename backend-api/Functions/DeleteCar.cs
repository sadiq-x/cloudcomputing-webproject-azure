using System.Net;
using System.Text.Json;
using backend_api.Models;
using backend_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace backend_api.Functions
{
    public class DeleteCar
    {
        private readonly ICarRepository _carRepository;

        public DeleteCar(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [Function("DeleteCar")] //Function to do login
        [Produces("application/json")]
        public async Task<HttpResponseData> Run1(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "delete/car")] HttpRequestData req) //Create the Http req and res
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var deleteCarModel = JsonSerializer.Deserialize<CarsModels>(requestBody);

            Console.WriteLine("Request body: " + requestBody);
            Console.WriteLine("Id received: " + deleteCarModel?.Id);

            if (deleteCarModel == null || deleteCarModel.Id <= 0)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Id don't received." });
                return BadResponse;
            }

            bool carDeleted = await _carRepository.DeleteCar(deleteCarModel);

            if (!carDeleted)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.NotFound);
                await BadResponse.WriteAsJsonAsync(new { message = "Id not found." });
                return BadResponse;
            }

            var OkResponse = req.CreateResponse(HttpStatusCode.OK);
            await OkResponse.WriteAsJsonAsync(new { message = "Car deleted." });
            return OkResponse;
        }
    }
}