namespace CLIFramework;

public abstract class DataCommand<TEntity>
    : ConsoleCommand
{
    protected DataCommand(TextCommand textCommand)
            : base(textCommand)
    {
        if (string.IsNullOrWhiteSpace(TextCommand.TypeName) == true)
            throw new ArgumentException(
                $"Value not set."
                , nameof(TextCommand.TypeName));
    }
}