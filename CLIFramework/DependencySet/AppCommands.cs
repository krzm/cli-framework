using CLIHelper;
using DIHelper.Unity;
using Unity;
using Unity.Injection;

namespace CLIFramework;

public class AppCommands
    : UnityDependencySet
{
    public AppCommands(
        IUnityContainer container)
        : base(container)
    {
    }

    public override void Register()
    {
        RegisterCommands();
        SetupHelpCommand();
    }

    protected virtual void RegisterCommands()
    {
        RegisterCommand<HelpCommand>(
            "help"
            , Container.Resolve<IOutput>());

        RegisterCommand<ClearCommand>(
            "clear"
            , Container.Resolve<IOutput>());

        RegisterCommand<ExitCommand>(
            "exit"
            , Container.Resolve<ISwitcher>("LoopSwitch"));
    }

    protected void RegisterCommand<TCommand>(
        string name
        , params object[] dependencies)
            where TCommand : IAppCommand
    {
        Container.RegisterSingleton<IAppCommand, TCommand>(
            name
            , GetInjectionConstructor(name, dependencies));
    }

    private InjectionConstructor GetInjectionConstructor(
        string name
        , object[] dependencies)
    {
        var ctorArgs = new List<object>
        {
            new TextCommand(name)
        };

        ctorArgs.AddRange(dependencies);

        return new InjectionConstructor(ctorArgs.ToArray());
    }

    protected void RegisterCommand<TCommand, TEntity>(
        string name
        , params object[] dependencies)
            where TCommand : IAppCommand
    {
        Container.RegisterSingleton<IAppCommand, TCommand>(
            name
            , GetInjectionConstructor(name, typeof(TEntity).Name, dependencies));
    }

    private InjectionConstructor GetInjectionConstructor(
        string name
        , string typeName
        , object[] dependencies)
    {
        var ctorArgs = new List<object>
        {
            new TextCommand(name, typeName)
        };

        ctorArgs.AddRange(dependencies);

        return new InjectionConstructor(ctorArgs.ToArray());
    }

    private void SetupHelpCommand()
    {
        var helpCommand = Container.Resolve<IAppCommand>("help") as HelpCommand;
        ArgumentNullException.ThrowIfNull(helpCommand);
        helpCommand.SetCommands(Container.Resolve<List<IAppCommand>>().Select(c => c.TextCommand).ToList());
    }
}