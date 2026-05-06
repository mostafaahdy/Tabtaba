using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.DTOs.Payment;

namespace Tabtaba.Services.Features.PaymentServices;

public class AddCardHandler : IRequestHandler<AddCardCommand, AddCardResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddCardHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<AddCardResponse> Handle(
        AddCardCommand request,
        CancellationToken cancellationToken)
    {
        // بنحفظ آخر 4 أرقام بس
        var maskedNumber = "**** **** **** " + request.CardNumber[^4..];

        var card = new PaymentCard
        {
            CardHolderName = request.CardHolderName,
            MaskedCardNumber = maskedNumber,
            ExpiryDate = request.ExpiryDate,
            UserId = request.UserId,
            IsDefault = false,
            CreatedAt = DateTime.UtcNow
        };

        var repo = _unitOfWork.GetRepository<PaymentCard>();
        await repo.AddAsync(card);
        await _unitOfWork.SaveChangesAsync();

        return new AddCardResponse
        {
            Id = card.Id,
            CardHolderName = card.CardHolderName,
            MaskedCardNumber = card.MaskedCardNumber,
            ExpiryDate = card.ExpiryDate,
            IsDefault = card.IsDefault
        };
    }
}