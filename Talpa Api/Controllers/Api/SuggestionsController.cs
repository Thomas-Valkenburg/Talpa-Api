using Microsoft.AspNetCore.Mvc;

namespace Talpa_Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : ControllerBase
    {

        private double CalculateSimilarityPercentage(string string1, string string2)
        {
            List<string> pairs1 = WordLetterPairs(string1.ToUpper());
            List<string> pairs2 = WordLetterPairs(string2.ToUpper());

            int intersection = 0;
            int union        = pairs1.Count + pairs2.Count;

            foreach (var pair1 in pairs1)
            {
                for (int number = 0; number < pairs2.Count; number++)
                {
                    if (pair1 == pairs2[number])
                    {
                        intersection++;
                        pairs2.RemoveAt(number);
                        break;
                    }
                }
            }

            // return the percentage of similarity
            return (2.0 * intersection * 100) / union;
        }

        // Required for the CalculateSimilarityPercentage method.
        private List<string> WordLetterPairs(string str)
        {
            List<string> allPairs = new List<string>();
            string[]     words    = str.Split(' ');

            foreach (string word in words)
            {
                for (int number = 0; number < word.Length - 1; number++)
                {
                    allPairs.Add(word.Substring(number, 2));
                }
            }

            return allPairs;
        }
    }
}
