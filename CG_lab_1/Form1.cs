using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CG_lab_1
{
    public partial class Form1 : Form
    {
        Bitmap image;
        private Stack<Bitmap> history;
        public Form1()
        {
            InitializeComponent();
            history = new Stack<Bitmap>();
        }

        private void отменитьПоследнееДействиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (history.Any())
            {
                image = history.Pop();
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void эффектВолныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new WavesFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            history.Push(image);
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.png;*.jpg;*.bmp;|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
                history.Clear();
            }
        }
        

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void оттенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilters();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void изменениеРезкоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить картинку как...";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;

                sfd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                sfd.ShowHelp = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MedianFilter(9);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayWorldFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линейноеРастяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new HistogramStretching();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void эффектСтеклаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GlassEffect();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void операторЩарраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ScharrFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void операторПрюиттаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new PrewitteFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void идеальноеОтражениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new IdealReflectionFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void коррекцияСОпорнымЦветомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ColorCorrectionFilter(Color.Red);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ColorCorrectionFilter(Color.Red);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ColorCorrectionFilter(Color.Green);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ColorCorrectionFilter(Color.Blue);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GlowEdgeFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TranslationFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new RotationFilter(45,100,100);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MotionBlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void светящиесяКраяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new EmbossFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void hatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TopHat();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void размытиеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            double[,] selectedKernel = { { -1.0, 0.0, 1.0 }, { -1.0, 0.0, 1.0 }, { -1.0, 0.0, 1.0 } };
            Filters filter = new TopHatFilter(selectedKernel);
            backgroundWorker1.RunWorkerAsync(filter);
        }
        
        private void усилениеГраницToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[,] selectedKernel = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            Filters filter = new TopHatFilter(selectedKernel);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void обнаружениеВертикальныхЛинийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[,] selectedKernel = { { 0,1,0 }, { 1, -4, 1 }, { 0, 1, 0 } };
            Filters filter = new TopHatFilter(selectedKernel);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void blackHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlackHat();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            double[,] selectedKernel = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            Filters filter = new BlackHatFilter(selectedKernel);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            double[,] selectedKernel = { { -1, 2, -1 }, { -1, 2, -1 }, { -1, 2, -1 } };
            Filters filter = new BlackHatFilter(selectedKernel);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            double[,] selectedKernel = { { -1, -1, -1 }, { 2, 2, 2 }, { -1, -1, -1 } };
            Filters filter = new BlackHatFilter(selectedKernel);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Grad();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void горизонтальныеКраяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[,] selectedKernel = { { 2, 2, 2 }, { 1, 1, 1 }, { 2, 2, 2 } };
            Filters filter = new GradientFilter(selectedKernel);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void вертикальныеКраяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[,] selectedKernel = { { 2, 1, 2 }, { 2, 1, 2 }, { 2, 1, 2 } };
            Filters filter = new GradientFilter(selectedKernel);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void усилениеГраницToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            double[,] selectedKernel = { { 2, 2, 2 }, { 2, 2, 2 }, { 2, 2, 2 } };
            Filters filter = new GradientFilter(selectedKernel);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (history.Any())
            {
                image = history.Pop();
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        
    }
    
}
