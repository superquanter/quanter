using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Common
{
    public class StockUtil
    {
        private static Random random = new Random();
        public static string getCode(String code)
        {
            if (code.Length == 6)
                return code;
            else if (code.Length == 8)
                return code.Substring(2);

            return code;
        }

        public static string GetFullCode(String stockCode)
        {
            if (stockCode.Length == 6)
            {
                switch (stockCode.Substring(0, 2))
                {
                    case "51":
                    case "50":
                    case "60":
                        stockCode = "sh" + stockCode;
                        return stockCode;

                    case "00":
                    case "15":
                    case "16":
                    case "30":
                        stockCode = "sz" + stockCode;
                        return stockCode;
                }
            }
            return stockCode;
        }

        public static int GetExchangeType(String code)
        {
            if (GetFullCode(code).StartsWith("sz")) return 2;
            else if (GetFullCode(code).StartsWith("sh")) return 1;

            return 0;
        }

        public static string Base64Encode(String s, Encoding e)
        {
            byte[] bytes = e.GetBytes(s);

            return Convert.ToBase64String(bytes);
        }

        public static string Base64Decode(String s, Encoding e)
        {
            byte[] bytes = Convert.FromBase64String(s);
            return e.GetString(bytes);
        }

        public static string RandomString
        {
            get
            {
                return random.Next().ToString();
            }
        }

        public static string GetFundCode(string code)
        {
            if (code.Length == 6)
                return "f_" + code;
            else if (code.Length == 8)
                return "f_" + code.Substring(2, 6);
            else return code;
        }

        public static string GetShortCode(string code)
        {
            switch (code.Length)
            {
                case 6:
                    return code;
                case 7:
                    return code.Substring(1);
                case 8:
                    return code.Substring(2);
                default:
                    return code;
            }
        }
    }
}
