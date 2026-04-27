using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Requests.IdentityRequests
{
    public record ForgotPasswordDTO([EmailAddress] string Email);
   
}
