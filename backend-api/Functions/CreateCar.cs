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
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "create/car")] HttpRequestData req) //Create the Http req and res
        {

            var createCarModel = await req.ReadFromJsonAsync<CarsModels>();
            var responseBody = await new StreamReader(req.Body).ReadToEndAsync();
            //Console.WriteLine(responseBody.ToString());
            //var createCarModel = JsonSerializer.Deserialize<CarsModels>(responseBody);
            if (createCarModel == null || string.IsNullOrEmpty(createCarModel.Mark) || string.IsNullOrEmpty(createCarModel.Color) || string.IsNullOrEmpty(createCarModel.Km) || string.IsNullOrEmpty(createCarModel.Model) || string.IsNullOrEmpty(createCarModel.Price) || string.IsNullOrEmpty(createCarModel.Year)) 
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
            Console.WriteLine(createdCar.ToString());

            var OkResponse = req.CreateResponse(HttpStatusCode.OK);
            await OkResponse.WriteAsJsonAsync(new { createdCar });
            return OkResponse;
        }
    }
}