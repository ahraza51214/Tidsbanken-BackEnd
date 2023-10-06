using System;
using Tidsbanken_BackEnd.Services.CommentService;
using Tidsbanken_BackEnd.Services.IneligiblePeriodService;
using Tidsbanken_BackEnd.Services.UserService;
using Tidsbanken_BackEnd.Services.VacationRequestService;

namespace Tidsbanken_BackEnd.Services
{
	public class ServiceFacade
	{
		public readonly ICommentService _commentService;
		public readonly IIneligiblePeriodService _ineligiblePeriodService;
		public readonly IUserService _userService;
		public readonly IVacationRequestService _vacationRequestService;

        public ServiceFacade(ICommentService commentService, IIneligiblePeriodService ineligiblePeriodService, IUserService userService, IVacationRequestService vacationRequestService)
        {
            _commentService = commentService;
            _ineligiblePeriodService = ineligiblePeriodService;
            _userService = userService;
            _vacationRequestService = vacationRequestService;
        }
    }
}