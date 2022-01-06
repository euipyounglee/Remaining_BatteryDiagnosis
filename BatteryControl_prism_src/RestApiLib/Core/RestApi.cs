using RestApiLib.Data;
using RestApiLib.Defines;
using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestApiLib.Core
{
	public class RestApi
	{
		/// <summary>
		/// write log
		/// </summary>
		/// <param name="msg"></param>
		private static void WriteLog(RestApiLogTypes logType, string msg)
		{
			try
			{
				string path = $@"{RestApiConfig.Instance.LogFilePath}\log_{StringEnum.GetStringValue(logType)}_{string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)}.txt";
				using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path, true))
				{
					var now = DateTime.Now;
					sw.WriteLine($"[{string.Format("{0:D4}.{1:D2}.{2:D2} {3:D2}:{4:D2}:{5:D2}.{6:D3}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond)}] {msg}");
					sw.Flush();
					sw.Close();
					sw.Dispose();
				}
			}
			catch
			{
			}
		}

		/// <summary>
		/// 검사시작을 알려주는 API 호출
		/// </summary>
		/// <param name="dev_id"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool SendStartTest(RestApiLogTypes logType, string dev_id, int key)
		{
			return SendRequest_GET(logType, dev_id, key, "start");
		}

		/// <summary>
		/// 검사진행 및 상태정보 갱신을 알려주는 API 호출
		/// </summary>
		/// <param name="dev_id"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool SendUpdateState(RestApiLogTypes logType, string dev_id, int key)
		{
			return SendRequest_GET(logType, dev_id, key, "check");
		}

		/// <summary>
		/// 검사완료 및 검사결과 등록을 알려주는 API 호출
		/// </summary>
		/// <param name="logType"></param>
		/// <param name="dev_id"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool SendRegResult(RestApiLogTypes logType, string dev_id, int key)
		{
			return SendRequest_GET(logType, dev_id, key, "end");
		}

		/// <summary>
		/// 작업 수행단계.
		/// </summary>
		/// <param name="dev_id"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public static bool SendStep(RestApiLogTypes logType, string dev_id, int id)
		{
			return SendRequest_GET(logType, dev_id, id, "input");
		}

		/// <summary>
		/// Health Check 상태정보 전달.
		/// </summary>
		/// <param name="dev_id"></param>
		/// <returns></returns>
		public static bool SendHealthCheck(string dev_id)
		{
            //return SendRequest_GET(RestApiLogTypes.Common, dev_id, -1, "lfCheck");
            return true;
		}

		/// <summary>
		/// 공통 모듈.
		/// </summary>
		/// <param name="dev_id"></param>
		/// <param name="key"></param>
		/// <param name="uriCmd"></param>
		/// <returns></returns>
		private static bool SendRequest_GET(RestApiLogTypes logType, string dev_id, int key, string uriCmd)
		{
			bool result = false;

            if (RestApiConfig.Instance.PortBaseUri.Trim().Equals(""))
                return false;

			try
			{
				//System.Console.WriteLine("REST API Step 1: " + dev_id + "," + key + "," + uriCmd);
				//시스템 설정정보 읽어오기.
				//IfConfig ifConfig = (IfConfig)App.Current.Properties["g_ifConfig"];

				//System.Console.WriteLine("REST API Step 2: ");
				//입력정보의 유효성 검사.
				if (string.IsNullOrEmpty(dev_id)) return false;
				//if (key <= 0) return false;

				//System.Console.WriteLine("REST API Step 3: " + ifConfig.portBaseUri);
				//REST API 호출.
				HttpClient client = new HttpClient();
				client.Timeout = TimeSpan.FromSeconds(10);      //Timeout 10 sec.
				client.BaseAddress = new Uri(RestApiConfig.Instance.PortBaseUri);

				//System.Console.WriteLine("REST API Step 4: ");
				// JSON 형식에 대한 Accept 헤더를 추가합니다.
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//System.Console.WriteLine("REST API Step 5: ");
				string strURI = uriCmd + "/" + dev_id + "/" + key;
				if (key < 0)
					strURI = uriCmd + "/" + dev_id;

				//System.Console.WriteLine("REST API Step 6: " + strURI);
				WriteLog(logType, $"[Request] dev_id({dev_id}) key({key}) uriCmd({uriCmd}) fullURL({RestApiConfig.Instance.PortBaseUri}{strURI})");
				HttpResponseMessage response = client.GetAsync(strURI).Result;  // 호출 블록킹!
				if (response.IsSuccessStatusCode)
				{
					WriteLog(logType, $"[Response] {response.ToString()}");
					// 응답 본문 파싱. 블록킹!
					result = true;
					//Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
				}
				else
				{
					WriteLog(logType, $"[Response] StatusCode({(int)response.StatusCode}), ReasonPhrase({response.ReasonPhrase}), {response.ToString()}");
					result = false;
					Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
				}
				//작업결과 전달.
				//System.Console.WriteLine("REST API Step 7: ");
			}
			catch (Exception ex)
			{
				WriteLog(logType, $"[Error] REST API Exceiption({uriCmd}): {ex.Message}");
				if (uriCmd != "lfCheck")
				{
					System.Console.WriteLine("REST API Exceiption(" + uriCmd + "): " + ex.Message);
				}
			}

			return result;
		}
	}
}
