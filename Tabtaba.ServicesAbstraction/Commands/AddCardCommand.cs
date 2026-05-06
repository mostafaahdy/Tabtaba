using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.DTOs.Payment;

namespace Tabtaba.ServicesAbstraction.Commands;

public record AddCardCommand(
    string CardHolderName,
    string CardNumber,
    string ExpiryDate,
    string UserId
) : IRequest<AddCardResponse>;