using System;
namespace Tidsbanken_BackEnd.Exceptions
{
    // Custom exception class for handling cases where a VacationRequest is not found.
    public class VacationRequestNotFoundException : Exception
	{
        // Constructor that takes the ID of the non-existent VacationRequest and constructs an appropriate error message.
        public VacationRequestNotFoundException(int id) : base($"VacationRequest with ID: {id}, does not exist")
        {
            // The base constructor of the Exception class is called with a custom error message.
        }
    }
}