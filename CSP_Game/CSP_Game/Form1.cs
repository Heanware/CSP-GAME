﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPhotoshop.Data;
using MyPhotoshop;

namespace CSP_Game
{
    public partial class Form1 : Form
    {
        Photo map;
        int playerIndex;
        List<Player> players;
        Player currentPlayer;
        bool bIsBuilding = false; // проверка на нажатие кнопки, если была нажата - нужно строить

        public void InitializeMap()
        {
            map = new Photo(50, 30);
            pictureBox1.Height = map.height * map.pixelHeight;
            pictureBox1.Width = map.width * map.pixelWidth;
            for (int x = 0; x < map.width; x++)
                for (int y = 0; y < map.height; y++)
                {
                    map[x, y] = new Pixel(1, 1, 1);
                }
        }

        public void InitializePlayer()
        {
            players = new List<Player>();
            players.Add(new Player("Andrew", Color.Green));
            players.Add(new Player("Roman", Color.Crimson));
            playerIndex = 0;
            currentPlayer = players[playerIndex];
            Text = currentPlayer.Name;
        }

        public Form1()
        {
            InitializeComponent();
            InitializeMap();
            InitializePlayer();
            pictureBox1.Image = Convertors.Photo2Bitmap(map);
            AnyObject[] masterySelector = new AnyObject[]
            {
                new Tank(currentPlayer),
                new RifleMan(currentPlayer),
                new MiningCamp(currentPlayer),
                new Tower(currentPlayer),
            };
            comboBox1.DataSource = masterySelector;
            comboBox1.DisplayMember = "Name";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            int x = (int)Math.Floor((double)Cursor.Position.X / map.pixelWidth);
            int y = (int)Math.Floor((double)(Cursor.Position.Y - 30) / map.pixelHeight);
            if (bIsBuilding)
            {
                PlayerTurn.Build(currentPlayer, (AnyObject)comboBox1.SelectedValue, new Tuple<int, int>(x, y));
            }
            /*   MessageBox.Show(comboBox1.SelectedValue.ToString());*/

            map[x, y] = new Pixel((double)currentPlayer.Color.R / 255,
                (double)currentPlayer.Color.G / 255,
                (double)currentPlayer.Color.B / 255);
            pictureBox1.Image = Convertors.Photo2Bitmap(map);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (players.Where(player => player.IsAlive).Count() != 1)
            {
                playerIndex++;
                if (playerIndex > players.Count - 1)
                    playerIndex = 0;
                currentPlayer = players[playerIndex];
                Text = currentPlayer.Name;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bIsBuilding = true;
        }
    }
}
