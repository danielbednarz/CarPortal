using API.Data;
using API.DTOs;
using API.Enums;
using API.Extensions;
using API.Interfaces.Repositories;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class OrganizerController : AppController
    {
        private readonly MainDatabaseContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizerRepository _organizerRepository;

        public OrganizerController(MainDatabaseContext context, IUserRepository userRepository, IOrganizerRepository organizerRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _organizerRepository = organizerRepository;
        }

        #region CarInsurance

        [HttpGet("getCarInsuranceTypes")]
        public ActionResult<CarInsuranceType> GetCarInsuranceTypes()
        {
            var data = EnumExtensions.GetValues<CarInsuranceType>();

            return Ok(data); 
        }

        [HttpGet("getCarInsuranceRemainingDays")]
        public async Task<ActionResult> GetCarInsuranceRemainingDays()
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user =  await _userRepository.GetUserByUsernameAsync(currentUsername);

            var data = await _organizerRepository.GetCarInsuranceRemainingDays(user.Id);

            return Ok(data);
        }

        [HttpGet("getCarInsurance")]
        public async Task<ActionResult<List<CarInsurance>>> GetCarInsurance()
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(currentUsername);

            var data = await _organizerRepository.GetCarInsurance(user.Id);

            return Ok(data);
        }

        [HttpPost("addCarInsurance")]
        public async Task<ActionResult<CarInsurance>> AddCarInsurance(CarInsuranceDto carInsurance)
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(currentUsername);

            var data = new CarInsurance()
            {
                CarInsuranceType = carInsurance.CarInsuranceType,
                Cost = carInsurance.Cost,
                ExpirationDate = carInsurance.ExpirationDate.AddDays(1),
                InsurerName = carInsurance.InsurerName,
                UserId = user.Id
            };

            await _context.CarInsurances.AddAsync(data);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("deleteCarInsurance/{carInsuranceId}")]
        public async Task<ActionResult> DeleteCarInsurance(int carInsuranceId)
        {
            var carInsurance = await _context.CarInsurances.FirstOrDefaultAsync(x => x.Id == carInsuranceId);

            if (carInsurance == null)
            {
                return NotFound();
            }

            _context.CarInsurances.Remove(carInsurance);
            await _userRepository.SaveAllAsync();

            return Ok();
        }

        #endregion

        #region PeriodicInspection

        [HttpGet("getPeriodicInspections")]
        public async Task<ActionResult> GetPeriodicInspections()
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(currentUsername);

            var data = await _organizerRepository.GetPeriodicInspections(user.Id);

            return Ok(data);
        }

        [HttpPost("addPeriodicInspection")]
        public async Task<ActionResult<CarInsurance>> AddPeriodicInspection(PeriodicInspectionDto periodicInspection)
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(currentUsername);

            var data = new PeriodicInspection()
            {
                InspectionDate = periodicInspection.InspectionDate.AddDays(1),
                isPositive = periodicInspection.isPositive,
                UserId = user.Id
            };

            await _context.PeriodicInspections.AddAsync(data);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("deletePeriodicInspection/{periodicInspectionId}")]
        public async Task<ActionResult> DeletePeriodicInspection(int periodicInspectionId)
        {
            var periodicInspection = await _context.PeriodicInspections.FirstOrDefaultAsync(x => x.Id == periodicInspectionId);

            if (periodicInspection == null)
            {
                return NotFound();
            }

            _context.PeriodicInspections.Remove(periodicInspection);
            await _userRepository.SaveAllAsync();

            return Ok();
        }

        [HttpGet("getPeriodicInspectionRemainingDays")]
        public async Task<ActionResult> GetPeriodicInspectionRemainingDays()
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(currentUsername);

            var data = await _organizerRepository.GetPeriodicInspectionRemainingDays(user.Id);

            return Ok(data);
        }

        #endregion

    }
}
