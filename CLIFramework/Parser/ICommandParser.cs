namespace CLIFramework;

public interface ICommandParser
{
	IAppCommand Parse(string input);
}