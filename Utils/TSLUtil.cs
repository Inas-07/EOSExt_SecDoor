using AmorLib.Utils.JsonElementConverters;
using GTFO.API;

namespace EOSExt.SecDoor.Utils
{
    /// <summary>
    /// Resolves text through TSL (Text Substitution Library), if it is installed.
    /// </summary>
    /// <remarks>
    /// TSL does not ship an assembly other plugins can reference; it publishes its parser through
    /// <see cref="InteropAPI"/> instead. <see cref="InteropAPI.Call"/> returns <see langword="null"/>
    /// when no plugin registered the call, so text is passed through untouched when TSL is absent.
    /// </remarks>
    internal static class TSLUtil
    {
        private const string PARSE_TEXT_FRAGMENTS = "TSL.ParseTextFragments";

        public static string ParseTextFragments(this LocaleText text)
        {
            return ParseTextFragments(text.ToString());
        }

        public static string ParseTextFragments(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            return InteropAPI.Call(PARSE_TEXT_FRAGMENTS, input) as string ?? input;
        }
    }
}
