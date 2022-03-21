using CLIHelper;
using CRUDCommandHelper;
using DataToTable;
using ModelHelper;
using Serilog;
using System.Collections.Generic;

namespace CLIFramework.Tests;

public class ModelAReadCommand 
    : ReadCommand<IModelAUnitOfWork, ModelA, ModelA>
{
    public ModelAReadCommand(
        IModelAUnitOfWork unitOfWork
        , IOutput output
        , ILogger log
        , IDataToText<ModelA> textProvider) 
            : base(
                unitOfWork
                , output
                , log
                , textProvider)
    {
    }

    protected override List<ModelA> Get(ModelA model) => 
        UnitOfWork.Get();
}