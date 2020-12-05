using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColorLevel
{
    public partial class Form1 : Form
    {
        private Color MaxColor = Color.FromArgb(75, 0, 130);
        private Color MinColor = Color.FromArgb(0, 190, 255);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDraw_Click(object sender, System.EventArgs e)
        {
            List<LevelView> levelViews = new List<LevelView>();

            int n = int.Parse(textBox1.Text);
            for (int i = 0; i < 2 * n; i++)
            {
                levelViews.Add(new LevelView() { Level = i + 1, NormalColor = i + 1 > n ? Color.Empty : GetColorLevel(i + 1, n), MediumColor = GetColorLevel(i + 1, 2 * n, true) });
            }    

            dataGridView1.DataSource = levelViews;

            for (int i = 0; i < 2 * n; i++)
            {
                dataGridView1.Rows[i].Cells[1].Style.BackColor = levelViews[i].NormalColor;
                dataGridView1.Rows[i].Cells[2].Style.BackColor = levelViews[i].MediumColor;
            }    
        }

        private Color GetColorLevel(int current, int level, bool medium = false)
        {
            if (medium)
                return Color.FromArgb((int)(((float)current / level) * 75), (int)(190 - ((float)current / level) * 190), (int)(255 - ((float)current / level) * 125));
            return Color.FromArgb(255, (int)(255 - ((float)current / level) * 255), (int)(50 - ((float)current / level) * 50));
        }
    }

    public class LevelView
    {
        public int Level { get; set; }
        public Color NormalColor { get; set; }
        public Color MediumColor { get; set; }
    }
}
