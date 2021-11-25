using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AnimalChecker
{
    public partial class FMain : Form
    {
        public bool _finish = true; 
        private Gameboard _game_board = new Gameboard();
        private Player _player1 = new Player();
        private Player _player2 = new Player();
        private Player _player_now = null;
        public FMain()
        {
            InitializeComponent();
            panel1.Controls.Clear();
            for (int i = 0; i < Gameboard.Rows; i++)
            {
                for (int j = 0; j < Gameboard.Columns; j++)
                {
                    panel1.Controls.Add(_game_board.gameboard[i, j]);
                }
            }
        }
        private void FMain_Load(object sender, EventArgs e)
        {
            resetToolStripMenuItem_Click(null, null);
        }
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _finish = false;
            _player1.player_pieces.Clear();
            _player1.player_pieces.AddRange(_game_board.Lst_Piece_Red);
            _player2.player_pieces.Clear();
            _player2.player_pieces.AddRange(_game_board.Lst_Piece_Blue);
            _game_board.Initalization();
            _game_board.display_board();
            _player_now = _player1;
            SetStatus();
        }
        private void SetStatus()
        {
            label1.Visible = false;
            label2.Visible = false;
            for (int i = 0; i < _player1.player_pieces.Count; i++)
            {
                _player1.player_pieces[i]._panel1.Click -= Panel_Piece_Click;
            }
            for (int i = 0; i < _player2.player_pieces.Count; i++)
            {
                _player2.player_pieces[i]._panel1.Click -= Panel_Piece_Click;
            }
            if (_player_now == _player1)
            {
                label1.Visible = true;
                for (int i = 0; i < _player1.player_pieces.Count; i++)
                {
                    _player1.player_pieces[i]._panel1.Click += Panel_Piece_Click;
                }
            }
            else
            {
                for (int i = 0; i < _player2.player_pieces.Count; i++)
                {
                    _player2.player_pieces[i]._panel1.Click += Panel_Piece_Click;
                }
                label2.Visible = true;
            }
            Hashtable hs = new Hashtable();
            for (int i = 0; i < _player_now.player_pieces.Count; i++)
            {
                hs.Add(_player_now.player_pieces[i]._panel1, "");
            }
            //点击空白处
            for (int i = 0; i < Gameboard.Rows; i++)
            {
                for (int j = 0; j < Gameboard.Columns; j++)
                {
                    _game_board.gameboard[i, j].Click -= Panel_Blank_Click;
                }
            }
            for (int i = 0; i < Gameboard.Rows; i++)
            {
                for (int j = 0; j < Gameboard.Columns; j++)
                {
                    if(hs.Contains(_game_board.gameboard[i, j]) == false)
                    {
                        _game_board.gameboard[i, j].Click += Panel_Blank_Click;
                    }
                }
            }
        }
        private void Panel_Piece_Click(object sender, EventArgs e)
        {
            if (_finish)
                return;
            Piece piece1 = null;
            Panel p1 = sender as Panel;
            for (int i = 0; i < _player_now.player_pieces.Count; i++)
            {
                if (p1 == _player_now.player_pieces[i]._panel1)
                {
                    piece1 = _player_now.player_pieces[i];
                    break;
                }
            }
            if (piece1 != null)
            {
                for (int i = 0; i < _player_now.player_pieces.Count; i++)
                {
                    if (_player_now.player_pieces[i] != piece1)
                    {
                        _player_now.player_pieces[i].Selected = false;
                    }
                }
                piece1.Selected = !piece1.Selected;
            }
        }
        private void Panel_Blank_Click(object sender, EventArgs e)
        {
            if (_finish)
                return;
            Panel p_tmp = sender as Panel;
            List<Piece> lst_rat = new List<Piece>();
            for (int i = 0; i < _player1.player_pieces.Count; i++)
            {
                if (_player1.player_pieces[i].Name == "鼠")
                { 
                    lst_rat.Add(_player1.player_pieces[i]);
                }
            }
            for (int i = 0; i < _player2.player_pieces.Count; i++)
            {
                if (_player2.player_pieces[i].Name == "鼠")
                { 
                    lst_rat.Add(_player2.player_pieces[i]);
                }
            }
            Piece p1 = _player_now.PieceSelected;
            if (p1 == null)
                return;
            int x = -1;
            int y = -1;
            for (int i = 0; i < Gameboard.Rows; i++)
            {
                for (int j = 0; j < Gameboard.Columns; j++)
                {
                    if(_game_board.gameboard[i, j] == sender)
                    {
                        x = j;
                        y = i;
                        break;
                    }
                }
            }
            Piece p_enemy = _player1.Get_Piece(p_tmp);
            if (p_enemy == null)
                p_enemy = _player2.Get_Piece(p_tmp);
            //如果没有敌人p_enemy，就直接进入
            bool is_legal = false;
            if (p_enemy == null)
            {
                if (p1.is_valid_move(x, y, lst_rat))
                {
                    is_legal = true;
                    p1.LocationColumn = x;
                    p1.LocationRow = y;
                    p1._panel1 = _game_board.gameboard[y, x];
                }
            }
            else if (p_enemy != null)
            {
                if (p1.is_valid_move(x, y, lst_rat))
                {
                    List<Point> lst_trap = null;
                    if (_player1 == _player_now)
                        lst_trap = _game_board.Lst_Point_Traps1;
                    else
                        lst_trap = _game_board.Lst_Point_Traps2;
                    bool is_in_trap = false;
                    for (int i = 0; i < lst_trap.Count; i++)
                    {
                        if (p_enemy.LocationColumn == lst_trap[i].X && p_enemy.LocationRow == lst_trap[i].Y)
                        {
                            is_in_trap = true;
                            break;
                        }
                    }
                    bool can_eat = p1.can_take_down(p_enemy) || is_in_trap;
                    if ( can_eat == true)
                    {
                        p_enemy.Dead = true;
                        _player1.player_pieces.Remove(p_enemy);
                        _player2.player_pieces.Remove(p_enemy);
                        is_legal = true;
                        p1.LocationColumn = x;
                        p1.LocationRow = y;
                        p1._panel1 = _game_board.gameboard[y, x];
                    }
                }
            }
            if (is_legal)
            {
                _game_board.display_board();
                if (_player_now == _player1)
                {
                    _player_now = _player2;
                }
                else
                {
                    _player_now = _player1;
                }
                SetStatus();
                if (isRedWin())
                {
                    _finish = true;
                    _player_now = null;
                    label1.Visible = false;
                    label2.Visible = false;
                    MessageBox.Show("红方赢了！");
                }
                else if (isBlueWin())
                {
                    _finish = true;
                    _player_now = null;
                    label1.Visible = false;
                    label2.Visible = false;
                    MessageBox.Show("蓝方赢了！");
                }
            }
        }
        private bool isRedWin()
        {
            for (int i = 0; i<_player1.player_pieces.Count; i++)
            {
                if (_player1.player_pieces[i].LocationColumn == _game_board.Point_Den2.X && _player1.player_pieces[i].LocationRow == _game_board.Point_Den2.Y)
                {
                    return true;
                }
            }
            if (_player2.player_pieces.Count == 0)
            {
                return true;
            }
            return false;
        }
        private bool isBlueWin()
        {
            for (int i = 0; i < _player2.player_pieces.Count; i++)
            {
                if (_player2.player_pieces[i].LocationColumn == _game_board.Point_Den1.X && _player2.player_pieces[i].LocationRow == _game_board.Point_Den1.Y)
                {
                    return true;
                }
            }
            if (_player1.player_pieces.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
