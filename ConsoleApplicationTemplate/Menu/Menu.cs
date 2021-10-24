namespace ConsoleApplicationTemplate.Menu;

public class Menu
{
    private readonly string[] _options;
    private readonly string _prompt;
    private int _leftConsole;
    private int _selectedIndex;
    private int _topConsole;

    /// <summary>
    ///     Initialize the menu.
    /// </summary>
    /// <param name="prompt">Message to be displayed before the options.</param>
    /// <param name="options">Options that the user can choose from.</param>
    public Menu(string prompt, string[] options)
    {
        _prompt = prompt;
        _options = options;
        _selectedIndex = 0;
    }

    /// <summary>
    ///     Wait for the user to make a selection.
    ///     Take user input to change the selected option.
    /// </summary>
    /// <returns>The selected option once the user press the "Enter" key.</returns>
    public int Run()
    {
        Console.CursorVisible = false;

        ConsoleKey keyPressed;
        DisplayOptions();

        do
        {
            var keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (keyPressed == ConsoleKey.DownArrow)
            {
                if (CanNavigateNext())
                    Navigate(NavigationDirections.Next);
            }
            else if (keyPressed == ConsoleKey.UpArrow)
            {
                if (CanNavigatePrevious())
                    Navigate(NavigationDirections.Previous);
            }
        } while (keyPressed != ConsoleKey.Enter);

        MoveCursorBottom();
        Console.ResetColor();
        Console.CursorVisible = true;

        return _selectedIndex;
    }

    /// <summary>
    ///     Write an option.
    /// </summary>
    /// <param name="isSelected">True if the option is the selected option.</param>
    /// <param name="option">The option to be displayed.</param>
    private static void WriteOption(bool isSelected, string option)
    {
        Console.ForegroundColor = isSelected ? ConsoleColor.Green : ConsoleColor.Gray;
        Console.WriteLine(isSelected ? $@"* << {option} >>" : $@"  << {option} >>");
    }

    /// <summary>
    ///     Display the menu options.
    ///     A different style is applied to the option that is selected.
    /// </summary>
    private void DisplayOptions()
    {
        Console.WriteLine(_prompt);

        for (var i = 0; i < _options.Length; i++) WriteOption(_selectedIndex == i, _options[i]);

        _leftConsole = Console.GetCursorPosition().Left;
        _topConsole = Console.GetCursorPosition().Top - _options.Length;

        Console.SetCursorPosition(_leftConsole, _topConsole);
    }

    /// <summary>
    ///     Check if it is possible to navigate to the previous element in the menu.
    /// </summary>
    /// <returns>True if possible, false otherwise.</returns>
    private bool CanNavigatePrevious()
    {
        return _selectedIndex > 0;
    }

    /// <summary>
    ///     Check if it is possible to navigate to the next element in the menu.
    /// </summary>
    /// <returns>True if possible, false otherwise.</returns>
    private bool CanNavigateNext()
    {
        return _selectedIndex < _options.Length - 1;
    }

    /// <summary>
    ///     Navigate to the next or previous item on the menu.
    /// </summary>
    private void Navigate(NavigationDirections direction)
    {
        // Rewrite old selected.
        WriteOption(false, _options[_selectedIndex]);

        switch (direction)
        {
            case NavigationDirections.Next:
                _selectedIndex++;
                _topConsole++;
                break;
            case NavigationDirections.Previous:
                _selectedIndex--;
                _topConsole--;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        Console.SetCursorPosition(_leftConsole, _topConsole);

        // Rewrite new selected.
        WriteOption(true, _options[_selectedIndex]);

        // Place the cursor nicely.
        _leftConsole = 0;
        Console.SetCursorPosition(_leftConsole, _topConsole);
    }

    /// <summary>
    ///     Move the cursor at the bottom.
    /// </summary>
    private void MoveCursorBottom()
    {
        _leftConsole = 0;
        _topConsole += _options.Length - _selectedIndex;

        Console.SetCursorPosition(_leftConsole, _topConsole);
    }
}