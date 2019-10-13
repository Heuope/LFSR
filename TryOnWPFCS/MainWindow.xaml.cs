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
        private int MaxBytes = 200;

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void text_box_LFSR_TextChanged(object sender, TextChangedEventArgs e)
        {            
            var _text = (TextBox)sender;
            string _str = "";
            int _temp = _text.CaretIndex;

            foreach (var item in _text.Text)
                if (item == '1' || item == '0')
                    _str += item;

            _text.Text = _str;
            _text.CaretIndex = _temp;
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
            LFSR.Start(LFSR_Key.Text, LoadFilePath);
            LFSR.SaveFile(SaveFilePath);

            StartBits.Text = "";
            KeyBits.Text = "";
            FinishBits.Text = "";

            string temp;
            for (int i = 0; (i < MaxBytes) && (i < LFSR.InitialFile.Length); i++)
            {
                temp = Convert.ToString(LFSR.InitialFile[i], 2);
                while (temp.Length < 8)
                    temp = '0' + temp;
                StartBits.Text += temp;
            }
            

            for (int i = 0; (i < MaxBytes) && (i < LFSR.Key.Length); i++)
            {
                temp = Convert.ToString(LFSR.Key[i], 2);
                while (temp.Length < 8)
                    temp = '0' + temp;
                KeyBits.Text += temp;
            }

            for (int i = 0; (i < MaxBytes) && (i < LFSR.CipherFile.Length); i++)
            {
                temp = Convert.ToString(LFSR.CipherFile[i], 2);
                while (temp.Length < 8)
                    temp = '0' + temp;
                FinishBits.Text += temp;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Geffe.Start(First.Text, Second.Text, Third.Text, LoadFilePath);
            Geffe.SaveFile(SaveFilePath);

            StartBits.Text = "";
            KeyBits.Text = "";
            FinishBits.Text = "";


            string temp = "";
            for (int i = 0; (i < MaxBytes) && (i < Geffe.InitialFile.Length); i++)
            {
                temp = Convert.ToString(Geffe.InitialFile[i], 2);
                while (temp.Length < 8)
                    temp = '0' + temp;
                StartBits.Text += temp;
            }           

            for (int i = 0; (i < MaxBytes) && (i < Geffe.Key.Length); i++)
            {
                temp = Convert.ToString(Geffe.Key[i], 2);
                while (temp.Length < 8)
                    temp = '0' + temp;
                KeyBits.Text += temp;
            }

            for (int i = 0; (i < MaxBytes) && (i < Geffe.CipherFile.Length); i++)
            {
                temp = Convert.ToString(Geffe.CipherFile[i], 2);
                while (temp.Length < 8)
                    temp = '0' + temp;
                FinishBits.Text += temp;
            }
        }

       
    }
}
