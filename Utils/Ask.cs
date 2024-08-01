using static Utils.Utils;

namespace Utils
{
    public static class Ask
    {
        public static string AskStringForUser(string text)
        {
            var str = "";
            while (true)
            {
                Console.WriteLine(text);
                str = Console.ReadLine();
                if (str != null)
                {
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

        public static void ParseOrAskENumValue<ENum>(string text, ref ENum variable, int requiredArgIndex = -1, string[]? args = null) where ENum : struct, Enum
        {
            var enumType = typeof(ENum);
            if (!enumType.IsEnum)
            {
                return;
            }

            if (args == null || args.Length == 0 || requiredArgIndex >= 0 && args.Length >= requiredArgIndex + 1 && !Enum.TryParse(args[requiredArgIndex], out variable) && Enum.IsDefined(enumType, variable))
            {
                var enumFullName = enumType.FullName;
                while (true)
                {
                    // Display the list of enum values
                    Console.WriteLine(enumFullName + " List:");
                    foreach (var enumValue in Enum.GetValues(enumType))
                    {
                        Console.WriteLine((int)enumValue + ": " + enumValue); // Example: "0: MediaCatalog"
                    }
                    Console.WriteLine();

                    var specifiedENumStr = AskStringForUser(text);
                    if (Enum.TryParse(specifiedENumStr, out variable) && Enum.IsDefined(enumType, variable))
                        break;

                    Console.WriteLine($"Parse to {enumFullName} failed" + newLineStr + "Please enter it again");
                    Console.WriteLine();
                }
            }
        }

        public static void ParseOrAskBooleanValue(string text, ref bool variable, int requiredArgIndex = -1, string[]? args = null)
        {
            if (args == null || args.Length == 0 || requiredArgIndex >= 0 && args.Length >= requiredArgIndex + 1 && !Boolean.TryParse(args[requiredArgIndex], out variable))
            {
                while (true)
                {
                    var specifiedBoolStr = AskStringForUser(text);
                    if (Boolean.TryParse(specifiedBoolStr, out variable))
                        break;

                    Console.WriteLine("Parse to boolean failed" + newLineStr + "Please enter it again");
                    Console.WriteLine();
                }
            }
        }

        public static string ParseOrAskAndValidPath(bool isFile, string text, int requiredArgIndex = -1, string[]? args = null)
        {
            var path = "";
            var checkTargetName = isFile ? "file" : "directory";
            var count = 0;
            while (true)
            {
                // Prevents an infinite loop if the path specified in the argument is incorrect
                if (args != null && requiredArgIndex >= 0 && count == 0)
                {
                    path = ParseStrFromArgOrAsk(requiredArgIndex, text, args);
                }
                else
                {
                    path = AskStringForUser(text);
                }

                path = path.Replace("\"", "");

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
