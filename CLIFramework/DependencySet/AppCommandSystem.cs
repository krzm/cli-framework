using CLIHelper;
using DIHelper.Unity;
using Unity;
using Unity.Injection;

namespace CLIFramework;

public class AppCommandSystem<TParser>
    : UnityDependencySet
        where TParser : ICommandParser
{
    public AppCommandSystem(
        IUnityContainer container)
        : base(container)
    {
    }

    public override void Register()
    {
        RegisterCommandParser();
        RegisterCommandRunner();
        SetCommandDependencies();
    }

    protected virtual void RegisterCommandParser()
    {
        Container
            .RegisterType<ICommandParser, TParser>(typeof(TParser).Name);
    }

    private void RegisterCommandRunner()
    {
        Container.RegisterSingleton<ICommandRunner, CommandRunner>(
            new InjectionConstructor(
                Container.Resolve<ICommandParser>(typeof(TParser).Name)
                , Container.Resolve<IOutput>()));
    }

    protected virtual void SetCommandDependencies() { }
}