using Unity;

namespace CLIFramework.TestApp;

public class UnityDependencySuite
	: DIHelper.Unity.UnityDependencySuite
{
	public UnityDependencySuite(
		IUnityContainer container) 
		: base(container)
	{
	}

	protected override void RegisterAppData() => 
		RegisterSet<AppData>();

	protected override void RegisterConsoleOutput() =>
		RegisterSet<AppOutput>();

	protected override void RegisterUtils() =>
		RegisterSet<AppUtils>();
	
	protected override void RegisterCommands() =>
		RegisterSet<AppCommands>();

	protected override void RegisterCommandSystem() =>
		RegisterSet<AppCommandSystem<ParamCommandParser>>();
	
	protected override void RegisterProgram()
		=> RegisterSet<AppProgram<AppProgram>>();
}