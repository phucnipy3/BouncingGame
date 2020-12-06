using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColorLevel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDraw_Click(object sender, System.EventArgs e)
        {
            List<LevelView> levelViews = new List<LevelView>();

            int n = int.Parse(textBox1.Text);
            for (int i = 1; i <= 2 * n; i++)
            {
                levelViews.Add(new LevelView() { Level = i, NormalColor = GetColorLevel(i, n), MediumColor = GetColorLevel(i, n, true) });
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
            if (current < 1 || level < 1)
                return Color.Empty;

            if ((!medium && current > level) || (medium && current > 2 * level))
                return Color.Empty;

            if (medium)
            {
                level = 2 * level - 1;
                return Color.FromArgb((int)((current - 1f) / level * 75), (int)(190 - (current - 1f) / level * 190), (int)(255 - (current - 1f) / level * 125));
            }

            if (level == 1)
                return Color.FromArgb(255, 0, 0);
            level--;
            return Color.FromArgb(255, (int)(255 - (current - 1f) / level * 255), (int)(50 - (current - 1f) / level * 50));
        }
    }

    public class LevelView
    {
        public int Level { get; set; }
        public Color NormalColor { get; set; }
        public Color MediumColor { get; set; }
    }
}
