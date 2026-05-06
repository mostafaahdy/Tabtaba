using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Shared.DTOs.Payment;

namespace Tabtaba.ServicesAbstraction.Queries;

public record GetTransactionQuery(int PaymentId) : IRequest<TransactionResponse>;