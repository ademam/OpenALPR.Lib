using OpenALPR.Lib;

namespace OpenALPR.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("Usage: OpenALPR.Console.exe imagePath");
                return;
            }

            var lib = new OpenALPRLib();
            var res = lib.GetBestMatch(Country.EU, args[0]);
            System.Console.WriteLine("Best match: "+ res);
        }
    }
}
