using Unity;

namespace CLIFramework.TestApp;

public class AppData 
    : CLIFramework.AppData
{
    public AppData(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void SetAppConfigData()
    {
        Config["AppName"] = "Console.TestApp";
        Config["CommandParser"] = nameof(ParamCommandParser);
    }
}