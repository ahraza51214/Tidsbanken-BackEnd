using System;
namespace Tidsbanken_BackEnd.Exceptions
{
    // Custom exception class for handling cases where a IneligiblePeriod is not found.
    public class IneligiblePeriodNotFoundException : Exception
	{
        // Constructor that takes the ID of the non-existent IneligiblePeriod and constructs an appropriate error message.
        public IneligiblePeriodNotFoundException(int id) : base($"Ineligible Period with ID: {id}, does not exist")
        {
            // The base constructor of the Exception class is called with a custom error message.
        }
    }
}