using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Tabtaba.ServicesAbstraction.Commands;

public record WithdrawCommand(
    int DoctorId,
    decimal Amount,
    string PaymentMethod
) : IRequest<bool>;