using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Package.Mongo.Utils
{
    /// <summary>
    /// Container for registered Vocabularies.  At present, only a single vocabulary is supported: Default.
    /// </summary>
    public static class Vocabularies
    {
        private static readonly Lazy<Vocabulary> Instance;

        static Vocabularies()
        {
            Instance = new Lazy<Vocabulary>(BuildDefault, LazyThreadSafetyMode.PublicationOnly);
        }

        /// <summary>
        /// The default vocabulary used for singular/plural irregularities.
        /// Rules can be added to this vocabulary and will be picked up by called to Singularize() and Pluralize().
        /// At this time, multiple vocabularies and removing existing rules are not supported.
        /// </summary>
        public static Vocabulary Default => Instance.Value;

        private static Vocabulary BuildDefault()
        {
            var vocabulary = new Vocabulary();

            vocabulary.AddPlural("$", "s");
            vocabulary.AddPlural("s$", "s");
            vocabulary.AddPlural("(ax|test)is$", "$1es");
            vocabulary.AddPlural("(octop|vir|alumn|fung|cact|foc|hippopotam|radi|stimul|syllab|nucle)us$", "$1i");
            vocabulary.AddPlural("(alias|bias|iris|status|campus|apparatus|virus|walrus|trellis)$", "$1es");
            vocabulary.AddPlural("(buffal|tomat|volcan|ech|embarg|her|mosquit|potat|torped|vet)o$", "$1oes");
            vocabulary.AddPlural("([dti])um$", "$1a");
            vocabulary.AddPlural("sis$", "ses");
            vocabulary.AddPlural("(?:([^f])fe|([lr])f)$", "$1$2ves");
            vocabulary.AddPlural("(hive)$", "$1s");
            vocabulary.AddPlural("([^aeiouy]|qu)y$", "$1ies");
            vocabulary.AddPlural("(x|ch|ss|sh)$", "$1es");
            vocabulary.AddPlural("(matr|vert|ind|d)ix|ex$", "$1ices");
            vocabulary.AddPlural("([m|l])ouse$", "$1ice");
            vocabulary.AddPlural("^(ox)$", "$1en");
            vocabulary.AddPlural("(quiz)$", "$1zes");
            vocabulary.AddPlural("(buz|blit|walt)z$", "$1zes");
            vocabulary.AddPlural("(hoo|lea|loa|thie)f$", "$1ves");
            vocabulary.AddPlural("(alumn|alg|larv|vertebr)a$", "$1ae");
            vocabulary.AddPlural("(criteri|phenomen)on$", "$1a");

            vocabulary.AddSingular("s$", "");
            vocabulary.AddSingular("(n)ews$", "$1ews");
            vocabulary.AddSingular("([dti])a$", "$1um");
            vocabulary.AddSingular("(analy|ba|diagno|parenthe|progno|synop|the|ellip|empha|neuro|oa|paraly)ses$", "$1sis");
            vocabulary.AddSingular("([^f])ves$", "$1fe");
            vocabulary.AddSingular("(hive)s$", "$1");
            vocabulary.AddSingular("(tive)s$", "$1");
            vocabulary.AddSingular("([lr]|hoo|lea|loa|thie)ves$", "$1f");
            vocabulary.AddSingular("(^zomb)?([^aeiouy]|qu)ies$", "$2y");
            vocabulary.AddSingular("(s)eries$", "$1eries");
            vocabulary.AddSingular("(m)ovies$", "$1ovie");
            vocabulary.AddSingular("(x|ch|ss|sh)es$", "$1");
            vocabulary.AddSingular("([m|l])ice$", "$1ouse");
            vocabulary.AddSingular("(o)es$", "$1");
            vocabulary.AddSingular("(shoe)s$", "$1");
            vocabulary.AddSingular("(cris|ax|test)es$", "$1is");
            vocabulary.AddSingular("(octop|vir|alumn|fung|cact|foc|hippopotam|radi|stimul|syllab|nucle)i$", "$1us");
            vocabulary.AddSingular("(alias|bias|iris|status|campus|apparatus|virus|walrus|trellis)es$", "$1");
            vocabulary.AddSingular("^(ox)en", "$1");
            vocabulary.AddSingular("(matr|d)ices$", "$1ix");
            vocabulary.AddSingular("(vert|ind)ices$", "$1ex");
            vocabulary.AddSingular("(quiz)zes$", "$1");
            vocabulary.AddSingular("(buz|blit|walt)zes$", "$1z");
            vocabulary.AddSingular("(alumn|alg|larv|vertebr)ae$", "$1a");
            vocabulary.AddSingular("(criteri|phenomen)a$", "$1on");

            vocabulary.AddIrregular("person", "people");
            vocabulary.AddIrregular("man", "men");
            vocabulary.AddIrregular("child", "children");
            vocabulary.AddIrregular("sex", "sexes");
            vocabulary.AddIrregular("move", "moves");
            vocabulary.AddIrregular("goose", "geese");
            vocabulary.AddIrregular("wave", "waves");
            vocabulary.AddIrregular("die", "dice");
            vocabulary.AddIrregular("foot", "feet");
            vocabulary.AddIrregular("tooth", "teeth");
            vocabulary.AddIrregular("curriculum", "curricula");
            vocabulary.AddIrregular("database", "databases");
            vocabulary.AddIrregular("zombie", "zombies");

            vocabulary.AddIrregular("is", "are", matchEnding: false);
            vocabulary.AddIrregular("that", "those", matchEnding: false);
            vocabulary.AddIrregular("this", "these", matchEnding: false);
            vocabulary.AddIrregular("bus", "buses", matchEnding: false);

            vocabulary.AddUncountable("equipment");
            vocabulary.AddUncountable("information");
            vocabulary.AddUncountable("rice");
            vocabulary.AddUncountable("money");
            vocabulary.AddUncountable("species");
            vocabulary.AddUncountable("series");
            vocabulary.AddUncountable("fish");
            vocabulary.AddUncountable("sheep");
            vocabulary.AddUncountable("deer");
            vocabulary.AddUncountable("aircraft");
            vocabulary.AddUncountable("oz");
            vocabulary.AddUncountable("tsp");
            vocabulary.AddUncountable("tbsp");
            vocabulary.AddUncountable("ml");
            vocabulary.AddUncountable("l");
            vocabulary.AddUncountable("water");
            vocabulary.AddUncountable("waters");
            vocabulary.AddUncountable("semen");
            vocabulary.AddUncountable("sperm");
            vocabulary.AddUncountable("bison");
            vocabulary.AddUncountable("grass");
            vocabulary.AddUncountable("hair");
            vocabulary.AddUncountable("mud");
            vocabulary.AddUncountable("elk");
            vocabulary.AddUncountable("luggage");
            vocabulary.AddUncountable("moose");
            vocabulary.AddUncountable("offspring");
            vocabulary.AddUncountable("salmon");
            vocabulary.AddUncountable("shrimp");
            vocabulary.AddUncountable("someone");
            vocabulary.AddUncountable("swine");
            vocabulary.AddUncountable("trout");
            vocabulary.AddUncountable("tuna");
            vocabulary.AddUncountable("corps");
            vocabulary.AddUncountable("scissors");
            vocabulary.AddUncountable("means");

            return vocabulary;
        }
    }
    /// <summary>
    /// A container for exceptions to simple pluralization/singularization rules.
    /// Vocabularies.Default contains an extensive list of rules for US English.
    /// At this time, multiple vocabularies and removing existing rules are not supported.
    /// </summary>
    public class Vocabulary
    {
        internal Vocabulary()
        {
        }

        private readonly List<Rule> _plurals = new();
        private readonly List<Rule> _singulars = new();
        private readonly List<string> _uncountables = new();

        /// <summary>
        /// Adds a word to the vocabulary which cannot easily be pluralized/singularized by RegEx, e.g. "person" and "people".
        /// </summary>
        /// <param name="singular">The singular form of the irregular word, e.g. "person".</param>
        /// <param name="plural">The plural form of the irregular word, e.g. "people".</param>
        /// <param name="matchEnding">True to match these words on their own as well as at the end of longer words. False, otherwise.</param>
        public void AddIrregular(string singular, string plural, bool matchEnding = true)
        {
            if (matchEnding)
            {
                AddPlural("(" + singular[0] + ")" + singular[1..] + "$", "$1" + plural.Substring(1));
                AddSingular("(" + plural[0] + ")" + plural[1..] + "$", "$1" + singular.Substring(1));
            }
            else
            {
                AddPlural($"^{singular}$", plural);
                AddSingular($"^{plural}$", singular);
            }
        }

        /// <summary>
        /// Adds an uncountable word to the vocabulary, e.g. "fish".  Will be ignored when plurality is changed.
        /// </summary>
        /// <param name="word">Word to be added to the list of uncountables.</param>
        public void AddUncountable(string word)
        {
            _uncountables.Add(word.ToLower());
        }

        /// <summary>
        /// Adds a rule to the vocabulary that does not follow trivial rules for pluralization, e.g. "bus" -> "buses"
        /// </summary>
        /// <param name="rule">RegEx to be matched, case insensitive, e.g. "(bus)es$"</param>
        /// <param name="replacement">RegEx replacement  e.g. "$1"</param>
        public void AddPlural(string rule, string replacement)
        {
            _plurals.Add(new Rule(rule, replacement));
        }

        /// <summary>
        /// Adds a rule to the vocabulary that does not follow trivial rules for singularization, e.g. "vertices/indices -> "vertex/index"
        /// </summary>
        /// <param name="rule">RegEx to be matched, case insensitive, e.g. ""(vert|ind)ices$""</param>
        /// <param name="replacement">RegEx replacement  e.g. "$1ex"</param>
        public void AddSingular(string rule, string replacement)
        {
            _singulars.Add(new Rule(rule, replacement));
        }

        /// <summary>
        /// Pluralizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be pluralized</param>
        /// <param name="inputIsKnownToBeSingular">Normally you call Pluralize on singular words; but if you're unsure call it with false</param>
        /// <returns></returns>
        public string Pluralize(string word, bool inputIsKnownToBeSingular = true)
        {
            var result = ApplyRules(_plurals, word);

            if (inputIsKnownToBeSingular)
                return result;

            var asSingular = ApplyRules(_singulars, word);
            var asSingularAsPlural = ApplyRules(_plurals, asSingular);
            if (asSingular != null && asSingular != word && asSingular + "s" != word && asSingularAsPlural == word && result != word)
                return word;

            return result;
        }

        /// <summary>
        /// Singularizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be singularized</param>
        /// <param name="inputIsKnownToBePlural">Normally you call Singularize on plural words; but if you're unsure call it with false</param>
        /// <returns></returns>
        public string Singularize(string word, bool inputIsKnownToBePlural = true)
        {
            var result = ApplyRules(_singulars, word);

            if (inputIsKnownToBePlural)
                return result;

            // the Plurality is unknown so we should check all possibilities
            var asPlural = ApplyRules(_plurals, word);
            var asPluralAsSingular = ApplyRules(_singulars, asPlural);
            if (asPlural != word && word + "s" != asPlural && asPluralAsSingular == word && result != word)
                return word;

            return result ?? word;
        }

        private string ApplyRules(IList<Rule> rules, string word)
        {
            if (word == null)
                return null;

            if (IsUncountable(word))
                return word;

            var result = word;
            for (var i = rules.Count - 1; i >= 0; i--)
            {
                if ((result = rules[i].Apply(word)) != null)
                    break;
            }
            return result;
        }

        private bool IsUncountable(string word)
        {
            return _uncountables.Contains(word.ToLower());
        }

        private class Rule
        {
            private readonly Regex _regex;
            private readonly string _replacement;

            public Rule(string pattern, string replacement)
            {
                _regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                _replacement = replacement;
            }

            public string Apply(string word)
            {
                if (!_regex.IsMatch(word))
                    return null;

                return _regex.Replace(word, _replacement);
            }
        }
    }

    /// <summary>
    /// Inflector extensions
    /// </summary>
    public static class InflectorExtensions
    {
        /// <summary>
        /// Pluralizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be pluralized</param>
        /// <param name="inputIsKnownToBeSingular">Normally you call Pluralize on singular words; but if you're unsure call it with false</param>
        /// <returns></returns>
        public static string Pluralize(this string word, bool inputIsKnownToBeSingular = true)
        {
            return Vocabularies.Default.Pluralize(word, inputIsKnownToBeSingular);
        }

        /// <summary>
        /// Singularizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be singularized</param>
        /// <param name="inputIsKnownToBePlural">Normally you call Singularize on plural words; but if you're unsure call it with false</param>
        /// <returns></returns>
        public static string Singularize(this string word, bool inputIsKnownToBePlural = true)
        {
            return Vocabularies.Default.Singularize(word, inputIsKnownToBePlural);
        }

        /// <summary>
        /// By default, pascalize converts strings to UpperCamelCase also removing underscores
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Pascalize(this string input)
        {
            return Regex.Replace(input, "(?:^|_)(.)", match => match.Groups[1].Value.ToUpper());
        }

        /// <summary>
        /// Same as Pascalize except that the first character is lower case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Camelize(this string input)
        {
            var word = Pascalize(input);
            return word.Substring(0, 1).ToLower() + word.Substring(1);
        }

        /// <summary>
        /// Separates the input words with underscore
        /// </summary>
        /// <param name="input">The string to be underscored</param>
        /// <returns></returns>
        public static string Underscore(this string input)
        {
            return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(input, @"([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])", "$1_$2"), @"[-\s]", "_").ToLower();
        }

        /// <summary>
        /// Replaces underscores with dashes in the string
        /// </summary>
        /// <param name="underscoredWord"></param>
        /// <returns></returns>
        public static string Dasherize(this string underscoredWord)
        {
            return underscoredWord.Replace('_', '-');
        }

        /// <summary>
        /// Replaces underscores with hyphens in the string
        /// </summary>
        /// <param name="underscoredWord"></param>
        /// <returns></returns>
        public static string Hyphenate(this string underscoredWord)
        {
            return Dasherize(underscoredWord);
        }
    }
}