using System;
using System.Drawing;
using System.Windows.Forms;
using TicTacToe.Properties;

namespace TicTacToe
{
    public partial class TicTacToeView : Form
    {
        private const int SquareAmount = 3;
        private const int CellWidth = 125;
        private const int MaxWidth = 1000;
        private const string Title = "Tic-tac-toe";

        private readonly Game _game;

        public TicTacToeView()
        {
            InitializeComponent();
            InitializeTable(SquareAmount);

            Text = Title;
            _game = new Game(SquareAmount, true);
        }

        protected virtual void OnWin(bool winner, int moveCount)
        {
            MessageBox.Show($"{GetName(winner)} have won.{Environment.NewLine}The match lasted for {moveCount} moves.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected virtual void OnDraw()
        {
            MessageBox.Show($"It's a draw.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializeTable(int width)
        {
            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = width,
                RowCount = width,
                TabIndex = 0
            };

            float sqWidth = 100f / width;

            for (int i = 0; i < width; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, sqWidth));
                table.RowStyles.Add(new RowStyle(SizeType.Percent, sqWidth));
            }

            for (int row = 0; row < width; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Panel cell = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Tag = new Point(row, col),
                        BackColor = Color.LightGray,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        Margin = new Padding(4)
                    };

                    cell.Click += CellClick;

                    table.Controls.Add(cell, col, row);
                }
            }

            Controls.Add(table);

            int formWidth = Math.Min(CellWidth * width, MaxWidth);
            ClientSize = new Size(formWidth, formWidth);
        }

        private void CellClick(object sender, EventArgs e)
        {
            Control cell = sender as Control;
            Point pos = (Point)cell.Tag;

            try
            {
                MoveResult result = _game.SetMark(pos.X, pos.Y);
                cell.BackgroundImage = GetImage(result.MarkedPlayer);

                if (result.GameEnded)
                {
                    if (result.IsDraw)
                    {
                        OnDraw();
                    }
                    else
                    {
                        OnWin(result.Winner, result.MoveCount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Image GetImage(bool player) => player ? Resources.O : Resources.X;
        private string GetName(bool player) => player ? "Circles" : "Crosses";
    }
}
