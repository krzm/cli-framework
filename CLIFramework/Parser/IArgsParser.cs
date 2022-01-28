namespace CLIFramework;

public interface IArgsParser<out TType>
{
	TType Parse(params string[] args);
}