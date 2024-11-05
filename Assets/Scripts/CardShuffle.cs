using System;
using System.Collections;
using System.Collections.Generic;



/// <summary>
/// Ï´ÅÆ¹¤¾ß
/// </summary>
public static class CardShuffle 
{
    private static readonly Random random=new Random();

    public static void Shuffle<T>(this List<T> list)
    {
        int n=list.Count;
        while (n-- > 0) { 
            var index=random.Next(n+1);
            (list[n], list[index]) = (list[index], list[n]);
        }
    }
}
