using System.Collections.Generic;

namespace Shared.Utils
{
    public class Helpers
    {
        public static Dictionary<string, string> GetArguments(string[] args)
        {
            var arguments = new Dictionary<string, string>();

            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length - 1; i += 2)
                {
                    var key = args[i].TrimStart('-').ToLower();
                    var value = args[i + 1];
                    arguments.Add(key, value);
                }
            }
            return arguments;
        }

    }
}
