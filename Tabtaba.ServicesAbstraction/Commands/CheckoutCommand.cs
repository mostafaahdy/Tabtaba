using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.DTOs.Checkout;

namespace Tabtaba.ServicesAbstraction.Commands;

public record CheckoutCommand(
    int PatientId,
    int PlanId,
    string PaymentMethod
) : IRequest<CheckoutResponse>;