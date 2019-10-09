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

namespace TryOnWPFCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string LoadFilePath;
        private string SaveFilePath;

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void text_box_LFSR_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _text = (TextBox)sender;
            string _str = "";

            foreach (var item in _text.Text)
                if (item == '1' || item == '0')
                    _str += item;

            _text.Text = _str;
            _text.CaretIndex = _text.Text.Length;
        }
        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 2;

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
                LoadFilePath = dialog.FileName;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();            

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
                SaveFilePath = dialog.FileName;
        }

        private void Button_Click_LFSR(object sender, RoutedEventArgs e)
        {
            LFSR.MainPoint(LFSR_Key.Text, LoadFilePath, SaveFilePath);
            //StartBits.Text = LFSR.Text;
            //KeyBits.Text = LFSR.Key;
            //FinishBits.Text = LFSR.CipherText;
        }
    }
}
