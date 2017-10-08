namespace CAI.Services
{
    using System;

    public class LanguageProcessinService
    {
        // Use this to keep digits. 
        private readonly char[] _delimitersKeepDigits = new char[] {
            '{', '}', '(', ')', '[', ']', '>', '<','-', '_', '=', '+',
            '|', '\\', ':', ';', ' ', ',', '.', '/', '?', '~', '!',
            '@', '#', '$', '%', '^', '&', '*', ' ', '\r', '\n', '\t' };

        // This will discard digits 
        private readonly char[] _delimitersNoDigits = new char[] {
            '{', '}', '(', ')', '[', ']', '>', '<','-', '_', '=', '+',
            '|', '\\', ':', ';', ' ', ',', '.', '/', '?', '~', '!',
            '@', '#', '$', '%', '^', '&', '*', ' ', '\r', '\n', '\t',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        ///  Tokenizes text into an array of words, using whitespace and
        ///  all punctuation as delimiters.
        /// </summary>
        /// <param name="text"> the text to tokenize</param>
        /// <returns> an array of resulted tokens</returns>
        public string[] GreedyTokenize(string text)
        {
            return text.Split(this._delimitersKeepDigits, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///  Tokenizes a text into an array of words, using whitespace and
        ///  all punctuation except the apostrophe "'" as delimiters. Digits
        ///  are handled based on user choice.
        /// </summary>
        /// <param name="text"> the text to tokenize</param>
        /// <param name="keepDigits"> true to keep digits; false to discard digits.</param>
        /// <returns> an array of resulted tokens</returns>
        public string[] Tokenize(string text, bool keepDigits)
        {
            string[] tokens = null;

            if (keepDigits)
                tokens = text.Split(this._delimitersKeepDigits, StringSplitOptions.RemoveEmptyEntries);
            else
                tokens = text.Split(this._delimitersNoDigits, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];

                // Change token only when it starts and/or ends with "'" and the 
                // toekn has at least 2 characters. 
                if (token.Length > 1)
                {
                    if (token.StartsWith("'") && token.EndsWith("'"))
                        tokens[i] = token.Substring(1, token.Length - 2); // remove the starting and ending "'" 

                    else if (token.StartsWith("'"))
                        tokens[i] = token.Substring(1); // remove the starting "'" 

                    else if (token.EndsWith("'"))
                        tokens[i] = token.Substring(0, token.Length - 1); // remove the last "'" 
                }
            }

            return tokens;
        }
    }
}
