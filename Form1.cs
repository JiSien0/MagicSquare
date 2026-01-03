using System;
using System.Drawing;
using System.Windows.Forms;

namespace MagicSquare
{
    public partial class Form1 : Form
    {
        private TextBox[,] textBoxes;
        private int n;
        private TextBox txtSize;
        private Button btnBegin;
        private Button btnCount;
        private Panel panel1;
        private Label lblResult;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Магічний квадрат";
            this.Size = new Size(500, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label label1 = new Label();
            label1.Text = "Введіть N:";
            label1.Location = new Point(20, 20);
            label1.AutoSize = true;
            this.Controls.Add(label1);

            txtSize = new TextBox();
            txtSize.Location = new Point(100, 17);
            txtSize.Width = 50;
            txtSize.Text = "3";
            this.Controls.Add(txtSize);

            btnBegin = new Button();
            btnBegin.Text = "Begin";
            btnBegin.Location = new Point(170, 15);
            btnBegin.Click += btnBegin_Click;
            this.Controls.Add(btnBegin);

            btnCount = new Button();
            btnCount.Text = "COUNT";
            btnCount.Location = new Point(260, 15);
            btnCount.Click += btnCount_Click;
            btnCount.Enabled = false;
            this.Controls.Add(btnCount);

            panel1 = new Panel();
            panel1.Location = new Point(20, 60);
            panel1.Size = new Size(440, 400);
            panel1.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panel1);

            lblResult = new Label();
            lblResult.Location = new Point(20, 470);
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Arial", 12, FontStyle.Bold);
            this.Controls.Add(lblResult);
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            lblResult.Text = "";

            // отримуємо розмір
            if (int.TryParse(txtSize.Text, out n) == false || n <= 0 || n > 9)
            {
                MessageBox.Show("Введіть число від 1 до 9!");
                return;
            }

            textBoxes = new TextBox[n, n];

            int size = 40;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    TextBox tb = new TextBox();
                    tb.Size = new Size(size, size);
                    tb.Location = new Point(j * (size + 5) + 10, i * (size + 5) + 10);
                    tb.MaxLength = 1;
                    tb.TextAlign = HorizontalAlignment.Center;
                    tb.Font = new Font("Arial", 14);
                    tb.KeyPress += tb_KeyPress;

                    textBoxes[i, j] = tb;
                    panel1.Controls.Add(tb);
                }
            }

            btnCount.Enabled = true;
        }

        // тільки цифри 1-9
        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) == false && (e.KeyChar < '1' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            // перевіримо чи все заповнено
            int[,] arr = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (textBoxes[i, j].Text == "")
                    {
                        MessageBox.Show("Заповніть всі поля!");
                        return;
                    }
                    arr[i, j] = Convert.ToInt32(textBoxes[i, j].Text);
                }
            }

            // перевіримо магічний квадрат
            if (IsMagic(arr) == true)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "Це МАГІЧНИЙ квадрат!";
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "Це НЕ магічний квадрат";
            }
        }

        private bool IsMagic(int[,] arr)
        {
            // сума першого рядка
            int sum = 0;
            for (int j = 0; j < n; j++)
            {
                sum = sum + arr[0, j];
            }

            // перевірка рядків
            for (int i = 0; i < n; i++)
            {
                int s = 0;
                for (int j = 0; j < n; j++)
                {
                    s = s + arr[i, j];
                }
                if (s != sum)
                    return false;
            }

            // перевірка стовпців
            for (int j = 0; j < n; j++)
            {
                int s = 0;
                for (int i = 0; i < n; i++)
                {
                    s = s + arr[i, j];
                }
                if (s != sum)
                    return false;
            }

            // головна діагональ
            int d1 = 0;
            for (int i = 0; i < n; i++)
            {
                d1 = d1 + arr[i, i];
            }
            if (d1 != sum)
                return false;

            // побічна діагональ
            int d2 = 0;
            for (int i = 0; i < n; i++)
            {
                d2 = d2 + arr[i, n - 1 - i];
            }
            if (d2 != sum)
                return false;

            return true;
        }
    }
}
