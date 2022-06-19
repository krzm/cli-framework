using CLIWizardHelper;

namespace CLIFramework;

public abstract class InsertCommand<TEntity>
    : DataCommand<TEntity>
        , IDataCommand
            where TEntity : new()
{
    protected readonly IInsertWizard<TEntity> modelAInsertWizard;
    private ICommandRunner? commandRunner;

    public InsertCommand(
        TextCommand textCommand
        , IInsertWizard<TEntity> modelAInsertWizard)
        : base(textCommand)
    {
        ArgumentNullException.ThrowIfNull(modelAInsertWizard);

        this.modelAInsertWizard = modelAInsertWizard;
    }

    public void SetCommandRunner(ICommandRunner commandRunner)
    {
        ArgumentNullException.ThrowIfNull(commandRunner);
        this.commandRunner = commandRunner;
    }

    public override void Execute(object? parameter)
    {
        modelAInsertWizard.Insert();
        ArgumentNullException.ThrowIfNull(commandRunner);
        ArgumentNullException.ThrowIfNull(TextCommand.TypeName);
        commandRunner.RunCommand(TextCommand.TypeName);
    }
}