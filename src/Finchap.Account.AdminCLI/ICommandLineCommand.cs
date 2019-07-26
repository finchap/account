using McMaster.Extensions.CommandLineUtils;

namespace Finchap.AdminCLI
{
    public interface ICommandLineCommand
    {
        void Configure(CommandLineApplication app);
    }
}