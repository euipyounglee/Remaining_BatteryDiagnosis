using System.Collections.Generic;
using System.Windows.Input;

namespace BatteryDashBoard
{
    public class MainModel : ViewModelBase
    {
        private int switchView;
        private int loginCmd;
        public int SwitchView
        {
            get
            {
                return switchView;
            }
            set
            {
                switchView = value;
                OnPropertyChanged("SwitchView");
            }
        }


        public int LoginCmd
        {
            get
            {
                return loginCmd;
            }
            set
            {
                loginCmd = value;
                OnPropertyChanged("LogInSwitchView");
            }
        }


        public ICommand SwitchViewCommand { get; }
        public ICommand LoginCommand { get; }

        public MainModel()
        {
            SwitchView = 0;
            LoginCmd = 0;

            SwitchViewCommand = new RelayCommand<object>(p => OnSwitchView(p));
            LoginCommand = new RelayCommand<object>(p => OnLoginView(p));
        }

        private void OnSwitchView(object index)
        {
            SwitchView = int.Parse(index.ToString());
        }

        private void OnLoginView(object index)
        {
            LoginCmd = int.Parse(index.ToString());
        }


        //public List<ChannelViewModel> Channels
        //{
        //    get
        //    {
        //        return SharedPreferences.Instance.ChannelVMs;
        //    }
        //}


    }
}
