using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tidsbanken_BackEnd.Data.DTOs.UserDTOs;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Exceptions;
using Tidsbanken_BackEnd.Services;


namespace Tidsbanken_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Private field to store an instance of the ServiceFacade, providing access to user-related services.
        private readonly ServiceFacade _serviceFacade;

        // Private field to store an instance of the auto mapper.
        private readonly IMapper _mapper;

        // Constructor for the UserController, which takes a ServiceFacade as a dependency.
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
        /// <returns>A list of characters.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(_mapper.Map<List<UserDTO>>(await _serviceFacade._userService.GetAllAsync()));
        }


        /// <summary>
        /// Retrieves a specific Character by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the character.</param>
        /// <returns>A Character object if found; otherwise, an error message.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            try
            {
                return _mapper.Map<UserDTO>(await _serviceFacade._userService.GetByIdAsync(id));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates the details of a specific Character based on the provided character object and unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the character to be updated.</param>
        /// <param name="userDTO">The character object containing the updated details.</param>
        /// <returns>Returns NoContent if the operation is successful; otherwise, BadRequest or NotFound based on the error.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest($"the id {id} given for character to be updated does not match the character id {userDTO.Id} given in the body");
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
        /// Adds a new Character to the database.
        /// </summary>
        /// <param name="userDTO">The character object to be added.</param>
        /// <returns>Returns a CreatedAtAction result, directing to the GetCharacter action to retrieve the newly added character; otherwise, an error response.</returns>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDTO)
        {
            var newCharacter = await _serviceFacade._userService.AddAsync(_mapper.Map<Character>(userDTO));

            return CreatedAtAction("GetCharacter", new { id = newCharacter.Id }, _mapper.Map<CharacterDTO>(newCharacter));
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
