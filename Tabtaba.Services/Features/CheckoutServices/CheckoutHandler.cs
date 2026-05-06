using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.DTOs.Checkout;

namespace Tabtaba.Services.Features.CheckoutServices;

public class CheckoutHandler : IRequestHandler<CheckoutCommand, CheckoutResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<CheckoutResponse> Handle(
        CheckoutCommand request,
        CancellationToken cancellationToken)
    {
        //  Plan
        var planRepo = _unitOfWork.GetRepository<Plan>();
        var plans = await planRepo.GetAllAsync();
        var plan = plans.FirstOrDefault(p => p.Id == request.PlanId);

        if (plan is null)
            throw new Exception("Plan not found.");

        //  Subscription
        var subscription = new Subscription
        {
            PatientId = request.PatientId,
            PlanId = request.PlanId,
            PaymentMethod = request.PaymentMethod,
            Status = "Active",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            CreatedAt = DateTime.UtcNow
        };

        var subRepo = _unitOfWork.GetRepository<Subscription>();
        await subRepo.AddAsync(subscription);
        await _unitOfWork.SaveChangesAsync();

        return new CheckoutResponse
        {
            SubscriptionId = subscription.Id,
            PlanName = plan.Name,
            Price = plan.Price,
            PaymentMethod = request.PaymentMethod,
            Status = subscription.Status,
            StartDate = subscription.StartDate,
            EndDate = subscription.EndDate
        };
    }
}