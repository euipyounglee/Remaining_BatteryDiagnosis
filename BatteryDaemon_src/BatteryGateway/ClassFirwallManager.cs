using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;


namespace BatteryGateway
{

    class FirewallAppInfo
    {
        public bool mListAdded; // 방화벽 앱 목록에 추가되어있는지 여부
        public bool mEnabled;   // 방화벽 앱 목록에 '허용'으로 되어있는지 여부
    }

    class ClassFirwallManager
    {
        private const string CLSID_FIREWALL_MANAGER = "{304CE942-6E39-40D8-943A-B913C40C9CD4}";
        private INetFwMgr mFirewallMng = null;

        public ClassFirwallManager()
        {
            // firewall manager 객체 생성
            Type objectType = Type.GetTypeFromCLSID(new Guid(CLSID_FIREWALL_MANAGER));
            mFirewallMng = Activator.CreateInstance(objectType) as INetFwMgr;
        }

        public FirewallAppInfo getAppInfo(string appPathName)
        {
            INetFwAuthorizedApplication authoredApp = findApp(appPathName);

            FirewallAppInfo appInfo = new FirewallAppInfo();

            if (authoredApp == null)
            {
                appInfo.mListAdded = false;
                appInfo.mEnabled = false;
            }
            else
            {
                appInfo.mListAdded = true;
                appInfo.mEnabled = authoredApp.Enabled;
            }

            return appInfo;
        }

        private INetFwAuthorizedApplication findApp(string appPathName)
        {
            foreach (INetFwAuthorizedApplication app in mFirewallMng.LocalPolicy.CurrentProfile.AuthorizedApplications)
            {

                // 일치하는 앱을 찾음
                if (app.ProcessImageFileName.ToLower().Equals(appPathName.ToLower()))
                {
                    return app;
                }
            }

            return null;
        }

        // 프로그램 방화벽 예외 등록 시키기

        public bool setAuthorizeProgramRun(string title, string path)
        {

          return  AuthorizeProgram(title, path,
                NET_FW_SCOPE_.NET_FW_SCOPE_ALL, NET_FW_IP_VERSION_.NET_FW_IP_VERSION_ANY);

        }

        private  bool AuthorizeProgram(string title, string path, NET_FW_SCOPE_ scope, NET_FW_IP_VERSION_ ipver)
        {

            if (null != mFirewallMng)
            {
                Type type = Type.GetTypeFromProgID("HNetCfg.FwAuthorizedApplication");
                INetFwAuthorizedApplication authapp = Activator.CreateInstance(type) as INetFwAuthorizedApplication;
                authapp.Name = title;
                authapp.ProcessImageFileName = path;
                authapp.Scope = scope;
                authapp.IpVersion = ipver;
                authapp.Enabled = true;

                INetFwMgr mgr = mFirewallMng;//  WinFirewallManager();
                try
                {
                    mgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(authapp);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.Write(ex.Message);
                    return false;
                }
            }

            return true;
        }



    }
}
