using System.Net;
using backend_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace backend_api.Functions
{
    public class GetCar
    {
        private readonly ICarRepository _carRepository;
        public GetCar(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [Function("GetCar")] //Function to do login
        [Produces("application/json")]
        public async Task<HttpResponseData> Run1(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "get/car")] HttpRequestData req) //Create the Http req and res
        {
            var carGet = await _carRepository.GetCars();

            if (carGet == null)
            {
                var BadResponse = req.CreateResponse(HttpStatusCode.NotFound);
                await BadResponse.WriteAsJsonAsync(new { message = "Cars not founds." });
                return BadResponse;
            }

            var OkResponse = req.CreateResponse(HttpStatusCode.OK);
            await OkResponse.WriteAsJsonAsync(new { carGet });
            return OkResponse;
        }
    }
}