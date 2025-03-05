using System.Collections.Generic;
using UnityEngine;

public static class ContainersUtility {
    public static void Randomize<T>(this List<T> list) {
        int count = list.Count;
        int last = count - 1;

        for (int i = 0; i < last; i++) {
            int rand = Random.Range(i, count);
            list.Swap(i, rand);
        }
    }

    public static void Swap<T>(this List<T> list, int firstIndex, int secondIndex) {
        (list[firstIndex], list[secondIndex]) = (list[secondIndex], list[firstIndex]);
    }

    public static T Pop<T>(this List<T> list, int index) {
        T obj = list[index];
        list.RemoveAt(index);
        return obj;
    }

    public static T PopRandom<T>(this List<T> list) {
        int index = Random.Range(0, list.Count);
        return list.Pop(index);
    }

    public static T GetRandom<T>(this List<T> list) {
        int index = Random.Range(0, list.Count);
        return list[index];
    }

    public static T GetRandom<T>(this T[] array) {
        int index = Random.Range(0, array.Length);
        return array[index];
    }
}
