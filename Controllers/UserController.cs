using System;
using System.Net.Mime;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tidsbanken_BackEnd.Data.DTOs.UserDTOs;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Exceptions;
using Tidsbanken_BackEnd.Services;

namespace Tidsbanken_BackEnd.Controllers
{
    [Route("api/v1/Users")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ControllerBase
    {
        // Private field to store an instance of the ServiceFacade, providing access to User-related services.
        private readonly ServiceFacade _serviceFacade;

        // Private field to store an instance of the auto mapper.
        private readonly IMapper _mapper;

        // Constructor for the UserController, which takes the ServiceFacade as a dependency.
        public UserController(ServiceFacade serviceFacade, IMapper mapper)
        {
            // Initialize the serviceFacade field with the provided instance of ServiceFacade.
            _serviceFacade = serviceFacade;
            // Initialize the _mapper field with the provided instance of Imapper.
            _mapper = mapper;
        }


        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns>
        /// A list of Users
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(_mapper.Map<List<UserDTO>>(await _serviceFacade._userService.GetAllAsync()));
        }


        /// <summary>
        /// Retrieves a specific User by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the User.</param>
        /// <returns>
        /// A User object if found; otherwise, an error message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser()
        {
            string subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                return _mapper.Map<UserDTO>(await _serviceFacade._userService.GetByIdAsync(Guid.Parse(subject)));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates the details of a specific User based on the provided User object and unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the User to be updated.</param>
        /// <param name="userDTO">The User object containing the updated details.</param>
        /// <returns>
        /// Returns NoContent if the operation is successful; otherwise, BadRequest or NotFound based on the error.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest($"the id {id} given for User to be updated does not match the User id {userDTO.Id} given in the body");
            }
            try
            {
                await _serviceFacade._userService.UpdateAsync(_mapper.Map<User>(userDTO));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        /// <summary>
        /// Adds a new User to the database.
        /// </summary>
        /// <param name="userDTO">The User object to be added.</param>
        /// <returns>
        /// Returns a CreatedAtAction result, directing to the GetUser action to retrieve the newly added user; otherwise, an error response.
        /// </returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> PostUser()
        {
            string subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string username = User.Claims.FirstOrDefault(claim => claim.Type == "preferred_username").Value;
            string firstName = User.FindFirst(ClaimTypes.GivenName).Value;
            string lastName = User.FindFirst(ClaimTypes.Surname).Value;
            string email = User.FindFirst(ClaimTypes.Email).Value;
            User newUser = new User()
            {
                Id = Guid.Parse(subject),
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            await _serviceFacade._userService.AddAsync(newUser);
            return CreatedAtAction("GetUser", new { id = newUser.Id }, _mapper.Map<UserDTO>(newUser));
        }


        /// <summary>
        /// Deletes a specified User from the database.
        /// </summary>
        /// <param name="id">The unique ID of the User to be deleted.</param>
        /// <returns>
        /// Returns a NoContent response if deletion is successful; otherwise, a NotFound response with an error message
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _serviceFacade._userService.DeleteAsync(id);
                return NoContent();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}