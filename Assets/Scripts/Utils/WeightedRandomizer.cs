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

    private Random _random;
    private List<KeyValuePair<T, int>> _sortedItems;
    private int totalWeights;

    /// <param name="weights">
    ///		An ordered list with the current spawn rates. The list will be updated
    ///     so that selected items will have a smaller chance of being repeated.
    /// </param>
    public WeightedRandomizer(Dictionary<T, int> weights) {
        _random = new Random();

        // Sorts the weights list
        _sortedItems = Sort(weights);

        // Sums all spawn rates
        foreach (var spawn in weights) {
            totalWeights += spawn.Value;
        }
    }

    public void UpdateWeights(Dictionary<T, int> weights) {
        // Sorts the weights list
        _sortedItems = Sort(weights);

        totalWeights = 0;
        // Sums all spawn rates
        foreach (var spawn in weights) {
            totalWeights += spawn.Value;
        }
    }

    /// <summary>
    /// Randomizes one item
    /// </summary>
    /// <returns>The randomized item.</returns>
    public T TakeOne() {
        // Randomizes a number from Zero to Sum
        int roll = _random.Next(0, totalWeights);

        // Finds chosen item based on spawn rate
        T selected = _sortedItems[_sortedItems.Count - 1].Key;
        foreach (var spawn in _sortedItems) {
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