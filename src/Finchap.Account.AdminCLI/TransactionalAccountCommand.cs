using Finchap.Account.Application.Queries;
using Finchap.Account.Application.Repositories;
using Finchap.Account.Domain;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Finchap.AdminCLI
{
    public class TransactionalAccountCommand : ICommandLineCommand
    {
        private readonly ITransactionalAccountQueries _queries;
        private readonly IFinancialInstitutionAccountRepository _repo;

        public TransactionalAccountCommand(IFinancialInstitutionAccountRepository repo, ITransactionalAccountQueries queries)
        {
            _repo = repo;
            _queries = queries;
        }

        public void Configure(CommandLineApplication app)
        {
            app.Command("traccount", trAccountCmd =>
            {
                trAccountCmd.HelpOption();
                trAccountCmd.OnExecute(() =>
                {
                    Console.WriteLine("Specify a subcommand");
                    trAccountCmd.ShowHelp();
                    return 1;
                });

                List(trAccountCmd);
                New(trAccountCmd);
            });
        }

        private void List(CommandLineApplication app)
        {
            app.Command("list", listCmd =>
            {
                listCmd.HelpOption();

                var fiAccountId = listCmd.Option("--fi-account-id", "Financial Account Id", CommandOptionType.SingleValue);
                fiAccountId.IsRequired();
                listCmd.OnExecute(() =>
                {
                    //var values = this._repo.GetForFinancialInstitutionAccountsAsync(fiAccountId.Value()).Result;
                    var values = _queries.GetAccountsAsync().Result;
                    if (fiAccountId.HasValue())
                    {
                        foreach (var value in Enumerable.Select(values, v => (string)JsonConvert.SerializeObject((object)v)))
                        {
                            Console.WriteLine(value);
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0,-40}, {1,-20}, {2,-20}, {3,-120}", "Id", "Account Type", "UserId", "Description");
                        foreach (var value in values)
                        {
                            Console.WriteLine($"{value.Id,-40}, {value.AccountType,-20}, {value.Id,-40}, {value.Description,-120}");
                        }
                        Console.WriteLine("dummy = value");
                    }
                });
            });
        }

        private void New(CommandLineApplication app)
        {
            app.Command("new", newCmd =>
            {
                newCmd.Description = "To create a new TransactionalAccount";
                newCmd.HelpOption();

                var userArgument = newCmd.Option("--user-id", "The id of the user who owns the account", CommandOptionType.SingleValue);
                var fiAccountIdArgument = newCmd.Option("--fi-account-id", "The id of the financial instution account id", CommandOptionType.SingleValue);
                userArgument.IsRequired();
                fiAccountIdArgument.IsRequired();
                newCmd.OnExecute(async () =>
                {
                    var trAccount = new TRAccount(userArgument.Value(), "")
                    {
                        Description = Prompt.GetString("Please enter the description", ""),
                        AccountNumber = Prompt.GetString("Please enter the account number", ""),
                        AccountType = Enum.Parse<Account.Domain.AccountType>(Prompt.GetString("Please enter the account type")),
                        FinancialInstitutionAccountId = fiAccountIdArgument.Value()
                    };

                    var fiAccount = _repo.GetAsync(fiAccountIdArgument.Value()).Result;
                    fiAccount.AddTransactionalAccount(trAccount);
                    _repo.Add(fiAccount);
                    await _repo.UnitOfWork.SaveEntitiesAsync();
                });
            });
        }
    }
}