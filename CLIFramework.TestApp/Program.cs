using CLIFramework.TestApp;
using DIHelper;
using Unity;

IBootstraper booter = new Bootstraper(
    new UnityDependencySuite(
        new UnityContainer().AddExtension(
            new Diagnostic())));
booter.CreateApp();
booter.RunApp(args);