using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.ServicesAbstraction.Queries;
using Tabtaba.Shared.DTOs.Checkout;

namespace Tabtaba.Services.Features.CheckoutServices;

public class GetPlansHandler : IRequestHandler<GetPlansQuery, List<PlanResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPlansHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<List<PlanResponse>> Handle(
        GetPlansQuery request,
        CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Plan>();
        var plans = await repo.GetAllAsync();

        return plans
            .Where(p => p.IsActive)
            .Select(p => new PlanResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                BillingCycle = p.BillingCycle
            })
            .ToList();
    }
}