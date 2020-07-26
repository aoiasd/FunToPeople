using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FunToPeople
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		FtpClient ftpClient = new FtpClient("localhost");
		private void BindData()
		{
			StatusListBox.ItemsSource = CommonData.statusList;
			LocalFileListBox.ItemsSource = CommonData.localFileList;
			RemoteFileListBox.ItemsSource = CommonData.remoteFileList;
			TextBox_LocalPath.Text = CommonData.Local_Path;
		}
		public MainWindow()
		{
			InitializeComponent();
			BindData();
			ftpClient.Connect("yah01", "FTPSERVER01");
			ftpClient.FreshFileList();
			ftpClient.FreshLocalFileList();
		}

		private void LocalFileListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (LocalFileListBox.SelectedIndex != -1&& LocalFileListBox.SelectedIndex <CommonData.Local_Folder&& LocalFileListBox.IsMouseOver&&e.ChangedButton==MouseButton.Left)
			{
				string file = LocalFileListBox.SelectedItem as string;
				if (LocalFileListBox.SelectedIndex > 0)
					CommonData.Local_Path += "\\" + file;
				else
				{
					string[] temp = Regex.Split(CommonData.Local_Path, @"\\");
					CommonData.Local_Path = "";
					for (int i = 0; i < temp.Length - 1; i++) CommonData.Local_Path += temp[i];
					//CommonData.Local_Path.Remove(CommonData.Local_Path.Length - 1- temp[temp.Length - 1].Length, temp[temp.Length - 1].Length);
					//CommonData.Local_Path += "\\";
				}
				TextBox_LocalPath.Text = CommonData.Local_Path;
				ftpClient.FreshLocalFileList();
			}
		}

		private void Button_Goto_Click(object sender, RoutedEventArgs e)
		{
			CommonData.Local_Path = TextBox_LocalPath.Text;
			ftpClient.FreshLocalFileList();
		}
	}
}
