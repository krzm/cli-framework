using CRUDCommandHelper;

namespace CLIFramework;

public abstract class ReadCommand<TEntity>
	: DataCommand<TEntity>
		where TEntity : class, new()
{
	private readonly IReadCommand<TEntity> entityReadCommand;

	public ReadCommand(
		TextCommand textCommand
		, IReadCommand<TEntity> entityReadCommand)
		: base(textCommand)
	{
		this.entityReadCommand = entityReadCommand;

		ArgumentNullException.ThrowIfNull(this.entityReadCommand);
	}

	public override void Execute(object? parameter)
	{
		entityReadCommand.Read(new TEntity());
	}
}