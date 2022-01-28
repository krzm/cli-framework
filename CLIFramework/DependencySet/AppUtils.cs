using DIHelper.Unity;
using Unity;

namespace CLIFramework;

public class AppUtils
    : UnityDependencySet
{
    public AppUtils(
        IUnityContainer container)
        : base(container)
    {
    }

    public override void Register()
    {
        Container.RegisterSingleton<ISwitcher, Switcher>("LoopSwitch");
    }
}