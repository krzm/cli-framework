namespace CLIFramework;

public abstract class ConsoleCommand : IAppCommand
{
	public event EventHandler? CanExecuteChanged;

	public TextCommand TextCommand { get; set; }

	//todo: use separete setter for this so dependencies are not needed to be always specified
	public ConsoleCommand(TextCommand textCommand)
	{
		ArgumentNullException.ThrowIfNull(textCommand);

		TextCommand = textCommand;
	}

	public virtual bool CanExecute(object? parameter)
	{
		return true;
	}

	public abstract void Execute(object? parameter);

	protected virtual void OnCanExecuteChanged()
	{
		CanExecuteChanged?.Invoke(this, new EventArgs());
	}
}