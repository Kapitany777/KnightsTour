using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFKnights
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBlock[,] textBlocks;

        /// <summary>
        /// A képernyőn megjelenítendő sakktábla egy mezőjének szélessége / magassága
        /// </summary>
        int squareSize = 50;

        public MainWindow()
        {
            InitializeComponent();

            CreateBoard();
        }

        /// <summary>
        /// A sakktábla létrehozása a képernyőn
        /// </summary>
        private void CreateBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(squareSize);
                GridBoard.RowDefinitions.Add(row);

                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(squareSize);
                GridBoard.ColumnDefinitions.Add(column);
            }

            textBlocks = new TextBlock[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    TextBlock tb = new TextBlock();
                    tb.Style = FindResource("TextBlockBoard") as Style;

                    if ((i + j) % 2 == 0)
                    {
                        tb.Background = new SolidColorBrush(Colors.LightGray);
                    }

                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                    GridBoard.Children.Add(tb);

                    textBlocks[i, j] = tb;
                }
            }
        }

        /// <summary>
        /// A sakktábla feliratainak törlése
        /// </summary>
        private void ClearBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    textBlocks[i, j].Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Az eredmény megjelenítése a képernyőn
        /// </summary>
        /// <param name="route">Az eredményként kapott útvonal</param>
        private void DisplayResult(Position[] route)
        {
            CanvasBoard.Width = GridBoard.Width;
            CanvasBoard.Height = GridBoard.Height;
            CanvasBoard.Children.Clear();

            Position prevPos = new Position(0, 0);
            int step = 1;

            foreach (Position pos in route)
            {
                textBlocks[pos.Column, pos.Row].Text = (step).ToString();

                if (step > 1)
                {
                    Line line = new Line();
                    line.Stroke = Brushes.Gray;
                    line.StrokeThickness = 1;

                    line.X1 = prevPos.Row * squareSize + squareSize / 2;
                    line.Y1 = prevPos.Column * squareSize + squareSize / 2;
                    line.X2 = pos.Row  * squareSize + squareSize / 2;
                    line.Y2 = pos.Column * squareSize + squareSize / 2;

                    CanvasBoard.Children.Add(line);

                    Ellipse ellipse = new Ellipse();
                    ellipse.Fill = Brushes.Black;
                    ellipse.Width = 8;
                    ellipse.Height = 8;
                    Canvas.SetLeft(ellipse, line.X1 - 4);
                    Canvas.SetTop(ellipse, line.Y1 - 4);
                    CanvasBoard.Children.Add(ellipse);

                    prevPos = pos;

                    if (step == route.Length)
                    {
                        Ellipse ellipse2 = new Ellipse();
                        ellipse2.Fill = Brushes.Black;
                        ellipse2.Width = 8;
                        ellipse2.Height = 8;
                        Canvas.SetLeft(ellipse2, line.X2 - 4);
                        Canvas.SetTop(ellipse2, line.Y2 - 4);
                        CanvasBoard.Children.Add(ellipse2);
                    }
                }

                step++;
            }
        }

        /// <summary>
        /// A keresés futtatása
        /// </summary>
        private void SolveProblem()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            this.Cursor = Cursors.Wait;

            Solver solver = new Solver(int.Parse(ComboBoxSize.Text));
            bool success = solver.Solve();

            this.Cursor = Cursors.Arrow;

            sw.Stop();

            TextBlockElapsedTime.Text = sw.ElapsedMilliseconds.ToString();

            ClearBoard();

            if (success)
            {
                DisplayResult(solver.Route);
            }
            else
            {
                MessageBox.Show("Nem találtam megoldást!");
            }
        } 

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            SolveProblem();
        }
    }
}
