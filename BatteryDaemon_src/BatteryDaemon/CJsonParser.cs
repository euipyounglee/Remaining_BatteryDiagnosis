using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Reflection;

namespace BatteryDaemon
{
    class CJsonParser
    {

        public void Sampleload()
        {
#if true
            string json = @"{
                'Email': 'james@example.com',
                'Active': true,
                'CreatedDate': '2013-01-20T00:00:00Z',
                'Roles': [
                { 'User':'111','Admin':'123'}

                ]}";
#endif
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(json);
        }

        string _strAppConfig;
        public CJsonParser()
        {
            string strApp = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);

            string strAppConfig = strApp + "_config.json";

            _strAppConfig = strAppConfig;

        }

        //포트 알아내기
        public string jsonMenuParsing(string jsonDir, string keyvalue)
        {
            string strReuslt = "";
            //실행파일명만 얻어오기
            string strAppConfig = _strAppConfig;
            string strJsonPath = String.Format("{0}\\{1}", jsonDir, strAppConfig);

            if (File.Exists(strJsonPath))
            {
                using (StreamReader r = new StreamReader(strJsonPath))
                {
                    string json = r.ReadToEnd();

                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic dobj = jsonSerializer.Deserialize<dynamic>(json);

                    try
                    {
                        //object result1 = dobj["menu"][0]["setting"];
                        object result1 = dobj["menu"][0][keyvalue];

                        if (result1.ToString() != "")
                        {
                            strReuslt = result1.ToString();
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                }
            }

            return strReuslt;
        }


        public int getConfigjsonInt32(string key)
        {

            string strRoot = System.Environment.CurrentDirectory;
            string strValue = jsonParsing(strRoot, key);
            int port = Int32.Parse(strValue);
            return port ;

        }

        public dynamic getObject(string key)
        {
            dynamic dobjResult = null;// jsonGetObject(key);

            string strReuslt = "";

            //실행파일명만 얻어오기
            string strJsonPath = _strAppConfig;
            if (File.Exists(strJsonPath))
            {
                using (StreamReader r = new StreamReader(strJsonPath))
                {
                    string json = r.ReadToEnd();

                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic dobj = jsonSerializer.Deserialize<dynamic>(json);

                    try
                    {
                        dynamic jValue = dobj[key];
                        dobjResult = jValue;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        dobjResult =null;
                    }

                }
            }



            return dobjResult;
        }


        public dynamic getObject(string strJsonData,string key)
        {
            dynamic dobjResult = null;// jsonGetObject(key);

            string strReuslt = "";

            //실행파일명만 얻어오기
            string strJsonPath = _strAppConfig;
            string json = strJsonData;// r.ReadToEnd();

             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             dynamic dobj = jsonSerializer.Deserialize<dynamic>(json);

             try
             {
                   dynamic jValue = dobj[key];
                   dobjResult = jValue;
             }
             catch (Exception ex)
             {
                   Console.WriteLine(ex.ToString());
                   dobjResult = null;
             }




            return dobjResult;
        }


        public bool getConfigjsonBoolean(string key)
        {
            bool result = false;
            string strRoot = System.Environment.CurrentDirectory;
            string strValue = jsonParsing(strRoot, key);
            if ("" != strValue)
            {
                result = Boolean.Parse(strValue);
            }
            return result;
        }

        public string getConfigjsonParsing( string key)
        {

            string strRoot = System.Environment.CurrentDirectory;
            string strValue =  jsonParsing(strRoot, key);

            return strValue;
        }
       // public jsonGetObject

        //포트 알아내기
        public string jsonParsing (string jsonDir, string key)
        {
            string strReuslt = "";

            //실행파일명만 얻어오기
            string strJsonPath = _strAppConfig;// String.Format("{0}\\{1}", jsonDir, strAppConfig);

            if (File.Exists(strJsonPath))
            {
                using (StreamReader r = new StreamReader(strJsonPath))
                {
                    string json = r.ReadToEnd();

                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    dynamic dobj = jsonSerializer.Deserialize<dynamic>(json);

                    try
                    {
                        dynamic jValue = dobj[key];
                        //int result = (int)jValue;
                      //  string strString = jValue;

                        if (jValue.ToString() != "") {
                            strReuslt = jValue.ToString();
                        }


                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                  
                }
            }

                return strReuslt;
        }


        public string jsonDataParsing(string jsonData, string key)
        {
            string strReuslt = "";

            //실행파일명만 얻어오기

            string json = jsonData;// r.ReadToEnd();

             JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
             dynamic dobj = jsonSerializer.Deserialize<dynamic>(json);

             try
             {
                  dynamic jValue = dobj[key];

                  if (jValue.ToString() != "")
                  {
                     strReuslt = jValue.ToString();
                  }


             }
             catch (Exception ex)
             {
                  Console.WriteLine(ex.ToString());
             }


            return strReuslt;
        }


    }//class

}
