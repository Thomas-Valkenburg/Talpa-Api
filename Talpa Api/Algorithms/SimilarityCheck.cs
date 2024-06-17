namespace Talpa_Api.Algorithms;

public abstract class SimilarityCheck
{
	public readonly struct ObjectWithSimilarity(int suggestionId, string title, double similarity)
	{
		public int Id { get; init; } = suggestionId;
		public string Title { get; init; } = title;
		public double Similarity { get; init; } = similarity;
	}

	private static double CalculateSimilarityPercentage(string string1, string string2)
	{
		var pairs1 = WordLetterPairs(string1.ToUpper());
		var pairs2 = WordLetterPairs(string2.ToUpper());

		var intersection = 0;
		var union = pairs1.Count + pairs2.Count;

		foreach (var pair1 in pairs1)
		{
			for (var number = 0; number < pairs2.Count; number++)
			{
				if (pair1 != pairs2[number]) continue;

				intersection++;
				pairs2.RemoveAt(number);
				break;
			}
		}

		// return the percentage of similarity
		return 2.0 * intersection * 100 / union;
	}

	private static List<string> WordLetterPairs(string str)
	{
		var allPairs = new List<string>();
		var words = str.Split(' ');

		foreach (var word in words)
		{
			for (var number = 0; number < word.Length - 1; number++)
			{
				allPairs.Add(word.Substring(number, 2));
			}
		}

		return allPairs;
	}

	public static (List<ObjectWithSimilarity> objects, double max) GetObjectWithSimilarity(string title, dynamic dbSet)
	{
		var objectWithSimilarity = new List<ObjectWithSimilarity>();
		var maxSimilarity = 0.0;

		foreach (var obj in dbSet)
		{
			var sim = CalculateSimilarityPercentage(obj.Title, title);

			if (sim > maxSimilarity) maxSimilarity = sim;

			if (sim > 50) objectWithSimilarity.Add(new ObjectWithSimilarity(obj.Id, obj.Title, sim));
		}

		return (objectWithSimilarity, maxSimilarity);
	}
}