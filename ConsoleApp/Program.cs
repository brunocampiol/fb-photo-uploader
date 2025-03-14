using ConsoleApp;

string email = "bruno.campiol@gmail.com";
string password = "3ULf^ch73&e&77H";

try
{
    Console.WriteLine("Application started");

    var appService = new AppService();
    appService.Run_V2();

    //GoogleSteps.Run(options);
    //BrunoCampiolSteps.Run(chromeOptions);
}
catch (Exception e)
{
    ColorConsole.WriteError($"Error: {e.Message}");
}