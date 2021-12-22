using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using log4net;


namespace BatteryDaemon
{
    class ClassCommand
    {

        static ClassCommand _instance;
        private static ILog log = LogManager.GetLogger("Program");

        public ClassCommand()
        {
            Console.WriteLine("초기화");
        }

        public static ClassCommand getInstance()
        {
            if (null == _instance)
            {
                _instance = new ClassCommand();
            }

            return _instance;
        }
        public int funcCommand(string strData)
        {

            if (string.IsNullOrWhiteSpace(strData)) { return -1; }

            if ("" != strData)
            {
                string json1 = strData;// JSONdata;
                if (7 == json1.Length && json1.ToLower() == "success")
                {
                    return 1;// true;
                }

                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

                dynamic dict = null;
                try
                {
                    dict = jsonSerializer.Deserialize<dynamic>(json1);
                }
                catch (Exception ex)
                {
                    //1. 문자열이 JSON 인지 파악 필요함.
                    Console.WriteLine(ex.Message);
                    string strErr = string.Format("{0}\n{1}", ex.Message, json1);
                    //MessageBox.Show(strErr, "error:Func");

                    log.Debug("Json Format Exception" + strErr);

                    //log4net.Layout
                    return -1;// false;
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                 
                foreach (var pair in dict)
                {

                    var value = pair.Value;
                    string nameKey = (string)pair.Key;
                    if ("daemon" == nameKey) return 0;

                    if (data.ContainsKey(nameKey))
                    {
                        data[nameKey] = (string)value;

                        Console.WriteLine("key:" + nameKey);
                    }else if("request" == nameKey || "request" == nameKey)
                    {
                        try
                        {
                            data = value;
                            break;
                        }catch(Exception ex)
                        {
                            Console.WriteLine("Err", ex.Message, value);
                            return -1;
                        }
                    }


                    //data[nameKey] = (string)value;

                    Console.WriteLine("key:" + nameKey);
                }


                foreach (KeyValuePair<string, object> item in data)
                {
                    Console.WriteLine("[{0}:{1}]", item.Key, item.Value);

                    string name = item.Key;
                    string nameValue = (string) item.Value;
                    if ("RleayCOM" == nameValue)//item.Key)
                    {

                        dynamic port = "COM3";// 3456;// dict["port"]; // result is Dictio

                        Console.WriteLine("port", port);
                        PythonClass.ConnectFuncCallUSB2CAN(port);// "COM7");
                        break;
                    }
                    else if ("Multimeter" == nameValue)
                    {
                        dynamic ipadress = "169.254.4.61"; // 장비 고정 IP

                        dynamic port = 5025; //장비의 고정 포트


                        string vIpadress = string.Format("{0}:{1}", ipadress, port);

                        string strBuffer = PythonClass.ConnectFuncCallTCP(vIpadress);

                        string Value = strBuffer.Replace("\r\n", "");
                        Value = strBuffer.Replace("\n", "");

                        string JsonString = "{";
                        JsonString += string.Format("\"name\" : \"{0}\"", name);
                        JsonString += string.Format(",\"value\" : \"{0}\"", Value);
                        JsonString += "}";


                        SendData(JsonString);
                        break;
                    }

                }


            }


            return 0;// false;

        }

        private void SendData(string strSendData)
        {
            //값 전송하기
            webSocketSharpClient.getInstance().Send(strSendData);

            Console.WriteLine("값 전송...:", strSendData);
        }

    }
}
