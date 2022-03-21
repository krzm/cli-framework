using CLIHelper;
using CLIReader;

namespace CLIFramework.TestApp;

public class RequriedDateTime : ConsoleCommand
{
    private const string Format = "dd.MM.yyyy HH:mm";
    private readonly IOutput output;
	private readonly IReader<DateTime> dateReader;

	public RequriedDateTime(
		TextCommand textCommand
		, IOutput output
		, IReader<DateTime> dateReader)
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
			, "[Enter for now][Esc to skip command]"
			, Format
            , DateTime.Now.ToString(Format));
		output.WriteLine("Result:" + dateReader.Read(config).ToString(config.Format));
	}
}