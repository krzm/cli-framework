using CLIHelper;
using DataToTable;
using ModelHelper;

namespace CLIFramework.TestApp;

public class TextTable : ConsoleCommand
{
	private readonly IOutput output;
	private readonly IDataToText<ModelA> textProvider;

	public TextTable(
		TextCommand textCommand
		, IOutput output
		, IDataToText<ModelA> textProvider)
		: base(textCommand)
	{
		this.output = output;
		this.textProvider = textProvider;
	}

	public override void Execute(object? parameter)
	{
		TestTextProvider("Case1: data shorter than headers", GetCase1());
		output.WriteLine("");
		TestTextProvider("Case2: data same length as headers", GetCase2());
		output.WriteLine("");
		TestTextProvider("Case3: data longer than headers", GetCase3());
	}

	private void TestTextProvider(string message, List<ModelA> data)
    {
        output.WriteLine(message);
        var text = textProvider.GetText(data);
        output.Write(text);
    }

    private static List<ModelA> GetCase1()
    {
        return new List<ModelA> {
			new ModelA
			{
				Id = 1
				, Name = ""
				, Description = ""
			}};
    }

	private static List<ModelA> GetCase2()
    {
        return new List<ModelA> {
			new ModelA
			{
				Id = 11
				, Name = "Name"
				, Description = "Description"
			}};
    }

	private static List<ModelA> GetCase3()
    {
        return new List<ModelA> {
			new ModelA
			{
				Id = 111
				, Name = "NameTest"
				, Description = "DescriptionTest"
			}};
    }
}