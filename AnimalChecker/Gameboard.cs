using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;

namespace AnimalChecker
{
    public class Gameboard
    {
        public const int Columns = 7;
        public const int Rows = 9;
        public Panel[,] gameboard = new Panel[Rows, Columns];
        public List<Point> Lst_Point_River = new List<Point>();
        public List<Point> Lst_Point_Traps1 = new List<Point>();
        public List<Point> Lst_Point_Traps2 = new List<Point>();
        public List<Piece> Lst_Piece_Red = new List<Piece>();
        public List<Piece> Lst_Piece_Blue = new List<Piece>();
        public Point Point_Den1 ;
        public Point Point_Den2 ;
        public Gameboard() 
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    gameboard[i, j] = new Panel();
                }
            }
            Lst_Point_River.Clear();
            Lst_Point_River.Add(new Point(1, 3));
            Lst_Point_River.Add(new Point(1, 4));
            Lst_Point_River.Add(new Point(1, 5));
            Lst_Point_River.Add(new Point(2, 3));
            Lst_Point_River.Add(new Point(2, 4));
            Lst_Point_River.Add(new Point(2, 5));
            Lst_Point_River.Add(new Point(4, 3));
            Lst_Point_River.Add(new Point(4, 4));
            Lst_Point_River.Add(new Point(4, 5));
            Lst_Point_River.Add(new Point(5, 3));
            Lst_Point_River.Add(new Point(5, 4));
            Lst_Point_River.Add(new Point(5, 5));
            Lst_Point_Traps1.Clear();
            Lst_Point_Traps1.Add(new Point(2, 0));
            Lst_Point_Traps1.Add(new Point(3, 1));
            Lst_Point_Traps1.Add(new Point(4, 0));
            Lst_Point_Traps2.Clear();
            Lst_Point_Traps2.Add(new Point(2, 8));
            Lst_Point_Traps2.Add(new Point(3, 7));
            Lst_Point_Traps2.Add(new Point(4, 8));
            Point_Den1 = new Point(3, 0);
            Point_Den2 = new Point(3, 8);
            Lst_Piece_Red.Clear();
            Lst_Piece_Red.Add(new Piece(Camp.Red, "鼠", 0, 2, gameboard[2, 0]));
            Lst_Piece_Red.Add(new Piece(Camp.Red, "象", 6, 2, gameboard[2, 6]));
            Lst_Piece_Red.Add(new Piece(Camp.Red, "狮", 0, 0, gameboard[0, 0]));
            Lst_Piece_Red.Add(new Piece(Camp.Red, "虎", 6, 0, gameboard[0, 6]));
            Lst_Piece_Red.Add(new Piece(Camp.Red, "豹", 2, 2, gameboard[2, 2]));
            Lst_Piece_Red.Add(new Piece(Camp.Red, "狼", 4, 2, gameboard[2, 4]));
            Lst_Piece_Red.Add(new Piece(Camp.Red, "狗", 1, 1, gameboard[1, 1]));
            Lst_Piece_Red.Add(new Piece(Camp.Red, "猫", 5, 1, gameboard[1, 5]));
            Lst_Piece_Blue.Clear();
            Lst_Piece_Blue.Add(new Piece(Camp.Blue, "鼠", 6, 6, gameboard[6, 6]));
            Lst_Piece_Blue.Add(new Piece(Camp.Blue, "象", 0, 6, gameboard[6, 0]));
            Lst_Piece_Blue.Add(new Piece(Camp.Blue, "狮", 6, 8, gameboard[8, 6]));
            Lst_Piece_Blue.Add(new Piece(Camp.Blue, "虎", 0, 8, gameboard[8, 0]));
            Lst_Piece_Blue.Add(new Piece(Camp.Blue, "豹", 4, 6, gameboard[6, 4]));
            Lst_Piece_Blue.Add(new Piece(Camp.Blue, "狼", 2, 6, gameboard[6, 2]));
            Lst_Piece_Blue.Add(new Piece(Camp.Blue, "狗", 5, 7, gameboard[7, 5]));
            Lst_Piece_Blue.Add(new Piece(Camp.Blue, "猫", 1, 7, gameboard[7, 1]));
            Initalization();
            display_board();
        }
        public void Initalization()
        {
            try
            { 
                //动物排列
                for (int i = 0; i < Lst_Piece_Red.Count; i++)
                {
                    if (Lst_Piece_Red[i].Name == "鼠")
                        Lst_Piece_Red[i].Init(0, 2, gameboard[2, 0]);
                    if (Lst_Piece_Red[i].Name == "象")
                        Lst_Piece_Red[i].Init(6, 2, gameboard[2, 6]);
                    if (Lst_Piece_Red[i].Name == "狮")
                        Lst_Piece_Red[i].Init(0, 0, gameboard[0, 0]);
                    if (Lst_Piece_Red[i].Name == "虎")
                        Lst_Piece_Red[i].Init(6, 0, gameboard[0, 6]);
                    if (Lst_Piece_Red[i].Name == "豹")
                        Lst_Piece_Red[i].Init(2, 2, gameboard[2, 2]);
                    if (Lst_Piece_Red[i].Name == "狼")
                        Lst_Piece_Red[i].Init(4, 2, gameboard[2, 4]);
                    if (Lst_Piece_Red[i].Name == "狗")
                        Lst_Piece_Red[i].Init(1, 1, gameboard[1, 1]);
                    if (Lst_Piece_Red[i].Name == "猫")
                        Lst_Piece_Red[i].Init(5, 1, gameboard[1, 5]);
                }
                for (int i = 0; i < Lst_Piece_Blue.Count; i++)
                {
                    if (Lst_Piece_Blue[i].Name == "鼠")
                        Lst_Piece_Blue[i].Init(6, 6, gameboard[6, 6]);
                    if (Lst_Piece_Blue[i].Name == "象")
                        Lst_Piece_Blue[i].Init(0, 6, gameboard[6, 0]);
                    if (Lst_Piece_Blue[i].Name == "狮")
                        Lst_Piece_Blue[i].Init(6, 8, gameboard[8, 6]);
                    if (Lst_Piece_Blue[i].Name == "虎")
                        Lst_Piece_Blue[i].Init(0, 8, gameboard[8, 0]);
                    if (Lst_Piece_Blue[i].Name == "豹")
                        Lst_Piece_Blue[i].Init(4, 6, gameboard[6, 4]);
                    if (Lst_Piece_Blue[i].Name == "狼")
                        Lst_Piece_Blue[i].Init(2, 6, gameboard[6, 2]);
                    if (Lst_Piece_Blue[i].Name == "狗")
                        Lst_Piece_Blue[i].Init(5, 7, gameboard[7, 5]);
                    if (Lst_Piece_Blue[i].Name == "猫")
                        Lst_Piece_Blue[i].Init(1, 7, gameboard[7, 1]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public Piece get_area(Panel p1)
        {
            for (int i = 0; i < Lst_Piece_Red.Count; i++)
            {
                if (Lst_Piece_Red[i]._panel1 == p1)
                {
                    return Lst_Piece_Red[i];
                }
            }
            for (int i = 0; i < Lst_Piece_Blue.Count; i++)
            {
                if (Lst_Piece_Blue[i]._panel1 == p1)
                {
                    return Lst_Piece_Blue[i];
                }
            }
            return null;
        }
        public void display_board()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Panel p1 = gameboard[i, j];
                    p1.Visible = true;
                    p1.Width = Piece.HW;
                    p1.Height = Piece.HW;
                    p1.Left = Piece.HW * j;
                    p1.Top = Piece.HW * i;
                    p1.BorderStyle = BorderStyle.FixedSingle;
                    p1.BackColor = Color.LightYellow;
                    p1.BackgroundImage = null;
                    p1.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            //画河
            for (int i = 0; i < Lst_Point_River.Count; i++)
            {
                gameboard[Lst_Point_River[i].Y, Lst_Point_River[i].X].BackColor = Color.LightSkyBlue;
            }
            //画陷阱
            Bitmap bt_map_den1 = new Bitmap(Application.StartupPath + "\\IMG\\trap1.jpg");
            for (int i = 0; i < Lst_Point_Traps1.Count; i++)
            {
                gameboard[Lst_Point_Traps1[i].Y, Lst_Point_Traps1[i].X].BackColor = Color.LightPink;
                gameboard[Lst_Point_Traps1[i].Y, Lst_Point_Traps1[i].X].BackgroundImage = bt_map_den1;
            }
            Bitmap bt_map_den2 = new Bitmap(Application.StartupPath + "\\IMG\\trap2.jpg");
            for (int i = 0; i < Lst_Point_Traps2.Count; i++)
            {
                gameboard[Lst_Point_Traps2[i].Y, Lst_Point_Traps2[i].X].BackColor = Color.LightPink;
                gameboard[Lst_Point_Traps2[i].Y, Lst_Point_Traps2[i].X].BackgroundImage = bt_map_den2;
            }
            //画穴
            gameboard[Point_Den1.Y, Point_Den1.X].BackColor = Color.Gray;
            gameboard[Point_Den1.Y, Point_Den1.X].BackgroundImage = new Bitmap(Application.StartupPath + "\\IMG\\den1.jpg");
            gameboard[Point_Den2.Y, Point_Den2.X].BackColor = Color.Gray;
            gameboard[Point_Den2.Y, Point_Den2.X].BackgroundImage = new Bitmap(Application.StartupPath + "\\IMG\\den2.jpg");
            //画棋子
            for (int i = 0; i < Lst_Piece_Red.Count; i++)
            {
                if(Lst_Piece_Red[i].Dead == false)
                    Lst_Piece_Red[i].Selected = false; 
            }
            for (int i = 0; i < Lst_Piece_Blue.Count; i++)
            {
                if (Lst_Piece_Blue[i].Dead == false)
                    Lst_Piece_Blue[i].Selected = false; 
            }
        }
    }
}
