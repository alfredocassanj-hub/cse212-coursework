
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Find symmetric pairs of two-character words.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        HashSet<string> wordSet = new HashSet<string>(words);
        HashSet<string> pairs = new HashSet<string>();

        foreach (string word in wordSet)
        {
            if (word.Length != 2)
                continue;

            string reversed = $"{word[1]}{word[0]}";

            if (word[0] != word[1] && wordSet.Contains(reversed))
            {
                string pair = string.CompareOrdinal(word, reversed) < 0
                    ? $"{word} & {reversed}"
                    : $"{reversed} & {word}";

                pairs.Add(pair);
            }
        }

        return new List<string>(pairs).ToArray();
    }

    /// <summary>
    /// Summarize the degrees earned by people in a census file.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");

            if (fields.Length <= 3)
                continue;

            string degree = fields[3].Trim();

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if two words are anagrams.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        Dictionary<char, int> letters = new Dictionary<char, int>();

        foreach (char character in word1)
        {
            if (char.IsWhiteSpace(character))
                continue;

            char letter = char.ToLowerInvariant(character);

            if (letters.ContainsKey(letter))
            {
                letters[letter]++;
            }
            else
            {
                letters[letter] = 1;
            }
        }

        foreach (char character in word2)
        {
            if (char.IsWhiteSpace(character))
                continue;

            char letter = char.ToLowerInvariant(character);

            if (!letters.ContainsKey(letter))
                return false;

            letters[letter]--;

            if (letters[letter] < 0)
                return false;
        }

        foreach (int count in letters.Values)
        {
            if (count != 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Retrieve earthquake data from the USGS website.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var response = client.Send(getRequestMessage);

        response.EnsureSuccessStatusCode();

        using var jsonStream = response.Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);

        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var featureCollection =
            JsonSerializer.Deserialize<FeatureCollection>(json, options);

        if (featureCollection?.Features == null)
        {
            return Array.Empty<string>();
        }

        List<string> summaries = new List<string>();

        foreach (var feature in featureCollection.Features)
        {
            string place = feature.Properties?.Place ?? "Unknown location";
            double? magnitude = feature.Properties?.Mag;

            if (magnitude.HasValue)
            {
                summaries.Add($"{place} - Mag {magnitude.Value}");
            }
        }

        return summaries.ToArray();
    }
}
