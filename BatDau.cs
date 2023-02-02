using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Basic_game
{
    public partial class BatDau : Form
    {
        public BatDau()
        {
            InitializeComponent();
        }


        private void startgame_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = "C:/Users/Admin/Downloads/sunny-land-files/Sunny-land-assets-files/Soundplatformer_level03.mp3"; // Đường dẫn đến file cần chơi
            player.controls.play();
        }
    }
}
