using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using KeyLocker;

namespace Keylocker.Admin
{
	public partial class MainForm : Form
	{
		byte[] _AESSalt;
		int _AESIterations;

		string _LockerPath;
		string _Password;
		bool _KeysChanged = false;
		BindingList<LockerKey> _KeysBindingSource = new BindingList<LockerKey>();

		public MainForm(string[] args)
		{
			this.Shown += FormShown;
			InitializeComponent();
			GetAppSettings();
			_KeysBindingSource.AllowEdit = true;
			_KeysBindingSource.ListChanged += KeyBindingSourceListChanged;
			keyDataGridView.DataSource = _KeysBindingSource;
			if(args.Length > 1)
			{
				_LockerPath = args[0];
				_Password = args[1];
			}
			ResolveKeys();
			SetMenuActive();
		}

		#region Menu Strip Events

		private void mainMenuFileNew_Click(object sender, EventArgs e)
		{
			bool ok = true;
			if(_KeysChanged)
			{
				ok = Prompt.ForConfirmation("Create New Key List");
			}
			if(ok)
			{
				_LockerPath = null;
				ResolveKeys();
			}
			SetMenuActive();
		}

		private void mainMenuFileOpen_Click(object sender, EventArgs e)
		{
			bool ok = true;
			if(_KeysChanged)
			{
				ok = Prompt.ForConfirmation("Create New Key List");
			}
			if(ok)
			{
				_LockerPath = Prompt.ForOpenPath(_LockerPath);
				if(!String.IsNullOrWhiteSpace(_LockerPath))
				{
					ResolveKeys();
				}
			}
			SetMenuActive();
		}

		private void mainMenuFileSave_Click(object sender, EventArgs e)
		{
			if(String.IsNullOrWhiteSpace(_LockerPath))
			{
				_LockerPath = Prompt.ForSavePath(_LockerPath);
			}
			if(!String.IsNullOrWhiteSpace(_LockerPath))
			{
				if(!String.IsNullOrWhiteSpace(_Password))
				{
					SaveLocker(_LockerPath, _Password);
					_KeysChanged = false;
				}
				else
				{
					Prompt.DisplayMessageBox("Password required for file save. Set password in options menu.", "Missing Password");
				}
			}
			SetMenuActive();
		}

		private void mainMenuFileSaveAs_Click(object sender, EventArgs e)
		{
			_LockerPath = Prompt.ForSavePath(_LockerPath);
			if(!String.IsNullOrWhiteSpace(_LockerPath))
			{
				if(!String.IsNullOrWhiteSpace(_Password))
				{
					SaveLocker(_LockerPath, _Password);
					_KeysChanged = false;
				}
				else
				{
					Prompt.DisplayMessageBox("Password required for file save. Set password in options menu.", "Missing Password");
				}
			}
			SetMenuActive();
		}

		private void mainMenuFileExit_Click(object sender, EventArgs e)
		{
			bool ok = true;
			if(_KeysChanged)
			{
				ok = Prompt.ForConfirmation("Create New Key List");
			}
			if(ok)
			{
				Close();
			}
		}

		private void mainMenuOptionsPassword_Click(object sender, EventArgs e)
		{
			_Password = Prompt.ForValue(_Password, "Password");
		}

		private void mainMenuOptionsSalt_Click(object sender, EventArgs e)
		{
			string result = Prompt.ForValue(_AESSalt.ValueToString(), "Salt");
			_AESSalt = result.ToBytes();
		}

		private void mainMenuOptionsIterations_Click(object sender, EventArgs e)
		{
			_AESIterations = Prompt.ForValue(_AESIterations, "Password");
		}

		#endregion

		#region Form Events

		private void buttonAddKey_Click(object sender, EventArgs e)
		{
			_KeysBindingSource.AddNew();
			keyDataGridView.BeginEdit(false);
			SetMenuActive();
		}

		private void buttonDeleteKey_Click(object sender, EventArgs e)
		{
			var row = keyDataGridView.CurrentRow;
			if(row != null)
			{
				string keyName = row.Cells[0].Value as string;
				var rowToDelete = _KeysBindingSource.FirstOrDefault(o => o.Key == keyName);
				_KeysBindingSource.Remove(rowToDelete);
			}
			SetMenuActive();
		}

		private void FormShown(object sender, EventArgs e)
		{
			if(String.IsNullOrWhiteSpace(_Password))
			{
				_Password = Prompt.ForValue(_Password, "Password");
			}
		}

		public void KeyBindingSourceListChanged(object sender, ListChangedEventArgs args)
		{
			_KeysChanged = true;
		}

		#endregion

		private void GetAppSettings()
		{
			_AESSalt = ConfigurationManager.AppSettings["AESSalt"].ToBytes();
			_AESIterations = Int32.Parse(ConfigurationManager.AppSettings["AESIterations"]);
		}

		private void SetMenuActive()
		{
			mainMenuFileSave.Enabled = _KeysChanged;
			//mainMenuFileSaveAs.Enabled = _KeysChanged;
		}

		public void ResolveKeys()
		{
			_KeysBindingSource.Clear();
			if(!String.IsNullOrWhiteSpace(_LockerPath))
			{
				List<LockerKey> keys = GetLockerKeys(_LockerPath, _Password);
				foreach(var key in keys)
				{
					_KeysBindingSource.Add(key);
				}
			}
			keyDataGridView.Columns[0].MinimumWidth = 250;
			keyDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			toolStripStatusLabel.Text = _LockerPath;
			statusStripMain.Refresh();
			_KeysChanged = false;
		}

		public List<LockerKey> GetLockerKeys(string lockerPath, string password)
		{
			Locker<List<LockerKey>> locker = GetNewLocker(password, lockerPath);
			locker.Open();
			return locker.Keys;
		}

		public Locker<List<LockerKey>> GetNewLocker(string password, string filePath = null)
		{
			AESEncryptor encryptor = new AESEncryptor(_AESSalt, _AESIterations);
			GenericBinarySerializer<List<LockerKey>> serializer = new GenericBinarySerializer<List<LockerKey>>();
			return new Locker<List<LockerKey>>(encryptor, serializer, password, filePath);
		}

		public void SaveLocker(string lockerPath, string password)
		{
			Locker<List<LockerKey>> locker = GetNewLocker(password, lockerPath);
			if(File.Exists(lockerPath))
			{
				locker.Open();
				locker.Keys.Clear();
			}
			foreach(LockerKey key in _KeysBindingSource)
			{
				locker.Keys.Add(key);
			}
			locker.Save();
			toolStripStatusLabel.Text = _LockerPath;
			statusStripMain.Refresh();
			Prompt.DisplayMessageBox("Keys Saved", "Save Keys");
		}
	}
}
