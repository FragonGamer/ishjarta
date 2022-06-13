
using System.Collections.Generic;

public enum Rarity
{

    
        common = 50,
        uncommon = 33,
        rare = 15,
        legendary = 2
    

    
}
public static class RarityRange
{

        public static Dictionary<string, int> CommonRange = new Dictionary<string, int>()
                { { "Min", 0 }, { "Max", 49 } };
        public static Dictionary<string, int> UncommonRange = new Dictionary<string, int>()
                { { "Min", 50 }, { "Max", 83 } };
        public static Dictionary<string, int> RareRange = new Dictionary<string, int>()
                { { "Min", 84 }, { "Max", 98 } };
        public static Dictionary<string, int> LegendaryRange = new Dictionary<string, int>()
                { { "Min", 99 }, { "Max", 100 } };

}
