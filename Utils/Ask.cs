namespace Utils
{
    public static class Ask
    {
        public static string newLineStr = Environment.NewLine;
        public static string AskStringForUser(string text)
        {
            var str = "";
            while (true)
            {
                Console.WriteLine(text);
                str = Console.ReadLine();
                if (str != null)
                {
                    str = str.Replace("\"", "");
                    break;
                }

                Console.WriteLine("Variable is null" + newLineStr + "Please enter it again");
            }
            return str;
        }

        public static string ParseStrFromArgOrAsk(int requiredArgIndex, string text, string[] args)
        {
            var str = "";
            if (args.Length >= requiredArgIndex + 1)
            {
                str = args[requiredArgIndex];
            }
            else
            {
                str = AskStringForUser(text);
            }
            return str;
        }

        public static string AskAndValidPath(bool isFile, string text, int requiredArgIndex, string[] args)
        {
            var path = "";
            var checkTargetName = isFile ? "file" : "directory";
            var count = 0;
            while (true)
            {
                // Prevents an infinite loop if the path specified in the argument is incorrect
                if (count == 0)
                {
                    path = ParseStrFromArgOrAsk(requiredArgIndex, text, args);
                }
                else
                {
                    path = AskStringForUser(text);
                }

                if (isFile && File.Exists(path) || !isFile && Directory.Exists(path))
                {
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("\"" + path + "\"" + " is not a " + checkTargetName + newLineStr + "Please try again");
                count++;
            }

            return path;
        }
    }
}
