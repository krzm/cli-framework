using CLIHelper;
using CLIReader;

namespace CLIFramework.TestApp;

public class OptionalDateTime : ConsoleCommand
{
	private readonly IOutput output;
	private readonly IReader<DateTime?> dateReader;

	public OptionalDateTime(
		TextCommand textCommand
		, IOutput output
		, IReader<DateTime?> dateReader)
		: base(textCommand)
	{
		this.output = output;
		this.dateReader = dateReader;
	}

	public override void Execute(object? parameter)
	{
		TestDateTimeReader("1. Test happy case. Type input and press enter.");
		output.WriteLine("");
		TestDateTimeReader("2. Test validator. Just press enter.");
		output.WriteLine("");
		TestDateTimeReader("3. Test canceling. Press escape.");
	}

	private void TestDateTimeReader(string message)
	{
		var config = new ReadConfig(
			16
			, message
			, "[Enter for NOW][Esc to skip Reader]"
			, "dd.MM.yyyy HH:mm");
			
		var date = dateReader.Read(config);
			
		switch (date.HasValue)
		{
			case false:
				output.WriteLine("Result:" + date);
				break;
			default:
				output.WriteLine("Result:" + date.Value.ToString(config.Format));
				break;
		}
	}
}