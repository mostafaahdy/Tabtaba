using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Session;

namespace Tabtaba.ServicesAbstraction.Commands.UserCommands
{
    public record SubmitReviewCommand(
     int AppointmentId,
     int RatingScore, 
     string ReviewText, 
     string? PatientComment
 ) :IRequest<ReviewResponse>;
}
