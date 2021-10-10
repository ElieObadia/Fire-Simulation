using System;
using System.Collections.Generic;
using System.Text;

namespace TD12
{
    class Cellule
    {
        //Attributs
        private string type;
        private bool etat;
        private int degre;

        //Constructeur
        public Cellule(string type, int degre, bool etat)
        {
            this.type = type;
            this.degre = degre;
            this.etat = etat;
        }

        //Propriétés
        public string Type { get { return this.type; } set { this.type = value; } }
        public bool Etat { get { return this.etat; } set { this.etat = value; } }
        public int Degre { get { return this.degre; } set { this.degre = value; } }

        //Méthode
        public void Affichage()
        {
            if(this.type!=null && this.etat!=null && this.degre!=null)
            {
                Console.WriteLine("Type : " + this.type + " Etat : " + this.etat + " Degré : " + this.degre);
            }
        }
    }
}
