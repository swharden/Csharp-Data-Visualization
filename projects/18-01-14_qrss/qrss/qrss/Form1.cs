using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using NAudio.Wave; // for sound card access
using NAudio.Dsp; // for fft

using System.Drawing;
using System.Drawing.Imaging; // for ImageLockMode
using System.Runtime.InteropServices; // for Marshal

namespace qrss
{
    public partial class Form1 : Form
    {        

        public Form1()
        {
            InitializeComponent();

            // populate combobox with available audio devices
            combo_device.Items.Clear();
            int device_number = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < device_number; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                combo_device.Items.Add(deviceInfo.ProductName);
            }
            if (combo_device.Items.Count>0) combo_device.SelectedIndex = 0;
            else MessageBox.Show("ERROR: no recording devices available");
            combo_rate.SelectedIndex = 0;

            // update the rest of the GUI
            gui_calculations_update();
        }









        /*
         * 
         * STARTING AND STOPPING RECORDING
         * 
         */

        public static WaveIn waveIn;

        private void button1_Click(object sender, EventArgs e)
        {
            gui_calculations_update();

            if (button1.Text == "start")
            {
                // INITIATING RECORDING

                // made GUI changes as necessary
                button1.Text = "stop";
                captured_buffers = 0;
                combo_device.Enabled = false;
                combo_rate.Enabled = false;
                nud_buffer_size.Enabled = false;
                nud_fft_size.Enabled = false;
                nud_fft_step_ms.Enabled = false;
                nud_spec_width.Enabled = false;

                // clear and initiate the incoming audio stream
                unanalyzed_audio = new List<short>();

                // pre-load important variables
                audio_rate = int.Parse(combo_rate.Text);

                // start the recording device
                waveIn = new WaveIn();
                waveIn.DataAvailable += Audio_buffer_captured;
                waveIn.DeviceNumber = 0;
                waveIn.WaveFormat = new NAudio.Wave.WaveFormat(audio_rate, 1);
                waveIn.BufferMilliseconds = audio_buffer_ms;
                waveIn.StartRecording();

                // start the auto-analysis timer
                timer1.Enabled = true;
            } else
            {
                // TERMINATING RECORDING

                // stop the recording device
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;

                // made GUI changes as necessary
                timer1.Enabled = false;
                button1.Text = "start";
                combo_device.Enabled = true;
                combo_rate.Enabled = true;
                nud_buffer_size.Enabled = true;
                nud_fft_size.Enabled = true;
                nud_fft_step_ms.Enabled = true;
                nud_spec_width.Enabled = true;
                pic_level_front.Height = pic_level_back.Height;
            }            
        }

        private int captured_buffers;
        private int audio_peak_value;
        List<short> unanalyzed_audio;

        private void Audio_buffer_captured(object sender, WaveInEventArgs args)
        {

            // add the new audio to the growing list of unanalyzed audio
            for (int i = 0; i < args.BytesRecorded; i += 2)
            {
                unanalyzed_audio.Add((short)((args.Buffer[i + 1] << 8) | args.Buffer[i + 0]));
            }

            // calculate envelope (peak value)
            int buffer_points = args.Buffer.Length / 2;
            List<short> recent_audio = unanalyzed_audio.GetRange(unanalyzed_audio.Count - buffer_points, buffer_points);
            int current_peak = recent_audio.Max();
            audio_peak_value = Math.Max(audio_peak_value, current_peak);

            // update the GUI
            captured_buffers += 1;
            lbl_buffers_captured.Text = captured_buffers.ToString();

            // the audio level meter is a little funnny. It's a red background with a resizing gray foreground.
            lbl_unanalyzed_sec.Text = string.Format("{0:F}", unanalyzed_audio.Count / (double)audio_rate);
            pic_level_front.Height = pic_level_back.Height - (int)(pic_level_back.Height * ((double)current_peak) / (double)audio_peak_value);

        }


        /*
         * 
         * BITMAP ROUTINES
         * 
         */

        private static List<List<double>> spec_data; // a list of column pixel values

        private void Data_init()
        {
            // fill "data" with 2d random data
            spec_data = new List<List<double>>();
            for (int x = 0; x < spectrograph_width_px; x++)
            {
                List<double> column = new List<double>();
                for (int y=0; y<spectrograph_height_px; y++)
                {
                    column.Add(0);
                }
                spec_data.Add(column);
            }
            System.Console.WriteLine("SPEC DATA: {0} x {1}", spec_data.Count, spec_data[0].Count);
        }

        void Update_bitmap_with_data()
        {
            // create a bitmap we will work with
            Bitmap bitmap = new Bitmap(spec_data.Count, spec_data[0].Count, PixelFormat.Format8bppIndexed);

            // modify the indexed color map
            switch (cmb_colormap.Text)
            {
                case "gray":
                    bitmap.Palette = pallette_gray(bitmap.Palette);
                    break;
                case "blue":
                    bitmap.Palette = pallette_blue(bitmap.Palette);
                    break;
                case "green":
                    bitmap.Palette = pallette_green(bitmap.Palette);
                    break;
                case "spectrum":
                    bitmap.Palette = pallette_spectrum(bitmap.Palette);
                    break;
                default:
                    bitmap.Palette = pallette_gray(bitmap.Palette);
                    break;
            }            

            // prepare to access data via the bitmapdata object
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                    ImageLockMode.ReadOnly, bitmap.PixelFormat);

            // create a byte array to reflect each pixel in the image
            byte[] pixels = new byte[bitmapData.Stride * bitmap.Height];

            // fill pixel array with data
            for (int col = 0; col < spec_data.Count; col++)
            {

                // I selected this manually to yield a number that "looked good"
                double scaleFactor = (double)nud_scale_factor.Value;

                for (int row = 0; row < spec_data[col].Count; row++)
                {
                    int bytePosition = row * bitmapData.Stride + col;
                    double pixelVal = spec_data[col][row] * scaleFactor;
                    pixelVal = Math.Max(0, pixelVal);
                    pixelVal = Math.Min(255, pixelVal);
                    pixels[bytePosition] = (byte)(pixelVal);
                }
            }

            // turn the byte array back into a bitmap
            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bitmap.UnlockBits(bitmapData);

            // apply the bitmap to the picturebox
            pictureBox1.Image = bitmap;
        }
        
        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.FileName = "capture.bmp";
            d.ShowDialog();
            pictureBox1.Image.Save(d.FileName);
        }

        /*
         * 
         * CUSTOM COLORMAPS
         * 
         */

        ColorPalette pallette_gray(ColorPalette pal)
        {
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = Color.FromArgb(255, i, i, i);
            return pal;
        }

        ColorPalette pallette_blue(ColorPalette pal)
        {
            for (int i = 0; i < 256; i++)
            {
                int rampFirst = Math.Min(255, (i * 2));
                int rampLast = Math.Max(0, (i - 128) * 2);
                pal.Entries[i] = Color.FromArgb(255, rampLast, i, rampFirst);
            }
            return pal;
        }

        ColorPalette pallette_green(ColorPalette pal)
        {
            for (int i = 0; i < 256; i++)
            {
                int rampFirst = Math.Min(255, (i * 2));
                int rampLast = Math.Max(0, (i - 128) * 2);
                pal.Entries[i] = Color.FromArgb(255, rampLast, rampFirst, i);
            }
            return pal;
        }

        ColorPalette pallette_spectrum(ColorPalette pal)
        {
            int r, g, b;
            for (int i = 0; i < 256; i++)
            {
                r = i * 4 - 128 * 4;
                b = 128 * 2 - i * 4;
                g = i * 4;
                if (i > 128) g = 256 * 4 - g;

                r = Math.Max(0, Math.Min(255, r));
                g = Math.Max(0, Math.Min(255, g));
                b = Math.Max(0, Math.Min(255, b));
                pal.Entries[i] = Color.FromArgb(255, r, g, b);
            }
            return pal;
        }


        /*
         * 
         * AUTOMATIC ANALYSIS OF UNANALYZED DATA
         * 
         */

        private void Analyze_audio_chunks()
        {
            while (unanalyzed_audio.Count() > fft_size)
            {
                Analyze_audio_chunk();
            }
        }

        private void Analyze_audio_chunk()
        {
            if (unanalyzed_audio.Count() < fft_size) return;
            
            // do the FFT on the first part of the unanalyzed audio data
            Complex[] fft_buffer = new Complex[fft_size];
            for (int i = 0; i < fft_size; i++)
            {
                fft_buffer[i].X = (float)(unanalyzed_audio[i] * FastFourierTransform.HammingWindow(i, fft_size));
                fft_buffer[i].Y = 0;
            }
            FastFourierTransform.FFT(true, (int)Math.Log(fft_size, 2.0), fft_buffer);

            // a list with FFT values
            List<double> new_data = new List<double>();
            for (int i = fft_size/2; i>0; i--)
            {
                double val;
                val = (double)fft_buffer[i].X + (double)fft_buffer[i].Y; // sqrt(X^2+Y^2)?
                val = Math.Abs(val);
                if (cb_logscale.Checked) val = Math.Log(val);
                new_data.Add(val);
            }

            // roll-in this FFT data to the far right of the data
            spec_data.RemoveAt(0);
            spec_data.Add(new_data);

            // delete some audio from the unanalyzed buffer
            unanalyzed_audio.RemoveRange(0, fft_step_size);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Console.WriteLine("TICK {0}",captured_buffers);
            Analyze_audio_chunks();
            Update_bitmap_with_data();
        }

        




        /*
         * 
         * GUI MATH AND VALUE CHECKING
         * 
         */

        private int audio_rate;
        private int audio_buffer_ms;

        private static int buffer_size_points;
        private static int fft_size;
        private static int fft_step_size;
        private static int spectrograph_height_px;
        private static int spectrograph_width_px;
        private static double spectrograph_resolution;

        private void nud_buffer_size_ValueChanged(object sender, EventArgs e) { gui_calculations_update(); }
        private void combo_rate_SelectedIndexChanged(object sender, EventArgs e) { gui_calculations_update(); }
        private void nud_fft_size_ValueChanged(object sender, EventArgs e) { gui_calculations_update(); }
        private void nud_fft_step_ms_ValueChanged(object sender, EventArgs e) { gui_calculations_update(); }
        private void nud_spec_width_ValueChanged(object sender, EventArgs e) { gui_calculations_update(); }

        // call this when chaging audio recording and FFT settings
        private void gui_calculations_update()
        {
            // sample rate
            audio_rate = int.Parse(combo_rate.Text);

            // buffer size
            audio_buffer_ms = (int)nud_buffer_size.Value;
            buffer_size_points = (int)((double)audio_buffer_ms / 1000.0 * audio_rate);
            lbl_buffer_size_points.Text = string.Format("{0} points", buffer_size_points);

            // fft size
            fft_size = (int)Math.Pow(2, (int)nud_fft_size.Value);
            lbl_fft_size.Text = string.Format("{0} points", fft_size.ToString());

            // fft step
            fft_step_size = (int)((double)nud_fft_step_ms.Value / 1000.0 * audio_rate);
            lbl_fft_step_ms.Text = string.Format("{0} points", fft_step_size.ToString());

            // spectrograph width
            spectrograph_width_px = (int)nud_spec_width.Value;

            // stats about spetrograph
            lbl_peak_freq.Text = string.Format("{0:##,###} Hz", (((double)audio_rate) / 2));
            spectrograph_height_px = fft_size/2;
            spectrograph_resolution = (double)audio_rate/(double)spectrograph_height_px;
            lbl_resolution.Text = string.Format("{0:n2} Hz / px", spectrograph_resolution);
            lbl_spec_size.Text = string.Format("{0} px", spectrograph_height_px);
            lbl_spec_width_time.Text = string.Format("{0:n2} sec", (double)nud_fft_step_ms.Value/1000.0*spectrograph_width_px);

            // fix-up the picturebox
            pictureBox1.Width = spectrograph_width_px;
            pictureBox1.Height = spectrograph_height_px;

            // colormap selection
            if (cmb_colormap.Text == "") cmb_colormap.SelectedIndex=0;

            // fill picturebox with blankness
            Data_init();
        }

    }
}
