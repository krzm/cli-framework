using CLIReader;
using CLIWizardHelper;
using ModelHelper;
using Serilog;

namespace CLIFramework.Tests;

public class ModelAInsertCommand 
    : InsertWizard<IModelAUnitOfWork, ModelA>
{
    public ModelAInsertCommand(
        IModelAUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , ILogger log) 
            : base(unitOfWork
                , requiredTextReader
                , log)
    {
    }

    protected override ModelA GetEntity()
    {
        return new ModelA()
        {
            Name = RequiredTextReader.Read(
                new ReadConfig(25, nameof(ModelA.Name)))
            ,
            Description = RequiredTextReader.Read(
                new ReadConfig(70, nameof(ModelA.Description)))
        };
    }

    protected override void InsertEntity(ModelA entity)
    {
        UnitOfWork.Insert(entity);
    }
}