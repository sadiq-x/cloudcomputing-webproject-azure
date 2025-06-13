using System.Net;
using System.Text.Json;
using backend_api.Models;
using backend_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace backend_api.Functions
{
    public class UpdateCar
    {
        private readonly ICarRepository _carRepository;
        public UpdateCar(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [Function("UpdateCar")] //Function to do login
        [Produces("application/json")]
        public async Task<HttpResponseData> Run1(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "update/car")] HttpRequestData req) //Create the Http req and res
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updateCarModel = JsonSerializer.Deserialize<CarsModels>(requestBody);

            if (updateCarModel == null || updateCarModel.Id <= 0)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Car don't received." });
                return BadResponse;
            }

            var updatedCar = await _carRepository.UpdateCar(updateCarModel);

            if (!updatedCar)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Car not updated." });
                return BadResponse;
            }

            var OkResponse = req.CreateResponse(HttpStatusCode.OK);
            await OkResponse.WriteAsJsonAsync(new { message = "Car updated." });
            return OkResponse;
        }
    }
}