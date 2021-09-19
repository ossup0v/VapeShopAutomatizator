using System;

namespace VapeShopAutomatizator.Common
{
    public static class StringEx
    {
#warning todo optimaze
        public static string RemoveExtraNewLines(this string str)
        {
            return str.RemoveDuplicates("\n ")
                .RemoveDuplicates("\n")
                .RemoveDuplicates(" \n ")
                .RemoveDuplicates(" \n")
                .RemoveDuplicates(Environment.NewLine);
        }

        public static string RemoveDuplicates(this string str, string toRemove)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == toRemove[0])
                {
                    var needToRemove = false;
                    for (int j = i; j < i + toRemove.Length && j < str.Length; j++)
                    {
                        if (str[j] != toRemove[j - i])
                        {
                            needToRemove = false;
                            break;
                        }

                        if (AreDuplicate(str, toRemove, i))
                            needToRemove = true;
                    }

                    if (needToRemove && i + toRemove.Length < str.Length)
                    {
                        str = str.Remove(i, toRemove.Length);
                        i -= toRemove.Length;
                    }
                }
            }

            return str;
        }

        private static bool AreDuplicate(string source, string target, int index)
        {
            for (int i = index - target.Length, j = 0; i < source.Length && j < target.Length; i++, j++)
            {
                if (i < 0)
                    return false;

                if (source[i] != target[j])
                    return false;
            }

            return true;
        }
    }
}
