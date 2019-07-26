using Finchap.Account.Application.Commands.Register;
using Finchap.Account.Application.Queries;
using Finchap.Account.Application.Repositories;
using Finchap.Account.Domain;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Finchap.AdminCLI
{
    public class FinancialInstitutionAccountCommand : ICommandLineCommand
    {
        private readonly IMediator _mediator;
        private readonly IFinancialInstitutionAccountQueries _queries;
        private readonly IFinancialInstitutionAccountRepository _repo;

        public FinancialInstitutionAccountCommand(IFinancialInstitutionAccountRepository repo,
          IFinancialInstitutionAccountQueries queries,
          IMediator mediator)
        {
            _repo = repo;
            _queries = queries;
            _mediator = mediator;
        }

        public void Configure(CommandLineApplication app)
        {
            app.Command("fiaccount", fiAccountCmd =>
            {
                fiAccountCmd.HelpOption();
                fiAccountCmd.OnExecute(() =>
                {
                    Console.WriteLine("Specify a subcommand");
                    fiAccountCmd.ShowHelp();
                    return 1;
                });

                List(fiAccountCmd);
                New(fiAccountCmd);
            });
        }

        private void List(CommandLineApplication app)
        {
            app.Command("list", listCmd =>
            {
                listCmd.HelpOption();
                var json = listCmd.Option("--json", "Json output", CommandOptionType.NoValue);
                listCmd.OnExecute(async () =>
                {
                    var values = await _queries.GetAccountsAsync();
                    if (json.HasValue())
                    {
                        foreach (var value in Enumerable.Select(values, v => JsonConvert.SerializeObject(v)))
                        {
                            Console.WriteLine(value);
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0,-40}{1, -30}{2,-20}{3,-120}", "Id", "Friendly Name", "Institution Name", "Description");
                        foreach (var value in values)
                        {
                            Console.WriteLine($"{value.Id,-40}{value.FriendlyName,-30}{value.Institution,-20}{value.Description,-120}");
                        }
                    }
                });
            });
        }

        private void New(CommandLineApplication app)
        {
            app.Command("new", newCmd =>
            {
                newCmd.Description = "To create a new FinancialInstitutionAccount";
                newCmd.HelpOption();

                var userArgument = newCmd.Option("--user-id", "The id of the user who owns the account", CommandOptionType.SingleValue);
                userArgument.IsRequired();
                newCmd.OnExecute(async () =>
                  {
                      var registerCommand = new RegisterCommand(
                          username: Prompt.GetString("Username", ""),
                          password: Prompt.GetString("Password", ""),
                          description: Prompt.GetString("Description?", ""),
                          friendlyName: Prompt.GetString("Friendly name?", ""),
                          institution: Prompt.GetString("Name of the financial institution?", ""),
                          internalUserId: Guid.NewGuid().ToString());

                      var result = await _mediator.Send(registerCommand);
                      var fiAccount = result.FIAccount;
                      Console.WriteLine("{0,-40}{1, -30}{2,-20}{3,-120}", "Id", "Friendly Name", "Institution Name", "Description");
                      Console.WriteLine($"{fiAccount.Id,-40} {fiAccount.FriendlyName,-30} {fiAccount.Institution,-20} {fiAccount.Description,-120}");
                  });
            });
        }
    }
}