using CLIHelper;
using CLIReader;
using DataToTable;
using ModelHelper;
using Unity;

namespace CLIFramework.TestApp;

public class AppCommands 
    : CLIFramework.AppCommands
{
    public AppCommands(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void RegisterCommands()
    {
        base.RegisterCommands();

        RegisterReader<RequriedText, string>(
            "TextReader".ToLowerInvariant()
            , nameof(RequiredTextReader));

        RegisterReader<OptionalText, string>(
            "TextOptional".ToLowerInvariant()
            , nameof(OptionalTextReader));
        
        RegisterReader<RequriedDateTime, DateTime>(
            "DateTimeReader".ToLowerInvariant()
            , nameof(RequiredDateTimeReader));
        
        RegisterReader<OptionalDateTime, DateTime?>(
            "DateTimeOptional".ToLowerInvariant()
            , nameof(OptionalDateTimeReader));
        
        RegisterCommand<TextTable>(
            "table"
            , Container.Resolve<IOutput>()
            , Container.Resolve<IDataToText<ModelA>>());

        RegisterCommand<NewCommand>(
            "new command"
            , Container.Resolve<IOutput>());

        RegisterCommand<HelpCommand<ModelA>, ModelA>(
            "help modela"
            , Container.Resolve<IOutput>()
            , new string[]
            {
                nameof(IModelA.Name)
                , nameof(IModelA.Description)
            });
    }

    private void RegisterReader<TCommand, TModel>(
        string name
        , string readerName)
        where TCommand : IAppCommand
    {
        RegisterCommand<TCommand>(
            name.ToLowerInvariant()
            , Container.Resolve<IOutput>()
            , Container.Resolve<IReader<TModel>>(readerName));
    }
}