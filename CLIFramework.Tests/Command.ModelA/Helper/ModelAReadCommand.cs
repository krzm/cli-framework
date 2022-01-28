using CLIHelper;
using CRUDCommandHelper;
using DataToTable;
using ModelHelper;
using System.Collections.Generic;

namespace CLIFramework.Tests;

public class ModelAReadCommand 
    : ReadCommand<IModelAUnitOfWork, ModelA, ModelA>
{
    public ModelAReadCommand(
        IModelAUnitOfWork unitOfWork
        , IOutput output
        , IDataToText<ModelA> textProvider) 
            : base(
                unitOfWork
                , output
                , textProvider)
    {
    }

    protected override List<ModelA> Get(ModelA model) => 
        UnitOfWork.Get();
}