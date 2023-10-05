using System;
using Tidsbanken_BackEnd.Data.Enums;

namespace Tidsbanken_BackEnd.Data.Entities
{
	public class Comment
	{
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateCommented { get; set; }
        public string CommentType { get; set; }
        public VacationRequestStatus StatusAtTimeOfComment { get; set; }
        public int? RequestId { get; set; }

        // Navigation properties
        public VacationRequest? VacationRequest { get; set; } // Has
    }
}