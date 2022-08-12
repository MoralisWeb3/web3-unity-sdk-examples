namespace MoralisUnity.Examples.Sdk.Shared
{
    /// <summary>
    /// Provides runtime formatters
    /// </summary>
    public static class SharedFormatter
    {
        /// <summary>
        /// Returns a string of form "abc...xyz"
        /// <see cref="https://github.com/web3ui/web3uikit/blob/master/src/web3utils/formatters.ts"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string GetWeb3AddressShortFormat(string str, int a = 5, int b = 4)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
	        
            if (str.Length < a)
            {
                return str;
            }
            
            if (str.Length < b)
            {
                return str;
            }

            // The (string str, int a = 5, int b = 4) results in the same format as metamask which is like "0xd38...a7d4"
            return $"{str.Substring(0, a)}...{str.Substring(str.Length - 4)}";
        }
    }
}