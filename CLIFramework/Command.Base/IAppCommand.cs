using System.Windows.Input;

namespace CLIFramework;

public interface IAppCommand
	: ICommand
{
	TextCommand TextCommand { get; set; }
}