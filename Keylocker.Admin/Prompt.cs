using System;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Keylocker.Admin
{
	public static class Prompt
	{
		public static void DisplayMessageBox(string message, string caption)
		{
			MessageBoxButtons buttons = MessageBoxButtons.OK;
			MessageBox.Show(message, caption, buttons);
		}

		public static bool ForConfirmation(string caption)
		{
			var confirmationResult=MessageBox.Show("Changes to current key list will be lost", caption, MessageBoxButtons.OKCancel);
			return confirmationResult == DialogResult.OK;
		}

		public static string ForValue(string value, string name)
		{
			Form prompt = GetTextPrompt(value, name);
			prompt.ShowDialog();
			var textbox = prompt.Controls.Find("promptTextBox", false);
			return textbox[0].Text;
		}

		public static int ForValue(int value, string name)
		{
			Form prompt = GetTextPrompt(value.ToString(), name, true);
			prompt.ShowDialog();
			var textbox = prompt.Controls.Find("promptTextBox", false);
			string textValue = textbox[0].Text;
			int result = String.IsNullOrWhiteSpace(textValue) ? 0 : Int32.Parse(textValue);
			return result;
		}

		public static string ForOpenPath(string initialPath)
		{
			string openPath = null;
			OpenFileDialog openDialog = new OpenFileDialog
			{
				Title = "Open",
				DefaultExt = "bin",
				Filter = "encrypted files (*.bin)|*.bin|all files (*.*)|*.*",
				FilterIndex = 1,
				AddExtension = true,
				CheckPathExists = true,
				InitialDirectory = initialPath
			};
			DialogResult result = openDialog.ShowDialog();
			if(result == DialogResult.OK)
			{
				openPath = openDialog.FileName;
			}
			return openPath;
		}

		public static string ForSavePath(string initialPath)
		{
			string savePath = null;
			SaveFileDialog saveDialog = new SaveFileDialog
			{
				Title = "Save",
				DefaultExt = "bin",
				Filter = "encrypted files (*.bin)|*.bin|all files (*.*)|*.*",
				FilterIndex = 1,
				AddExtension = true,
				CheckPathExists = true,
				OverwritePrompt = true,
				InitialDirectory = initialPath
			};
			DialogResult result = saveDialog.ShowDialog();
			if(result == DialogResult.OK)
			{
				savePath = saveDialog.FileName;
			}
			return savePath;
		}

		public static Form GetTextPrompt(string text, string caption, bool integer = false, int maxLength = 999)
		{
			Form prompt = new Form
			{
				Width = 480,
				Height = 70,
				Text = caption,
			};
			TextBox textBox = new TextBox { Name = "promptTextBox", Left = 5, Top = 5, Width = 400, Text = text, MaxLength = maxLength};
			if(integer)
			{
				textBox.KeyPress += (sender, e) =>
				{
					// Key pressed can be a control or digit character
					if(Char.IsControl(e.KeyChar) || Char.IsDigit(e.KeyChar))
					{
						// Validate that FIRST DIGIT entered is not a zero
						if(Char.IsDigit(e.KeyChar))
						{
							// Do not allow 0 for the first digit
							var tb = sender as TextBox;
							if(tb.Text.Length == 0 && e.KeyChar == '0')
							{
								e.Handled = true;
							}
						}
					}
					else
					{
						e.Handled = true;
					}
				};
			}

			Button confirmation = new Button { Name = "promptOKButton", Text = "Ok", Left = 410, Top = 3, Width = 50 };
			confirmation.Click += (sender, e) => { prompt.Close(); };
			prompt.Controls.Add(confirmation);
			prompt.Controls.Add(textBox);
			prompt.AcceptButton = confirmation;
			return prompt;
		}
	}
}
