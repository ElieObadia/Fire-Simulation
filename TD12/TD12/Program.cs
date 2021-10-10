using System;
using System.Collections;

namespace TD12
{
    class Program
    {
        #region Saisie Entier
        static int SaisieEntier(string message)
        {
            int nb;
            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out nb) || nb < 0) { Console.WriteLine(message); }
            return nb;
        }
        #endregion

        #region Remplissage Aléatoire
        static Cellule[,] RemplissageAleatoire()
        {
            #region Création Tableau
            int n = SaisieEntier("Entrez le nombre de lignes");
            int m = SaisieEntier("Entrez le nombre de colonnes");
            Cellule[,] foret = new Cellule[n, m];
            #endregion

            Random rand = new Random(); //Création du module aléatoire

            #region Remplissage Aléatoire
            for (int i = 0; i < foret.GetLength(0); i++) //Parcours des lignes
            {
                for (int j = 0; j < foret.GetLength(1); j++) //Parcours des colonnes
                {
                    int type = rand.Next(1, 7); //Génération du type de la cellule 
                    switch (type)
                    {
                        case 1: // Ajout herbe
                            foret[i, j] = new Cellule("x", 8, false);
                            break;
                        case 2: // Ajout arbre
                            foret[i, j] = new Cellule("*", 10, false);
                            break;
                        case 3:  // Ajout terrain
                            foret[i, j] = new Cellule("+", 0, false);
                            break;
                        case 4:  // Ajout feuille
                            foret[i, j] = new Cellule(" ", 4, false);
                            break;
                        case 5:  // Ajout eau
                            foret[i, j] = new Cellule("/", 0, false);
                            break;
                        case 6:  // Ajout rocher
                            foret[i, j] = new Cellule("#", 50, false);
                            break;
                    }
                }
            }
            #endregion

            return foret;
        }
        #endregion

        #region Affichage Plateau
        static void AffichagePlateau(Cellule[,] plateau)
        {
            for (int i = 0; i < plateau.GetLength(0); i++) //Parcours des lignes
            {
                for (int j = 0; j < plateau.GetLength(1); j++) //Parcours des colonnes
                {
                    Console.Write(plateau[i, j].Type + " "); //Affichage de la cellule(i,j)
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region Initialisation Feu
        static void InitialiseFeu(Cellule[,] plateau)
        {
            Random rand = new Random(); //Création du module aléatoire
            int temi = rand.Next(0, plateau.GetLength(0)); //Création du numéro de ligne aléatoire
            int temj = rand.Next(0, plateau.GetLength(1)); //Création du numéro de colonne aléatoire
            while (plateau[temi, temj].Type == "/" || plateau[temi, temj].Type == " ") // On vérifie que la case puisse prendre feu
            {
                temi = rand.Next(0, plateau.GetLength(0)); //Création d'un nouveau numéro de ligne aléatoire
                temj = rand.Next(0, plateau.GetLength(1)); //Création d'un nouveau numéro de colonne aléatoire
            }
            #region Allumage Cellule
            plateau[temi, temj].Etat = true;
            plateau[temi, temj].Degre -= 1;
            #endregion
        }
        #endregion

        #region Nombre De Voisins En Feu
        static int NombreVoisinsFeu(Cellule[,] plateau, int indL, int indC)
        {
            int nb = 0; //Initialisation du nombre de voisin en feu
            for (int i = indL - 1; i <= indL + 1; i++) //Parcours des lignes voisines
            {
                for (int j = indC - 1; j <= indC + 1; j++) //Parcours des colonnes voisines
                {
                    if (i != indC && j != indL) //On vérfie que l'on ne se situe pas à la case index
                    {
                        int temi = i; //Création d'un indice de ligne temporaire
                        int temj = j; //Création d'un indice de colonne temporaire
                        if (i == -1) //Si i = -1 alors on est sur la ligne en bordure haute
                        {
                            temi = plateau.GetLength(0)-1; //On effectue donc la jonction avec le bas
                        }
                        else
                        {
                            if(i== plateau.GetLength(0)) //Si i = plateau.GetLength(0) alors on est sur la ligne en bordure inférieure
                            {
                                temi = 0; //On effectue donc la jonction avec le haut
                            }
                        }
                        if (j == -1) //Si j = -1 alors on est sur la colonne en bordure gauche
                        {
                            temj = plateau.GetLength(1)-1; //On effectue donc la jonction avec la droite
                        }
                        else
                        {
                            if (j == plateau.GetLength(1)) //Si j = plateau.GetLength(1) alors on est sur la colonne en bordure droite
                            {
                                temj = 0; //On effectue donc la jonction avec la gauche
                            }
                        }
                        if (plateau[temi, temj].Etat == true) //On regarde si la cellule (temi,temj) est en feu
                        {
                            nb++; //Si oui on ajoute 1 au nombre de voisin
                        }
                    }
                }
            }
            return nb;
        }
        #endregion

        #region Règle 1
        static void Regle1(Cellule[,] foret, int i, int j) //Méthode permettant d'appliquer la règle 1
        {
            foret[i, j].Type = "-"; // Transformation en cendres
            foret[i, j].Etat = true;
            foret[i, j].Degre -= 1;
        }
        #endregion

        #region Règle 2
        static void Regle2(Cellule[,] foret, int i, int j) //Méthode permettant d'appliquer la règle 2
        {
            foret[i, j].Type = "."; // Transformation en cendres éteintes
            foret[i, j].Etat = false;
            foret[i, j].Degre -= 1;
        }
        #endregion

        #region Règle 3
        static void Regle3(Cellule[,] foret, int i, int j) //Méthode permettant d'appliquer la règle 3
        {
            foret[i, j].Etat = true; //Mise à feu
            foret[i, j].Degre -= 1;
        }
        #endregion

        #region Règle 4
        static void Regle4(Cellule[,] foret, int i, int j) //Méthode permettant d'appliquer la règle 4
        {
            foret[i, j].Degre -= 1; //Combustion
        }
        #endregion

        #region Simulation
        static void Simulation()
        {
            Cellule[,] foret = RemplissageAleatoire();
            InitialiseFeu(foret);
            int nbTours = SaisieEntier("Entrez le nombre de tours");
            for (int k = 1; k < nbTours; k++)
            {
                for (int i = 0; i < foret.GetLength(0); i++) // Parcours des lignes du plateau
                {
                    for (int j = 0; j < foret.GetLength(1); j++) // Parcours des colonnes du plateau
                    {
                        if (foret[i, j].Etat == true && foret[i, j].Degre == 2) // Règle 1
                        {
                            Regle1(foret, i, j);
                        }
                        else
                        {
                            if (foret[i, j].Type == "-") // Règle 2
                            {
                                Regle2(foret, i, j);
                            }
                            else
                            {
                                //L'énoncé ne demande pas le cas des feuilles je décide donc de les laisser à part elles ne pourront pas prendre feu
                                //Si on voulait modifier cela il faudrait ajouter à l'intérieur de la paranthèse dans la condition : foret[i, j].Type == " " ||
                                if (NombreVoisinsFeu(foret, i, j) > 0 && foret[i, j].Etat == false && (foret[i, j].Type == "*" || foret[i, j].Type == "#" || foret[i, j].Type == "*" || foret[i, j].Type == "x")) // Règle 3
                                {
                                    Regle3(foret, i, j);
                                }
                                else
                                {
                                    if (foret[i, j].Etat) //Règle 4
                                    {
                                        Regle4(foret, i, j);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            AffichagePlateau(foret);
        }
        #endregion

        static void Main(string[] args)
        {
            Simulation();
        }
    }
}
