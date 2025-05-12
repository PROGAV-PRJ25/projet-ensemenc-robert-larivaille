public abstract class Animaux
{
    Random rng = new Random();
    public string Nom { get; set; }
    public int X { get; set; }

    public int Y { get; set; }

    public int Duree { get; set; } // Combien de temps l'animal reste sur le potager au maximum

    public Potager Pot { get; set; }

    public int ProbaApparition { get; set; }
    public List<string> Predateurs { get; set; }

    public Animaux(int probaApparition, Potager pot, int duree)
    {
        Nom = "Animal";
        ProbaApparition = probaApparition;
        Pot = pot;
        Duree = duree;
        X = rng.Next(0, Pot.Hauteur);
        Y = rng.Next(0, Pot.Longueur);
        Predateurs = new List<string>();
    }

    public void Disparait()
    {
        X = -1; // Si l'animal est mort ou disparait on passe ses coordonnées à -1
        Y = -1;
    }

    public void EstMange()
    {
        if (Predateurs.Count != 0)
        {
            foreach (Animaux animal in Pot.ListeAnimaux)
            {
                if ((animal.X == X) && (animal.Y == Y))
                {
                    foreach (string predateur in Predateurs)
                    {
                        if (animal.Nom == predateur)
                        {
                            Disparait();
                        }
                    }
                }
            }
        }
    }

    public void SeDeplacer()
    {
        int direction = rng.Next(0, 4);
        if (direction == 0) //Nord
        {
            if (X != 0) X--;
            else X++;
        }
        else
        {
            if (direction == 1) //Sud
            {
                if (X != Pot.Hauteur - 1) X++;
                else X--;
            }
            else
            {
                if (direction == 2) //Est
                {
                    if (Y != Pot.Longueur - 1) Y++;
                    else Y--;
                }
                else //Ouest
                {
                    if (Y != 0) Y--;
                    else Y++;
                }
            }
        }
    }

    public abstract void Effet(Plante plante);
}
