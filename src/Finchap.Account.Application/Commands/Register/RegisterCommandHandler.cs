using Finchap.Account.Application.Repositories;
using Finchap.Account.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Finchap.Account.Application.Commands.Register
{
    public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        private readonly IFinancialInstitutionAccountRepository _repo;

        public RegisterCommandHandler(IFinancialInstitutionAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<RegisterResult> Handle(RegisterCommand request,
          CancellationToken cancellationToken)
        {
            var account = new FIAccount(request.InternalUserId)
            {
                Description = request.Description,
                Institution = request.Institution,
                FriendlyName = request.FriendlyName,
                State = FIStateEnum.New
            };

            account = _repo.Add(account);
            await _repo.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new RegisterResult(new Results.FinancialInstitutionAccountResult
            {
                Description = account.Description,
                FriendlyName = account.FriendlyName,
                Id = account.Id,
                Institution = account.Institution,
                UserId = account.UserId
            });
        }
    }
}