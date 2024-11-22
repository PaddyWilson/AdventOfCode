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

    // days[1].AddTestInput(new string[] {
    //     "WORDS:THE,OWE,MES,ROD,HER",
    //     "",
    //     "AWAKEN THE POWER ADORNED WITH THE FLAMES BRIGHT IRE"}, "4", "");

    // days[1].AddTestInput(new string[] {
    //     "WORDS:THE,OWE,MES,ROD,HER",
    //     "",
    //     "AWAKEN THE POWE ADORNED WITH THE FLAMES BRIGHT IRE",
    //     "THE FLAME SHIELDED THE HEART OF THE KINGS",
    //     "POWE PO WER P OWE R",
    //     "THERE IS THE END",
    //     ""}, "", "37");

    // days[1].AddTestInputFromFile("everybody_codes_e2024_q02_p1.txt", "34", "");
    // days[1].AddTestInputFromFile("everybody_codes_e2024_q02_p2.txt", "", "5165");
    // days[1].AddTestInputFromFile("everybody_codes_e2024_q02_p3.txt", "", "1");

    //days[2].AddTestInput(new string[] { "A Y", "B X", "C Z" }, "15", "12");
    //days[3].AddTestInputFromFile("Day3test.txt", "157", "70");
    //days[4].AddTestInputFromFile("Day4test.txt", "2", "4");

    ////test very large community made input files for day 5
    //days[5].AddTestInputFromFile("aoc_2022_day05_message.txt", "CHRISTMAS", "GREETINGS");
    //days[5].AddTestInputFromFile("aoc_2022_day05_large_input.txt", "GATHERING", "DEVSCHUUR");
    ////days[5].AddTestInputFromFile("aoc_2022_day05_large_input-2.txt", "KERSTBOOM", "HENKLEEFT");

    //days[7].AddTestInputFromFile("day7test.txt", "95437", "24933642");
    ////large test file , errors with a stack overflow 
    ////days[7].AddTestInputFromFile("aoc_2022_day07_deep.txt", "0", "0");

    //days[8].AddTestInputFromFile("day8test.txt", "21", "8");
    //days[10].AddTestInputFromFile("day10test.txt", "13140", "");
    //days[11].AddTestInputFromFile("day11test.txt", "10605", "2713310158");
    //days[12].AddTestInputFromFile("day12test.txt", "31", "29");
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