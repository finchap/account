using Finchap.Account.Application.Repositories;
using Finchap.Account.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Finchap.Account.Application.Commands.Refresh
{
    public sealed class RefreshCommandHandler : IRequestHandler<RefreshCommand, bool>
    {
        private readonly IFinancialInstitutionAccountRepository _repo;

        public RefreshCommandHandler(IFinancialInstitutionAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(RefreshCommand request,
          CancellationToken cancellationToken)
        {
            var account = await _repo.GetAsync(request.FIAccountId);

            // TODO: Send request

            return true;
        }
    }
}