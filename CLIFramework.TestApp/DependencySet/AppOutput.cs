using DataToTable;
using ModelHelper;
using Unity;

namespace CLIFramework.TestApp;

public class AppOutput 
    : CLIHelper.Unity.AppOutput
{
    public AppOutput(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void RegisterColumnCalculators()
    {
        Container
            .RegisterType<IColumnCalculator<ModelA>
                , ColumnCalculator<ModelA>>();
    }

    protected override void RegisterTableProviders()
    {
        Container
            .RegisterType<IDataToText<ModelA>
                , ModelATable<ModelA>>();
    }
}