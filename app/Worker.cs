using System;
using System.Threading;
using System.Drawing;
using System.IO.Ports;

namespace app
{
    public sealed class Worker
    {
        private int executionCount = 0;
        private Timer _timer;

        public int width { get; set; }

        public int height { get; set; }


        public int length { get; set; }

        private SerialPort serial;


        private string _portName = null;

        public String PortName {
            set {
                if (serial != null) {
                    serial.Close();
                    serial = null;
                }
                _portName = value;
            }
        }


        private static readonly Lazy<Worker> lazy = new Lazy<Worker>(() => new Worker());

        public static Worker Instance { get { return lazy.Value; } }

        private Worker()
        {
            this.width = 36;
            this.height = 13;
            this.length = 60;
            _timer = new Timer(CaptureScaledScreen, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100.0));
        }



        private void CaptureScaledScreen(object state)
        {
            try
            {
                var count = Interlocked.Increment(ref executionCount);

                var size = new System.Drawing.Size((int)System.Windows.SystemParameters.WorkArea.Width, (int)System.Windows.SystemParameters.WorkArea.Height);



                using (var screenBmp = new Bitmap(size.Width, size.Height))
                {

                    var destRect = new Rectangle(0, 0, this.width, this.height);
                    var destImage = new Bitmap(this.width, this.height);

                    using (Graphics screenGraphics = Graphics.FromImage(screenBmp),
                     destGraphics = Graphics.FromImage(destImage)
                    )
                    {

                        screenGraphics.CopyFromScreen(new System.Drawing.Point(0, 0), new System.Drawing.Point(0, 0), size );

                        Bitmap32 bm32 = new Bitmap32(screenBmp);
                        bm32.LockBitmap();
                        Rectangle src_rect = Bitmap32.ImageBounds(bm32);
                        bm32.UnlockBitmap();



                        // Copy the non-white area.
                        int wid = src_rect.Width;
                        int hgt = src_rect.Height;
                        Bitmap bm = new Bitmap(wid, hgt);


                        using (Graphics gr = Graphics.FromImage(bm))
                        {
                            gr.Clear(Color.White);
                            Rectangle dest_rect = new Rectangle(
                                0, 0, src_rect.Width, src_rect.Height);
                            gr.DrawImage(screenBmp, dest_rect, src_rect, GraphicsUnit.Pixel);
                        }


                        destGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                        destGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                        //destGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                        {
                            wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                            destGraphics.DrawImage(bm, destRect, 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel, wrapMode);
                        }


                        //destImage.Save("test.png");
                    }


                    this.ExtractColors(destImage);

                }

            }
            catch (Exception)
            {

            }
        }


        private System.Drawing.Color trafo(System.Drawing.Color input)
        {


            double R = input.R;
            double G = input.G;
            double B = input.B;

            double sum = Math.Max(3.0 * 50.0, (R + G + B) * .4);
            R /= sum / 50.0;
            G /= sum / 50.0;
            B /= sum / 50.0;

            double addon = Math.Max(5.0, Math.Min(20.0, 20.0 - (R + G + B) / 3.0));

            R += addon;
            G += addon;
            B += addon;

            R *= 1.3;
            G *= 0.85;
            B *= 0.85;

            var output = System.Drawing.Color.FromArgb((int)R, (int)G, (int)B);

            return output;
        }



        private void ExtractColors(Bitmap img)
        {
            if (this.serial != null && this.serial.IsOpen)
            {
                var values = new byte[this.length * 3];

                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = 0x0;
                }

                int p = 0;

                for (int y = this.height - 1; y >= 0; y--)
                {
                    Color col = trafo(img.GetPixel(0, y));
                    values[p] = col.R;
                    p++;
                    values[p] = col.G;
                    p++;
                    values[p] = col.B;
                    p++;
                }

                for (int x = 1; x < this.width - 1; x++)
                {
                    Color col = trafo(img.GetPixel(x, 0));
                    values[p] = col.R;
                    p++;
                    values[p] = col.G;
                    p++;
                    values[p] = col.B;
                    p++;
                }


                for (int y = 0; y < this.height; y++)
                {
                    Color col = trafo(img.GetPixel(this.width - 1, y));
                    values[p] = col.R;
                    p++;
                    values[p] = col.G;
                    p++;
                    values[p] = col.B;
                    p++;
                }





                this.serial.Write(values, 0, values.Length);
            }
            else
            {
                //try to open serial for the next run
                this.serial = new SerialPort();
                this.serial.PortName = this._portName;
                this.serial.BaudRate = 115200;
                this.serial.Open();
            }
        }
    }
}