using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using openalprnet;
using OpenALPR.Lib.Data;

namespace OpenALPR.Lib
{
    public class OpenALPRLib
    {
        /// <summary>
        /// Gets best license plate match for given image
        /// </summary>
        /// <param name="country">Country (US / EU)</param>
        /// <param name="imagePath">Image path</param>
        /// <returns>Best license plate match</returns>
        public string GetBestMatch(Country country, string imagePath)
        {
            var all = GetPlateNumberCandidates(country, imagePath);
            if (all.Any())
            {
                return all.First().PlateNumber;
            }

            return null;
        }

        /// <summary>
        /// Gets all license plate matches for their confidence for given image
        /// </summary>
        /// <param name="country">Country (US / EU)</param>
        /// <param name="imagePath">Image path</param>
        /// <returns>License plate matches for their confidence</returns>
        public List<Result> GetPlateNumberCandidates(Country country, string imagePath)
        {
            var ass = Assembly.GetExecutingAssembly();
            var assemblyDirectory = Path.GetDirectoryName(ass.Location);

            var configFile = Path.Combine(assemblyDirectory, "openalpr.conf");
            var runtimeDataDir = Path.Combine(assemblyDirectory, "runtime_data");
            using (var alpr = new AlprNet(country == Country.EU ? "eu" : "us", configFile, runtimeDataDir))
            {
                if (!alpr.isLoaded())
                {
                    throw new Exception("Error initializing OpenALPR");
                }

                var results = alpr.recognize(imagePath);
                return results.plates.SelectMany(l => l.topNPlates.Select(plate => new Result
                {
                    PlateNumber = plate.characters,
                    Confidence = plate.overall_confidence
                })).ToList();
            }
        }
    }
}
