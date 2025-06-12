using System.Net;
using System.Text.Json;
using backend_api.Models;
using backend_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace backend_api.Functions
{
    public class CreateCar
    {
        private readonly ICarRepository _carRepository;

        public CreateCar(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [Function("CreateCar")] //Function to do login
        [Produces("application/json")]
        public async Task<HttpResponseData> Run1(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "create/car")] HttpRequestData req) //Create the Http req and res
        {

            var responseBody = await new StreamReader(req.Body).ReadToEndAsync();
            var createCarModel = JsonSerializer.Deserialize<CarsModels>(responseBody);

            if (createCarModel == null)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Car don't received." });
                return BadResponse;
            }

            var createdCar = await _carRepository.CreateCar(createCarModel);

            if (createdCar == null)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await BadResponse.WriteAsJsonAsync(new { message = "Car not created." });
                return BadResponse;
            }

            var OkResponse = req.CreateResponse(HttpStatusCode.OK);
            await OkResponse.WriteAsJsonAsync(new { createdCar });
            return OkResponse;
        }
    }
}