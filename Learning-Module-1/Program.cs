// Display a welcome message
Console.WriteLine("Welcome to the greeting program!");

// Ask for the user's name
Console.Write("What is your name? ");
string name = Console.ReadLine() ?? "Ben Harry"; // Default name if input is null (Ben Harry is a nod to Binary)

// Ask for the user's age
Console.Write("How old are you? ");
string age = Console.ReadLine() ?? "Unknown"; // Default age if input is null

// Display a personalized greeting
Console.WriteLine($"Hello, {name}! It's great to meet you. Your age is {age}.");