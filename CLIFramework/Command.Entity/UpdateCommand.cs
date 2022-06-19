using CLIWizardHelper;

namespace CLIFramework;

public abstract class UpdateCommand<TEntity>
    : DataCommand<TEntity>
        , IDataCommand
{
    protected readonly IUpdateWizard<TEntity> updateWizard;
    private ICommandRunner? commandRunner;

    public UpdateCommand(
        TextCommand textCommand
        , IUpdateWizard<TEntity> updateWizard)
        : base(textCommand)
    {
        ArgumentNullException.ThrowIfNull(updateWizard);

        this.updateWizard = updateWizard;
    }

    public void SetCommandRunner(ICommandRunner commandRunner)
    {
        ArgumentNullException.ThrowIfNull(commandRunner);
        this.commandRunner = commandRunner;
    }

    public override void Execute(object? parameter)
    {
        updateWizard.Update();
        ArgumentNullException.ThrowIfNull(commandRunner);
        ArgumentNullException.ThrowIfNull(TextCommand.TypeName);
        commandRunner.RunCommand(TextCommand.TypeName);
    }
}