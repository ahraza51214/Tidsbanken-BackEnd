using System;
namespace Tidsbanken_BackEnd.Exceptions
{
    // Custom exception class for handling cases where a character is not found.
    public class UserNotFoundException : Exception
	{
        // Constructor that takes the ID of the non-existent User and constructs an appropriate error message.
        public UserNotFoundException(Guid id) : base($"User with ID: {id}, does not exist")
        {
            // The base constructor of the Exception class is called with a custom error message.
        }
    }
}