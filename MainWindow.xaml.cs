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
using Microsoft.Win32;
using System.IO;

namespace WpfHelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool saved = false;
        bool edited = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            saved = false;
            if (!saved && !edited)
            {
                Label.Content += "*";
                edited = true;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            savefile();
        }

        private void openbutton_Click(object sender, RoutedEventArgs e)
        {

            if (!saved)
            {
                var Result = MessageBox.Show("Your content will not be saved. Do you want to run it?","OpenFile",MessageBoxButton.YesNoCancel,MessageBoxImage.Warning);

                switch(Result)
                {
                    case MessageBoxResult.Yes:
                        openfile();
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            else if(saved)
            {
                openfile();
            }
        }

        public void savefile()
        {
            var dialog = new SaveFileDialog();

            dialog.FileName = TextBox.Text;
            dialog.Title = "保存するファイル名を入力してください。";
            dialog.Filter = "テキストファイル (*.txt)|*.txt|CSVファイル(*.csv)|*.csv|全てのファイル (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                var encoding = Encoding.GetEncoding("utf-8");

                try
                {
                    File.WriteAllText(filename, TextBox.Text, encoding);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                //MessageBox.Show(filename);
                Title = "TextNote";
                Label.Content = filename;
                saved = true;
                edited = false;
            }
        }
        private void openfile()
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "開くファイルを選択してください";
            dialog.Filter = "テキストファイル(*.txt)|*.txt|全てのファイル(*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                var encoding = Encoding.GetEncoding("utf-8");

                try
                {
                    TextBox.Text = File.ReadAllText(filename, encoding);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                //MessageBox.Show(filename);
                Title = "TextNote";
                Label.Content = filename;
                saved = true;
                edited = false;
            }
        }
    }
}
