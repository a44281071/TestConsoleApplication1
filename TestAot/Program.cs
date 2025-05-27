// See https://aka.ms/new-console-template for more information
// dotnet publish -r win-x64 -c Release

Console.WriteLine("Press any key!!!");

var kk = Console.ReadKey();
Console.WriteLine("You pressed key: " + kk.Key);

Console.WriteLine("Press any to exit...");
Console.ReadKey();