using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tidsbanken_BackEnd.Data.DTOs.CommentDTOs;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Exceptions;
using Tidsbanken_BackEnd.Services;

namespace Tidsbanken_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // Private field to store an instance of the ServiceFacade, providing access to Comment-related services.
        private readonly ServiceFacade _serviceFacade;

        // Private field to store an instance of the auto mapper.
        private readonly IMapper _mapper;

        // Constructor for the CommentController, which takes the ServiceFacade as a dependency.
        public CommentController(ServiceFacade serviceFacade, IMapper mapper)
        {
            // Initialize the serviceFacade field with the provided instance of ServiceFacade.
            _serviceFacade = serviceFacade;
            // Initialize the _mapper field with the provided instance of Imapper.
            _mapper = mapper;
        }


        /// <summary>
        /// Get all Comments
        /// </summary>
        /// <returns>
        /// A list of Comments
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments()
        {
            return Ok(_mapper.Map<List<CommentDTO>>(await _serviceFacade._commentService.GetAllAsync()));
        }


        /// <summary>
        /// Retrieves a specific Comment by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the Comment.</param>
        /// <returns>
        /// A Comment object if found; otherwise, an error message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int id)
        {
            try
            {
                return _mapper.Map<CommentDTO>(await _serviceFacade._commentService.GetByIdAsync(id));
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates the details of a specific Comment based on the provided Comment object and unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the Comment to be updated.</param>
        /// <param name="commentDTO">The Comment object containing the updated details.</param>
        /// <returns>
        /// Returns NoContent if the operation is successful; otherwise, BadRequest or NotFound based on the error.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, CommentDTO commentDTO)
        {
            if (id != commentDTO.Id)
            {
                return BadRequest($"The id {id} given for Comment to be updated does not match the Comment id {commentDTO.Id} given in the body");
            }
            try
            {
                await _serviceFacade._commentService.UpdateAsync(_mapper.Map<Comment>(commentDTO));
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        /// <summary>
        /// Adds a new Comment to the database.
        /// </summary>
        /// <param name="commentDTO">The Comment object to be added.</param>
        /// <returns>
        /// Returns a CreatedAtAction result, directing to the GetComment action to retrieve the newly added comment; otherwise, an error response.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> PostUser(CommentPostDTO commentDTO)
        {
            var newComment = await _serviceFacade._commentService.AddAsync(_mapper.Map<Comment>(commentDTO));

            return CreatedAtAction("GetComment", new { id = newComment.Id }, _mapper.Map<CommentDTO>(newComment));
        }


        /// <summary>
        /// Deletes a specified Comment from the database.
        /// </summary>
        /// <param name="id">The unique ID of the Comment to be deleted.</param>
        /// <returns>
        /// Returns a NoContent response if deletion is successful; otherwise, a NotFound response with an error message
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _serviceFacade._commentService.DeleteAsync(id);
                return NoContent();
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}