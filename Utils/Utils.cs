namespace Utils
{
    public static class Utils
    {
        public static string newLineStr = Environment.NewLine;

        public static void DisplayHelpIfArgIsHelp(string[] args, string helpText)
        {
            if (args.Length > 0 && args[0] == "--help")
            {
                Console.WriteLine(helpText);
                Environment.Exit(0);
            }
        }
    }
}
