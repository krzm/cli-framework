using Unity;

namespace CLIFramework;

public class UnityDependencySuite
	: DIHelper.Unity.UnityDependencySuite
{
	public UnityDependencySuite(
		IUnityContainer container)
			: base(container)
	{
	}

	protected override void RegisterUtils() =>
		RegisterSet<AppUtils>();

	protected override void RegisterCommands() =>
		RegisterSet<AppCommands>();

	protected override void RegisterCommandSystem() =>
		RegisterSet<AppCommandSystem<ParamCommandParser>>();

	protected override void RegisterProgram() =>
		RegisterSet<AppProgram<AppProgram>>();
}