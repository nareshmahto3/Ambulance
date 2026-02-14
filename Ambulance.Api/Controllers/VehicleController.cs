using Ambulance.Api.DTOs;
using Ambulance.Api.Interfaces;
using Ambulance.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Ambulance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicle _vehicleService;

        public VehicleController(IVehicle vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicleId = await _vehicleService.CreateVehicleAsync(dto);

            return Ok(new
            {
                Message = "Vehicle created successfully",
                VehicleId = vehicleId
            });
        }
        [HttpPut()]
        public async Task<IActionResult> UpdateVehicle([FromBody] VehicleDto dto)
        {
            if (dto==null)
            {
                return BadRequest("Vehicle ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _vehicleService.UpdateVehicleAsync(dto);

            if (!result)
            {
                return NotFound("Vehicle not found.");
            }

            return Ok(new
            {
                success = true,
                message = "Vehicle updated successfully"
            });
        }
    }
}



        //[HttpGet]
        //[Route("GetById/{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var result = await _mediator.Send(new GetAdmissionByIdQuery(id));
        //    if (result == null)
        //        return NotFound();
        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        //{
        //    var filter = new PaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
        //    var result = await _mediator.Send(new GetAllAdmissionQuery(filter));
        //    return Ok(result);
        //}

        //[HttpPost]
        //[Route("AddNew")]
        //public async Task<IActionResult> AddProduct(AdmissionDto product)
        //{
        //    var result = await _mediator.Send(new AddAdmissionCommand(product));
        //    return result.IsSuccess ? Ok(result) : BadRequest(result);

        //}

        //[HttpPut]
        //[Route("Update")]
        //public async Task<IActionResult> UpdateProduct(AdmissionDto product)
        //{
        //    var result = await _mediator.Send(new UpdateAdmissionCommand(product));
        //    return result.IsSuccess ? Ok(result) : BadRequest(result);

        //}
    


