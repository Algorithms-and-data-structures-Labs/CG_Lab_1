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
        public Form1()
        {
            InitializeComponent();
        }
        private Bitmap previousImage;
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.png;*.jpg;*.bmp;|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
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

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new InvertFilter();
            //Bitmap resultImage = filter.processImage(image, backgroundWorker1);
            backgroundWorker1.RunWorkerAsync(filter);
            //pictureBox1.Image = resultImage;
            //pictureBox1.Refresh();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new BlurFilter();
           // Bitmap resultImage = filter.processImage(image, backgroundWorker1);
            backgroundWorker1.RunWorkerAsync(filter);
            //pictureBox1.Image = resultImage;
            //pictureBox1.Refresh();
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new GaussianFilter();
            //Bitmap resultImage = filter.processImage(image, backgroundWorker1);
            backgroundWorker1.RunWorkerAsync(filter);
            //pictureBox1.Image = resultImage;
            //pictureBox1.Refresh();
        }

        private void оттенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new GrayScaleFilters();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void изменениеРезкоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        

        private void отменитьПоследнееДействиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (previousImage != null)
            {
                pictureBox1.Image = previousImage;
                pictureBox1.Refresh();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить картинку как...";
                sfd.OverwritePrompt = true; // показывать ли "Перезаписать файл" если пользователь указывает имя файла, который уже существует
                sfd.CheckPathExists = true; // отображает ли диалоговое окно предупреждение, если пользователь указывает путь, который не существует

                sfd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                sfd.ShowHelp = true; // отображается ли кнопка Справка в диалоговом окне

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
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new MedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new GrayWorldFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линейноеРастяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new HistogramStretching();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void эффектСтеклаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new GlassEffect();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void эффектВолныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new WavesFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void операторЩарраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new ScharrFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void операторПрюиттаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage = new Bitmap((Bitmap)pictureBox1.Image.Clone());
            Filters filter = new PrewitteFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
    
}
