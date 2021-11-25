using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalChecker
{
    public class Game
    {
        protected Player player1, player2;
        protected Gameboard gameboard;
        public Game() { }
        public Game(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
        }
        private void initalization()
        { }
        public void game_logic()
        { }
        public bool detect_game_end()
        {
            return true;
        }
        public void display_game() { }
        public void display_winning() { }
    }
}
