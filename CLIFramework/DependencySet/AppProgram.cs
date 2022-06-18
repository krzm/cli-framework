using CLIHelper;
using DIHelper;
using DIHelper.Unity;
using Unity;
using Unity.Injection;

namespace CLIFramework;

public class AppProgram<TProgram>
    : UnityDependencySet
        where TProgram : IMainProgram
{
    public AppProgram(
        IUnityContainer container)
        : base(container)
    {
    }

    public override void Register()
    {
        Container.RegisterType<IMainProgram, TProgram>(
            GetInjectionConstructor());
    }

    protected virtual InjectionConstructor GetInjectionConstructor()
    {
        return new InjectionConstructor(
            new object[]
            {
                Container.Resolve<IAppConfig>()
                , Container.Resolve<ICommandRunner>()
                , Container.Resolve<ISwitcher>("LoopSwitch")
                , Container.Resolve<IInput>()
                , Container.Resolve<IOutput>()
            });
    }
}