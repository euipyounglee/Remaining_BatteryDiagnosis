//using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryDashBoard.Data.VM
{
	public class QuestionVM // : BindableBase
	{
		#region property

		/// <summary>
		/// yes or no
		/// </summary>
		private bool m_IsChecked;
		public bool IsChecked
		{
			get
			{
				return m_IsChecked;
			}
			set
			{
				m_IsChecked = value;
				//RaisePropertyChanged("IsChecked");
			}
		}

		/// <summary>
		/// main question
		/// </summary>
		private string m_Message;
		public string Message
		{
			get
			{
				return m_Message;
			}
			private set
			{
				m_Message = value;
				//RaisePropertyChanged("Message");
			}
		}

		/// <summary>
		/// has input box
		/// </summary>
		private bool m_HasInputBox;
		public bool HasInputBox
		{
			get
			{
				return m_HasInputBox;
			}
			set
			{
				m_HasInputBox = value;
				//RaisePropertyChanged("HasInputBox");
			}
		}

		/// <summary>
		/// input box message
		/// </summary>
		private string m_InputBoxMessage;
		public string InputBoxMessage
		{
			get
			{
				return m_InputBoxMessage;
			}
			private set
			{
				m_InputBoxMessage = value;
				//RaisePropertyChanged("InputBoxMessage");
			}
		}

		#endregion

		#region method

		public QuestionVM(string msg, bool hasInput = false, string inputMsg = "")
		{
			IsChecked = false;
			Message = msg;
			HasInputBox = hasInput;
			InputBoxMessage = inputMsg;
		}

		#endregion
	}
}
