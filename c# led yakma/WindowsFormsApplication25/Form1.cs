using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision;
using AForge.Vision.Motion;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
namespace WindowsFormsApplication25
{
    public partial class Form1 : Form
    {
        int mode;
        int derlemezamani;
        int Red, Green, Blue;
        int objectX;
        int objectY;
        int x_koordinat;
        int y_koordinat;
        Graphics g;
        Bitmap video;
        private FilterInfoCollection webcamsayisi;   //webcam isminde tanımladığımız değişken bilgisayara kaç kamera bağlıysa onları tutan bir dizi.
        private VideoCaptureDevice kamera;   //cam ise bizim kullanacağımız aygıt.
        bool LEDdurumu = false;
        SerialPort seriPort;
        public Form1()
        {
            InitializeComponent();
            seriPort = new SerialPort();
            seriPort.PortName = "COM8";
            seriPort.BaudRate = 9600;
            seriPort.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webcamsayisi = new FilterInfoCollection(FilterCategory.VideoInputDevice); //webcam dizisine mevcut kameraları dolduruyoruz.
            foreach (FilterInfo videocapturedevice in webcamsayisi)
            {
                comboBox1.Items.Add(videocapturedevice.Name);  //kameraları combobox a dolduruyoruz.
                comboBox1.SelectedIndex = 0;
            }
            
            kamera = new VideoCaptureDevice();
        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            kamera = new VideoCaptureDevice(webcamsayisi[comboBox1.SelectedIndex].MonikerString);
            kamera.NewFrame += new NewFrameEventHandler(kullanilacakcihaz_NewFrame);
            kamera.Start();
        }
        private void kullanilacakcihaz_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            video = (Bitmap)eventArgs.Frame.Clone();
            Bitmap video2 = (Bitmap)eventArgs.Frame.Clone();
            switch(mode)
            {
                
                case 1:
                    {
                //
                ColorFiltering colorfiller = new ColorFiltering();
                colorfiller.Red = new IntRange(Red, (int)numericUpDown1.Value);
                colorfiller.Green = new IntRange(Green, (int)numericUpDown2.Value);
                colorfiller.Blue = new IntRange(Blue,(int)numericUpDown3.Value);
                colorfiller.ApplyInPlace(video2);
                // 
                BlobCounter blobcounter = new BlobCounter();
                blobcounter.MinHeight = 20;
                blobcounter.MinWidth = 20;
                blobcounter.ObjectsOrder = ObjectsOrder.Size;
                blobcounter.ProcessImage(video2);
                Rectangle[] rect = blobcounter.GetObjectsRectangles();
                if(rect.Length>0)
                {
                    Rectangle objec = rect[0];
                    Graphics graphic = Graphics.FromImage(video2);
                    using (Pen pen = new Pen(Color.White, 3))
                    {
                        graphic.DrawRectangle(pen, objec);
                        g = Graphics.FromImage(video);
                        g.DrawRectangle(pen, objec);
                        objectX = objec.X + (objec.Width / 2);
                        objectY = objec.Y + (objec.Height / 2);
                        g.DrawString(objectX.ToString() + "X" + objectY.ToString(), new Font("Arial", 12), Brushes.Red, new System.Drawing.Point(1, 1));
                        g.Dispose();
                              
                      }
                    graphic.Dispose();
                }
                
            }
                    break;
            }
           
            pictureBox1.Image = video;
            
        }
      
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(kamera.IsRunning==true)
            {
                kamera.SignalToStop();
                kamera = null;
            }
            if (seriPort.IsOpen) seriPort.Close();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            x_koordinat = objectX;
            y_koordinat = objectY;
            label4.Text = x_koordinat.ToString() + "X" + y_koordinat.ToString();

            if (x_koordinat < 215 && y_koordinat < 150)
            {
                label5.Text = "3.led yandı";
                seriPort.Write("3");

            }
            else if (x_koordinat < 430 && y_koordinat < 150)
            {
                label5.Text = "2.led yandı";
                seriPort.Write("2");
            }
            else if (x_koordinat < 650 && y_koordinat < 150)
            {
                label5.Text = "1.led yandı";
                seriPort.Write("1");
            }
            else if (x_koordinat < 215 && y_koordinat < 325)
            {
                label5.Text = "6.led yandı";
                seriPort.Write("6");
            }
            else if (x_koordinat < 430 && y_koordinat < 325)
            {
                label5.Text = "5.led yandı";
                seriPort.Write("5");
            }
            else if (x_koordinat < 650 && y_koordinat < 325)
            {
                label5.Text = "4.led yandı";
                seriPort.Write("4");
            }
            else if (x_koordinat < 215 && y_koordinat < 500)
            {
                label5.Text = "9.led yandı";
                seriPort.Write("9");
            }
            else if (x_koordinat < 430 && y_koordinat < 500)
            {
                label5.Text = "8.led yandı";
                seriPort.Write("8");
            }
            else if (x_koordinat < 650 && y_koordinat < 500)
            {
                label5.Text = "7.led yandı";
                seriPort.Write("7");
            }
            else if (x_koordinat > 650 && y_koordinat > 500)
            {
                label5.Text = "led yanmadı";
                
            }
            else if (x_koordinat <0 && y_koordinat <0)
            {
                label5.Text = "led yanmadı";

            }
            else label5.Text = "led yanmadı";

        }
       
        

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Red = trackBar1.Value;
         
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Green =trackBar2.Value;
            
        }

       
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Blue = trackBar3.Value;
        }

        

       

        private void button3_Click(object sender, EventArgs e)
        {
            mode = 1;
            timer1.Start();
 

        }
    }

}