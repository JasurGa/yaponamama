using System;
using System.Collections.Generic;

namespace Atlas.Application.Common.Helpers
{
    public class TranslitConverter
    {
        private static readonly Dictionary<char, string> RuEnAlphabet = new Dictionary<char, string>()
        {
            {'а', "a"},
            {'б', "b"},
            {'в', "v"},
            {'г', "g"},
            {'д', "d"},
            {'е', "e"},
            {'ё', "yo"},
            {'ж', "j"},
            {'з', "z"},
            {'и', "i"},
            {'й', "yi"},
            {'к', "k"},
            {'л', "l"},
            {'м', "m"},
            {'н', "n"},
            {'о', "o"},
            {'п', "p"},
            {'р', "r"},
            {'с', "s"},
            {'т', "t"},
            {'у', "u"},
            {'ф', "f"},
            {'х', "h"},
            {'ц', "c"},
            {'ч', "ch"},
            {'ш', "sh"},
            {'щ', "sh"},
            {'ы', "i"},
            {'э', "e"},
            {'ю', "yu"},
            {'я', "ya"}
        };

        private static readonly Dictionary<char, string> EnRuAlphabet = new Dictionary<char, string>()
        {
            {'a', "а"},
            {'b', "б"},
            {'c', "к"},
            {'d', "д"},
            {'e', "е"},
            {'f', "ф"},
            {'g', "г"},
            {'h', "х"},
            {'i', "и"},
            {'j', "ж"},
            {'k', "к"},
            {'l', "л"},
            {'m', "м"},
            {'n', "н"},
            {'o', "о"},
            {'p', "п"},
            {'q', "к"},
            {'r', "р"},
            {'s', "с"},
            {'t', "т"},
            {'u', "у"},
            {'v', "в"},
            {'w', "в"},
            {'x', "й"},
            {'z', "з"}
        };

        public static string TranslitRuEn(string ruString)
        {
            ruString = ruString.ToLower();

            var result = "";
            foreach (var c in ruString)
            {
                if (RuEnAlphabet.ContainsKey(c))
                {
                    result += RuEnAlphabet[c];
                }
                else
                {
                    result += c;
                }
            }

            return result;
        }

        public static string TranslitEnRu(string enString)
        {
            enString = enString.ToLower();

            var result = "";
            foreach (var c in enString)
            {
                if (EnRuAlphabet.ContainsKey(c))
                {
                    result += EnRuAlphabet[c];
                }
                else
                {
                    result += c;
                }
            }

            return result;
        }
    }
}

