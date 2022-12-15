using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JeuxDuPendu.MyControls;

namespace JeuxDuPendu
{
    public partial class GameForm : Form
    {

        Random r = new Random();

        string[] mots = {"esiee", "poule", "azerty", "manger" }; 

        string mot;
        

        // Initialisation de l'instance de la classe d'affichage du pendu.
        HangmanViewer _HangmanViewer = new HangmanViewer();

        /// <summary>
        /// Constructeur du formulaire de jeux
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
            InitializeMyComponent();
            StartNewGame();
        }

        /// <summary>
        /// Initialisations des composants specifiques a l'application
        /// </summary>
        private void InitializeMyComponent()
        {
            // On positionne le controle d'affichage du pendu dans panel1 : 
            panel1.Controls.Add(_HangmanViewer);
			
			// à la position 0,0
            _HangmanViewer.Location = new Point(0, 0);
			
			// et de la même taille que panel1
            _HangmanViewer.Size = panel1.Size;

            //initialisation du mot
        }

        /// <summary>
        /// Initialise une nouvelle partie
        /// </summary>
        public void StartNewGame()
        {
            // Methode de reinitialisation classe d'affichage du pendu.
            _HangmanViewer.Reset();

            mot = mots[r.Next(0, mots.Length)];

            //Affichage du mot à trouver dans le label.
            AfficheInit();
        }

        public void AfficheInit()
        {
            
            StringBuilder myStringBuilder = new StringBuilder();
            StringBuilder myStringBuilder2 = new StringBuilder();


            for (int i = 0; i < mot.Length ; i++)
            {
                myStringBuilder.Append("_");
                myStringBuilder2.Append("");
            }
            lCrypedWord.Text = myStringBuilder.ToString();

            
            label1.Text = myStringBuilder2.ToString();
        }

        
        /// <summary>
        /// Methode appelé lors de l'appui d'une touche du clavier, lorsque le focus est sur le bouton "Nouvelle partie"
        /// </summary>
        private void bReset_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressed(e.KeyChar);
        }

        /// <summary>
        /// Methode appelé lors de l'appui d'un touche du clavier, lorsque le focus est sur le formulaire
        /// </summary>
        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {

            KeyPressed(e.KeyChar);

        }

        /// <summary>
        /// Methode appelé lors de l'appui sur le bouton "Nouvelle partie"
        /// </summary>
        private void bReset_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void KeyPressed(char letter)
        {
            StringBuilder myStringBuilder = new StringBuilder(lCrypedWord.Text);
            StringBuilder myStringBuilder2 = new StringBuilder(label1.Text);
            bool c = false;

            for (int i=0;i< mot.Length; i++)
            {
                if (mot[i] == letter)
                {
                    myStringBuilder.Remove(i, 1);
                    myStringBuilder.Insert(i, letter);
                    c = true;
                }

              
            }

            if (c == false && !label1.Text.Contains(letter))
            {
                // On avance le pendu d'une etape
                _HangmanViewer.MoveNextStep();
                myStringBuilder2.Append(letter);
            }

            lCrypedWord.Text = myStringBuilder.ToString();
            label1.Text = myStringBuilder2.ToString();


            if (lCrypedWord.Text == mot)
            {
                MessageBox.Show("Vous avez gagné !");
                StartNewGame();
            }

            // Si le pendu est complet, le joueur à perdu.
            if (_HangmanViewer.IsGameOver)
            {
                MessageBox.Show("Vous avez perdu !");
                StartNewGame();
            }
        }

        

        private void lCrypedWord_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void règlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Le but du jeu est simple : deviner toute les lettres qui doivent composer un mot, éventuellement avec un nombre limité de tentatives et des thèmes fixés à l’avance.\n A chaque fois que le joueur devine une lettre, celle-ci est affichée. Dans le cas contraire, le dessin d’un pendu se met à apparaître…");
        }

        private void créditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" pendu crée par Paul Falloni, Benoit Gautier et Maxime Baratte");
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Merci d'avoir joué !");
            Application.Exit();
        }
    }

}
