using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryDaemon
{
    class ClassMultimeter : ClassTCPClient
    {

        public string ConnectTCP(string vIpadress)
        {
            //string strResult = "";
            Console.WriteLine("결과:{0}", vIpadress);
            string[] adress = vIpadress.Split(':');
            string strIP = "";
            int nPort = 0;
            if (adress.Length == 2)
            {
                strIP = adress[0];
                nPort = int.Parse(adress[1]);
            }

            string strRET = connectTry(strIP, nPort);

            return strRET;
        }
    }
}
