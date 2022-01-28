using DIHelper.Unity;
using Unity;

namespace CLIFramework;

public abstract class AppData
    : UnityDependencySet
{
    protected readonly IAppConfig Config = new AppConfig();

    public AppData(
        IUnityContainer container)
        : base(container)
    {
    }

    public override void Register()
    {
        SetAppConfigData();
        RegisterAppConfig();
    }

    private void RegisterAppConfig() =>
        Container.RegisterInstance(Config);

    protected abstract void SetAppConfigData();
}