namespace CLIFramework;

public interface IDataCommand
{
    void Execute(object? parameter);
    void SetCommandRunner(ICommandRunner commandRunner);
}