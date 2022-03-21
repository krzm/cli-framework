namespace CLIFramework;

public class TextCommand
{
	public string CommandText { get; init; }

	public string? TypeName { get; init; }

	public string[]? Params { get; init; }

	public TextCommand(
		string commandText)
	{
		if (string.IsNullOrWhiteSpace(commandText) == true)
			throw (new ArgumentNullException(nameof(commandText), $"Value cant be: '{commandText}'"));

		CommandText = commandText;
	}

	public TextCommand(
		string commandText
		, string[] paramArray) : this(commandText)
	{
		ArgumentNullException.ThrowIfNull(paramArray);

		Params = paramArray;
	}

	public TextCommand(
		string commandText
		, string typeName) : this(commandText)
	{
		if (string.IsNullOrWhiteSpace(typeName) == true)
			throw (new ArgumentNullException(nameof(typeName), $"Value cant be: '{typeName}'"));

		TypeName = typeName;
	}

	public TextCommand(
		string commandText
		, string typeName
		, string[] paramArray) : this(commandText, typeName)
	{
		ArgumentNullException.ThrowIfNull(paramArray);

		Params = paramArray;
	}

	public override bool Equals(object? obj)
	{
		if ((obj == null) || !GetType().Equals(obj.GetType()))
		{
			return false;
		}
		else
		{
			TextCommand c = (TextCommand)obj;
			return CommandText == c.CommandText;
		}
	}

	public override int GetHashCode() =>
		CommandText.GetHashCode();

	public override string ToString() =>
		CommandText;
}