// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Student? stu = null;
stu?.Age = 3;
 

public class Student
{
    public int? Age { get; set; } = null;

    public string Message
    {
        get;
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    }
}

file static class MyExtensions
{
    extension(Version source)
    {
        public bool IsOk => source.Major > 0;
    }
}