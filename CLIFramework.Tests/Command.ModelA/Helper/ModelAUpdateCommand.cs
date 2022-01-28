using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using ModelHelper;

namespace CLIFramework.Tests;

public class ModelAUpdateCommand 
    : UpdateWizard<IModelAUnitOfWork, ModelA>
{
    public ModelAUpdateCommand(
        IModelAUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , IOutput output) 
            : base(unitOfWork
                , requiredTextReader
                , output)
    {
    }

    protected override ModelA GetById(int id) => 
        UnitOfWork.GetById(id);

    protected override void UpdateEntity(int nr, ModelA model)
    {
        switch (nr)
        {
            case 1:
                model.Name = RequiredTextReader.Read(
                    new ReadConfig(25, nameof(ModelA.Name)));
                break;
            case 2:
                model.Description = RequiredTextReader.Read(
                    new ReadConfig(70, nameof(ModelA.Description)));
                break;
        }
    }
}