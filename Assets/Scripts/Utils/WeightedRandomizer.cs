using System;
using System.Collections.Generic;

/// <summary>
/// Static class to improve readability
/// Example:
/// <code>
/// var selected = WeightedRandomizer.From(weights).TakeOne();
/// </code>
/// 
/// </summary>
public class WeightedRandomizer<T> {

    private static WeightedRandomizer<T> _instance;
    private static Dictionary<T, int> _weights;

    private static Random _random = new Random();

    /// <param name="weights">
    ///		An ordered list with the current spawn rates. The list will be updated
    ///     so that selected items will have a smaller chance of being repeated.
    /// </param>
    public static WeightedRandomizer<T> From(Dictionary<T, int> weights) {
        if (_instance == null) {
            _instance = new WeightedRandomizer<T>();
        }
        _weights = weights;
        return _instance;
    }

    /// <summary>
    /// Randomizes one item
    /// </summary>
    /// <returns>The randomized item.</returns>
    public T TakeOne() {
        // Sorts the spawn rate list
        var sortedSpawnRate = Sort(_weights);

        // Sums all spawn rates
        int sum = 0;
        foreach (var spawn in _weights) {
            sum += spawn.Value;
        }

        // Randomizes a number from Zero to Sum
        int roll = _random.Next(0, sum);

        // Finds chosen item based on spawn rate
        T selected = sortedSpawnRate[sortedSpawnRate.Count - 1].Key;
        foreach (var spawn in sortedSpawnRate) {
            if (roll < spawn.Value) {
                selected = spawn.Key;
                break;
            }
            roll -= spawn.Value;
        }

        // Returns the selected item
        return selected;
    }

    private List<KeyValuePair<T, int>> Sort(Dictionary<T, int> weights) {
        var list = new List<KeyValuePair<T, int>>(weights);

        // Sorts the Weight List for randomization later
        list.Sort(
            delegate (KeyValuePair<T, int> firstPair, KeyValuePair<T, int> nextPair) {
                return firstPair.Value.CompareTo(nextPair.Value);
            }
         );

        return list;
    }
}