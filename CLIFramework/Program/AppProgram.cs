using CLIHelper;
using DIHelper.Program;

namespace CLIFramework;

public class AppProgram : IAppProgram
{
	private readonly IAppConfig appInfo;
	protected readonly ICommandRunner CommandRunner;
	private readonly ISwitcher switcher;
	private readonly IInput input;
	private readonly IOutput output;

	public AppProgram(
		IAppConfig appInfo
		, ICommandRunner commandRunner
		, ISwitcher switcher
		, IInput input
		, IOutput output)
	{
		this.appInfo = appInfo;
		CommandRunner = commandRunner;
		this.switcher = switcher;
		this.input = input;
		this.output = output;
	}

	public int Main(string[] args)
	{
		switcher.Switch = true;
		while (switcher.Switch)
		{
			output.WriteLine("");
			output.Write($"{appInfo["AppName"]}->");
			CommandRunner.RunCommand(
				input.ReadLine());
		}
		return 0;
	}
}