using OpenALPR.Lib;
using OpenALPR.Lib.Data;

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
            System.Console.WriteLine("Best match: " + res);
            System.Console.WriteLine();

            var all = lib.GetPlateNumberCandidates(Country.EU, args[0]);
            System.Console.WriteLine("All matches");
            foreach (var result in all)
            {
                System.Console.WriteLine("- {0} confidence {1}", result.PlateNumber.PadRight(12), result.Confidence);
            }
        }
    }
}
