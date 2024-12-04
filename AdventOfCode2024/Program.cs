// See https://aka.ms/new-console-template for more information
using AOC;
using System.Reflection;

BaseDay.Year = "2024";
BaseDay.InputDir = Path.Combine(Directory.GetCurrentDirectory(), "Input", BaseDay.Year + Path.DirectorySeparatorChar.ToString());

Runner days = new Runner(Assembly.GetExecutingAssembly());

//tests
{
    days[0].AddTestInput(new string[] {
        "3   4",
        "4   3",
        "2   5",
        "1   3",
        "3   9",
        "3   3"
        }, "11", "31");

    days[1].AddTestInput(new string[] {
        "7 6 4 2 1",
        "1 2 7 8 9",
        "9 7 6 2 1",
        "1 3 2 4 5",
        "8 6 4 4 1",
        "1 3 6 7 9"
        }, "2", "4");

    days[2].AddTestInput(new string[] {
        "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
        }, "161", "");

    days[2].AddTestInput(new string[] {
        "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
        }, "", "48");

days[3].AddTestInput(new string[] {
        "..X...",
        ".SAMX.",
        ".A..A.",
        "XMAS.S",
        ".X...."
        }, "4", "");

days[3].AddTestInput(new string[] {
        "MMMSXXMASM",
        "MSAMXMSMSA",
        "AMXSXMAAMM",
        "MSAMASMSMX",
        "XMASAMXAMM",
        "XXAMMXXAMA",
        "SMSMSASXSS",
        "SAXAMASAAA",
        "MAMMMXMMMM",
        "MXMXAXMASX"
        }, "18", "");
        
        days[3].AddTestInput(new string[] {
        ".M.S......",
        "..A..MSMS.",
        ".M.S.MAA..",
        "..A.ASMSM.",
        ".M.S.M....",
        "..........",
        "S.S.S.S.S.",
        ".A.A.A.A..",
        "M.M.M.M.M.",
        ".........."
        }, "", "9");
    // days[0].AddTestInputFromFile("everybody_codes_e2024_q01_p1.txt", "1437", "");
    // days[0].AddTestInputFromFile("everybody_codes_e2024_q01_p2.txt", "", "5669");
    // days[0].AddTestInputFromFile("everybody_codes_e2024_q01_p3.txt", "", "5669");
}

////run
Console.WriteLine("Running Newest");
days.RunLatest();

//Console.ReadKey();

////run all
Console.WriteLine("Running All");
days.RunAll();

//windows closes the console window, linux does not
//keep open on windows
Helpers.ConsoleReadKeyWindows();