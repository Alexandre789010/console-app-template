namespace ConsoleApplicationTemplate;

public static class Program
{
    private static readonly string[] MmOptions = {Strings.MMOption1, Strings.MMOption2, Strings.MMOption3,};
    private static readonly string[] Sm1Options = {Strings.SM1Option1, Strings.SM1Option2};
    
    public static void Main(string[] args)
    {
        var mainMenu = new Menu.Menu(Strings.MMPrompt, MmOptions);
        var selectedMm = mainMenu.Run();

        Console.WriteLine($@"You have selected: {MmOptions[selectedMm]}");

        var subMenu = new Menu.Menu(Strings.SM1Prompt, Sm1Options);
        var selectedSm = subMenu.Run();
        
        Console.WriteLine($@"You have selected: {Sm1Options[selectedSm]}");
    }
}