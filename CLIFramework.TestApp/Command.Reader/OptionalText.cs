using CLIHelper;
using CLIReader;

namespace CLIFramework.TestApp;

public class OptionalText : ConsoleCommand
{
	private readonly IOutput output;
	private readonly IReader<string> textReader;

	public OptionalText(
		TextCommand textCommand
		, IOutput output
		, IReader<string> textReader)
		: base(textCommand)
	{
		this.output = output;
		this.textReader = textReader;
	}

	public override void Execute(object? parameter)
	{
		TestTextReader("1. Test happy case. Type input and press enter.");
		output.WriteLine("");
		TestTextReader("2. Test validator. Just press enter.");
		output.WriteLine("");
		TestTextReader("3. Test canceling. Press escape.");
	}

	private void TestTextReader(string message)
	{
		var text = textReader.Read(
			new ReadConfig(
				10
				, message
				, "[Input text][Enter or Esc to skip reader]"));
		if (text == null)
		{
			output.WriteLine("Result: null");
		}
		else
		{
			output.WriteLine("Result:" + text);
		}
	}
}