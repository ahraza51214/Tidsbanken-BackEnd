using System;
namespace Tidsbanken_BackEnd.Exceptions
{
    // Custom exception class for handling cases where a Comment is not found.
    public class CommentNotFoundException : Exception
	{
        // Constructor that takes the ID of the non-existent Comment and constructs an appropriate error message.
        public CommentNotFoundException(int id) : base($"Comment with ID: {id}, does not exist")
        {
            // The base constructor of the Exception class is called with a custom error message.
        }
    }
}