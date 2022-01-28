namespace CLIFramework;

public class ParamCommandParser 
	: ICommandParser
{
	private readonly List<IAppCommand> commands;

	public ParamCommandParser(
		List<IAppCommand> commands)
	{
		this.commands = commands.OrderByDescending(c => c.TextCommand.CommandText.Length).ToList(); ;
	}

	public IAppCommand Parse(string input)
	{
		input = input.Trim();
		var command = FindMatchingCommand(input);
		if (string.IsNullOrWhiteSpace(command.TextCommand.TypeName) == true)
		{
			command.TextCommand = new TextCommand(
				command.TextCommand.CommandText
				, GetParams(input.Remove(0, command.TextCommand.CommandText.Length)));
		}
		else
		{
			command.TextCommand = new TextCommand(
				command.TextCommand.CommandText
				, command.TextCommand.TypeName
				, GetParams(input.Remove(0, command.TextCommand.CommandText.Length)));
		}
		return command;
	}

	private IAppCommand FindMatchingCommand(string input)
	{
		foreach (var command in commands)
		{
			if (input.StartsWith(command.TextCommand.CommandText))
				return command;
		}
		throw new ArgumentException($"No such command: {input}");
	}

	private string[] GetParams(string input)
	{
		input = input.Trim();
		if (input.Length == 0)
		{
			return Array.Empty<string>();
		}
		return input.Split(' ');
	}
}