using EFCore.Helper;
using ModelHelper;
using System.Collections.Generic;

namespace CLIFramework.Tests;

public interface IModelAUnitOfWork 
    : IUnitOfWork
{
    void Insert(ModelA model);

    ModelA GetById(int id);

    List<ModelA> Get();
}