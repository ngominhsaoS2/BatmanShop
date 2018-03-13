using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class AccDocHelp
    {
        public static string CreateDocNo(string docCode, long id)
        {
            string result = id.ToString();
            int len = result.Length;
            for (int i = len; i < 14; i++)
            {
                result = "0" + result;
            }
            result = docCode + result;
            return result;
        }







    }
}
