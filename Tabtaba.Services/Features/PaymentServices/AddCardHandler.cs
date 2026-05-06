using MediatR;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.Services.Services;
using Tabtaba.ServicesAbstraction.Commands;
using Tabtaba.Shared.DTOs.Payment;

namespace Tabtaba.Services.Features.PaymentServices;

public class AddCardHandler : IRequestHandler<AddCardCommand, AddCardResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly EncryptionService _encryption;

    public AddCardHandler(
        IUnitOfWork unitOfWork,
        EncryptionService encryption)
    {
        _unitOfWork = unitOfWork;
        _encryption = encryption;
    }

    public async Task<AddCardResponse> Handle(
        AddCardCommand request,
        CancellationToken cancellationToken)
    {
        var maskedNumber = "**** **** **** " + request.CardNumber[^4..];

        var card = new PaymentCard
        {
            CardHolderName = _encryption.Encrypt(request.CardHolderName),
            MaskedCardNumber = maskedNumber,
            ExpiryDate = _encryption.Encrypt(request.ExpiryDate),
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
            CardHolderName = request.CardHolderName,
            MaskedCardNumber = card.MaskedCardNumber,
            ExpiryDate = request.ExpiryDate,
            IsDefault = card.IsDefault
        };
    }
}