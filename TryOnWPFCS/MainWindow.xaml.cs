using System;
using System.Windows;
using System.Windows.Controls;

namespace TryOnWPFCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string LoadFilePath;
        private string SaveFilePath;
        private const int MaxBytes = 200;

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void text_box_LFSR_TextChanged(object sender, TextChangedEventArgs e)
        {            
            var textBox = (TextBox)sender;
            string str = "";
            int caretIndex = textBox.CaretIndex;

            foreach (char item in textBox.Text)
                if (item == '1' || item == '0')
                    str += item;

            textBox.Text = str;
            textBox.CaretIndex = caretIndex;
        }
        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();          

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

        private string GetBits(byte[] bytes)
        {
            string bits = "";
            for (int i = 0; (i < MaxBytes) && (i < bytes.Length); i++)
            {
                string temp = Convert.ToString(bytes[i], 2);
                while (temp.Length < 8)
                    temp = '0' + temp;
                bits += temp;
            }
            return bits;
        }

        private void PrintBits(object obj)
        {
            var lfsr = (LFSR)obj;

            InitialFileBits.Text = GetBits(lfsr.InitialFile);
            KeyBits.Text = GetBits(lfsr.Key);
            CipherFileBits.Text = GetBits(lfsr.CipherFile);
        }

        private void Button_Click_LFSR(object sender, RoutedEventArgs e)
        {
            var lfsr = new LFSR();

            lfsr.Start(LFSRKey.Text, LoadFilePath);
            lfsr.SaveFile(SaveFilePath);

            PrintBits(lfsr);          
        }        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var geffe = new Geffe();
            geffe.Start(FirstKeyStream.Text, SecondKeyStream.Text, ThirdKeyStream.Text, LoadFilePath);
            geffe.SaveFile(SaveFilePath);

            PrintBits(geffe);
        }       
    }
}
