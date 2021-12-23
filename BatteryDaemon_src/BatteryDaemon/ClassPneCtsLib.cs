//using PneCtsLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryGateway
{
    class ClassPneCtsLib
    {

        public bool connect()
        {
            var result = "false";

            try
            {
                //Core Devices = new Core();
                //Devices.Connect();

            }catch(Exception ex)
            {
               result = "false" + ex.Message;
               MessageBox.Show(ex.Message, "Connect Error");
            }

            Console.WriteLine("PNE 결과:{0}", result);
            return false;// result;
        }
    }
}
