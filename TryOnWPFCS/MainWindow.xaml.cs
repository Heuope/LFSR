using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TryOnWpfCs
{
    internal partial class MainWindow
    {
        private string _loadFilePath = string.Empty;
        private string _saveFilePath = string.Empty;
        private const int MaxBytes = 200;

        public MainWindow() => InitializeComponent();

        private void text_box_LFSR_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox) sender;
            var caretIndex = textBox.CaretIndex;
            textBox.Text = string.Concat(textBox.Text.Where(x => x == '1' || x == '0'));
            textBox.CaretIndex = caretIndex;
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            var result = dialog.ShowDialog();

            if (result == true)
            {
                _loadFilePath = dialog.FileName;
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            var result = dialog.ShowDialog();

            if (result == true)
            {
                _saveFilePath = dialog.FileName;
            }
        }

        private static string GetBits(IEnumerable<byte> bytes) =>
            string.Concat(
                bytes
                    .Take(MaxBytes)
                    .Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

        private void PrintBits(IFieldsCipher cipher)
        {
            InitialFileBits.Text = GetBits(cipher.InitialFile);
            KeyBits.Text = GetBits(cipher.Key);
            CipherFileBits.Text = GetBits(cipher.CipherFile);

            if (!(cipher is IGeffe geffe)) return;

            First.Text = GetBits(geffe.FirstKey);
            Second.Text = GetBits(geffe.SecondKey);
            Third.Text = GetBits(geffe.ThirdKey);
        }

        private void Button_Click_LFSR(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_saveFilePath) || string.IsNullOrWhiteSpace(_loadFilePath) || LFSRKey.Text.Length != 29)
                return;

            var lfsr = Lfsr.Create(LFSRKey.Text, _loadFilePath, _saveFilePath);
            PrintBits(lfsr);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_saveFilePath) ||
                string.IsNullOrWhiteSpace(_loadFilePath) ||
                FirstKeyStream.Text.Length != 29 ||
                SecondKeyStream.Text.Length != 27 ||
                ThirdKeyStream.Text.Length != 37)
                return;

            var geffe = Geffe.Create(FirstKeyStream.Text, SecondKeyStream.Text, ThirdKeyStream.Text, _loadFilePath, _saveFilePath);
            PrintBits(geffe);
        }
    }
}
