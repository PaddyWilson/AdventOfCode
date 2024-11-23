// See https://aka.ms/new-console-template for more information
using AOC;
using System.Reflection;

BaseDay.Year = "2024";
BaseDay.InputDir = Path.Combine(Directory.GetCurrentDirectory(), "Input", BaseDay.Year + Path.DirectorySeparatorChar.ToString());

Runner days = new Runner(Assembly.GetExecutingAssembly());

//tests
{
    // days[0].AddTestInput(new string[] {
    //     "ABBAC" }, "5", "");
    // days[0].AddTestInput(new string[] {
    //     "AxBCDDCAxD" }, "", "28");

    // days[0].AddTestInputFromFile("everybody_codes_e2024_q01_p1.txt", "1437", "");
    // days[0].AddTestInputFromFile("everybody_codes_e2024_q01_p2.txt", "", "5669");
    // days[0].AddTestInputFromFile("everybody_codes_e2024_q01_p3.txt", "", "5669");
}

////run
Console.WriteLine("Running Newest");
days.RunLatest();

////run all
Console.WriteLine("Running All");
days.RunAll();

//windows closes the console window, linux does not
//keep open on windows
Helpers.ConsoleReadKeyWindows();