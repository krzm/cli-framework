using DataToTable;
using ModelHelper;
using Unity;

namespace CLIFramework.TestApp;

public class DataToTableSet 
    : DataToTable.Unity.DataToTableSet
{
    public DataToTableSet(
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