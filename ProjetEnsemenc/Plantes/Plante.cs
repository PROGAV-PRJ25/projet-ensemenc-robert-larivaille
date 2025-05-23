
using System.Globalization;
public abstract class Plante
{
    public string Espece { get; set; }
    protected Potager Pot;
    protected Terrain TerrainPlant { get; set; }
    public Saison SaisondeSemis { get; set; }
    public Saison SaisondeRecolte { get; set; }
    public int TourPlantation { get; set; }
    public int Espacement { get; set; }
    protected bool Comestible { get; set; }
    protected int QuotaCroissance { get; set; }
    public int Taille { get; set; } // à 1 par défaut
    public int TailleMax { get; set; }
    public int TempsCroissance { get; set; }
    public int BesoinEau { get; set; }
    public int NiveauHumidite { get; set; } // Optimal à la plantation
    public int SeuilHumidite { get; set; }
    public int NiveauLuminosite { get; set; } //Optimal à la plantation
    public int SeuilLuminosite { get; set; }
    public List<int> TemperatureCible { get; set; }
    public int NiveauTemperature { get; set; }  // Celle du potager par défaut 
    protected List<Maladie> MaladiesPotentielles { get; set; }
    protected List<int> ProbaMaladies { get; set; }
    public int Sante { get; set; }  // à 100 par défaut
    public int EsperanceDeVie { get; set; } //En mois
    public List<Maladie> EstMaladeDe { get; set; } = new List<Maladie>();
    protected int QteMaxProduite { get; set; }
    private int QteProduite { get; set; }
    public int NbRecoltePossible { get; set; } //Compris entre 1 et 3 -> Nb fois où l'on peut récolter dans la saison
    public int NbRecolte { get; set; }
    private int ScoreCondition { get; set; }
    protected int ScoreTerrain { get; set; }

    private List<Plante> PlantesAutour = new List<Plante>();
    private int coorX; //Ligne
    private int coorY; //Colonne
    public int CoorX
    {
        get { return coorX; }
        set
        {
            coorX = value;
        }
    }
    public int CoorY
    {
        get { return coorY; }
        set
        {
            coorY = value;
        }
    }
#pragma warning disable CS8618
    public Plante(int y, int x, Potager pot, Terrain terrain, Simulation simu)
    {
        Pot = pot;
        CoorX = x;
        CoorY = y;
        NiveauTemperature = Pot.Temperature;
        TerrainPlant = terrain;
        NbRecolte = 0;
        TourPlantation = simu.NumeroTour;
        QteProduite = 0;
        ProbaMaladies = new List<int>();
        MaladiesPotentielles = new List<Maladie>();
        TemperatureCible = new List<int>();
    }
    public Plante() { } //Pour faire fonctionner la méthode AssocierGrainePlante

#pragma warning restore CS8618

    public void MettreAJourPlantesAutour()
    {
        PlantesAutour.Clear();
        foreach (Plante plante in Pot.ListePlantes)
        {
            if (plante != this) // pour ne pas ajouter la plante elle-même
            {
                int distancex = Math.Abs(plante.CoorX - this.CoorX);
                int distancey = Math.Abs(plante.CoorY - this.CoorY);
                if (distancex <= 1 && distancey <= 1)
                {
                    PlantesAutour.Add(plante);
                }
            }
        }
    }

    private void CalculerQteProduite()
    {
        int pas;
        pas = QteMaxProduite / TailleMax;
        if (Taille == 1) QteProduite = 0;
        if (Taille == 2) QteProduite = pas;
        if (Taille == 3) QteProduite = 2 * pas;
        if (Taille == 4) QteProduite = 3 * pas;
        if (Taille == TailleMax) QteProduite = QteMaxProduite;
    }

    public void DonnerRecolte(Potager pot, Recolte recolte)
    {
        Console.WriteLine($"Voulez-vous récolter {Espece} ? (Oui ou Non)");
        string reponse = Console.ReadLine()!;
        ChoixOuiNon choix;
        while (!Enum.TryParse<ChoixOuiNon>(reponse, true, out choix) || int.TryParse(reponse, out _))
        {
            Console.WriteLine("Entrée invalide. Veuillez saisir un choix valide : Oui, Non");
            reponse = Console.ReadLine()!;
        }
        if (choix == ChoixOuiNon.Oui)
        {
            CalculerQteProduite();
            recolte.Quantite += QteProduite;
            NbRecolte++;
            this.EstMorte();
        }
    }

    private int CalculerQuotaCroissance()
    {
        int quota = 0;
        foreach (Plante plante in PlantesAutour)
        {
            quota += plante.Taille;
        }
        return quota;
    }

    public void Contamination()
    {
        Random rng = new Random();
        int proba;
        if (EstMaladeDe.Count() != 0) // SI la liste n'est pas vide
        {
            foreach (Plante plante in PlantesAutour)
            {
                for (int i = 0; i < EstMaladeDe.Count(); i++) // On parcours la liste de maladies que la plante a
                {
                    for (int j = 0; j < plante.MaladiesPotentielles.Count(); j++) // Et la liste de maladie que chaque plante autour peut attraper
                    {
                        if (EstMaladeDe[i] == plante.MaladiesPotentielles[j]) //on les compare et si c'est les même
                        {
                            proba = rng.Next(0, 101); // on tire au hasard un chiffre ente 1 et 100
                            if (proba < plante.ProbaMaladies[j]) // Si celui-ci est entre 0 et la proba d'attraper la maladie
                            {
                                plante.AttraperMaladie(EstMaladeDe[i]); //La plante devient malade
                            }
                        }
                    }

                }
            }
        }
    }

    public void Grandir()
    {
        if (QuotaCroissance >= CalculerQuotaCroissance())
        {
            if (Taille < TailleMax)
            {
                Taille += 1;
            }
        }
        else
            Console.WriteLine($"Il y a trop de plantes autour pour que {Espece} grandisse");
    }

    public void EstMange()
    {
        Sante -= 5;
    }

    public void EstMorte()
    {
        CoorX = -1;
        CoorY = -1;
    }

    public void AmelioreSante()
    {
        Sante += 5;
    }

    public void AmelioreTerrain()
    {
        ScoreTerrain += 5;
    }

    public void Fertilise()
    {
        double augmentation = 1.10 * Convert.ToDouble(QteMaxProduite);
        QteMaxProduite = Convert.ToInt32(augmentation);
    }

    private int CalculerScoreCondition()
    {
        int scoreEau;
        int scoreTemp;
        int scoreLum;

        // Gestion du respect des conditions d'humidité
        if (NiveauHumidite == SeuilHumidite)
        {
            scoreEau = 100;
        }
        else
        {
            int differenceEau = Math.Abs(NiveauHumidite - SeuilHumidite);
            scoreEau = Math.Max(0, 100 - (differenceEau * 5)); // Réduit le score de 5 points par unité d'écart
        }

        // Gestion du respect des conditions de luminosité
        if (NiveauLuminosite == SeuilLuminosite)
        {
            scoreLum = 100;
        }
        else
        {
            int differenceLum = Math.Abs(NiveauLuminosite - SeuilLuminosite);
            scoreLum = Math.Max(0, 100 - (differenceLum * 5)); // Réduit le score de 5 points par unité d'écart
        }

        // Gestion du respect des conditions de température

        int tempCible = (TemperatureCible[1] + TemperatureCible[0]) / 2; //On prend le milieu de la zone de température comme référentiel
        if (NiveauTemperature == tempCible)
        {
            scoreTemp = 100;
        }
        else
        {
            int differenceTemp = Math.Abs(NiveauTemperature - tempCible);
            scoreTemp = Math.Max(0, 100 - (differenceTemp * 5)); // Réduit le score de 5 points par unité d'écart
        }
        ScoreCondition = ScoreTerrain + scoreEau + scoreTemp + scoreLum;
        // Console.WriteLine($"Score Luminosité: {scoreLum}");
        // Console.WriteLine($"Score Eau : {scoreEau}");
        // Console.WriteLine($"Score Terrain : {ScoreTerrain}");
        // Console.WriteLine($"Score Eau : {scoreEau}");
        // Console.WriteLine($"--  Score Conditions : {ScoreCondition} -- ");
        return ScoreCondition;
    }

    public void ImpactConditions()
    {
        foreach (Maladie maladie in EstMaladeDe)
        {
            maladie.EffetMaladie(this);
        }
        int score = CalculerScoreCondition();
        if ((score < 200) || (Sante < 40))
        {
            EstMorte();
        }
        else if ((score >= 200) && (score < 250))
        {
            QteProduite /= 2;
        }
        else if (score >= 350)
            QteProduite *= 2;
    }
    private void AttraperMaladie(Maladie maladie)
    {
        if (MaladiesPotentielles.Contains(maladie) && !EstMaladeDe.Contains(maladie))
        {
            EstMaladeDe.Add(maladie);
            Console.WriteLine($"{Espece} a attrapé {maladie.Nom} !");
        }
    }

    public void ProbabiliteTomberMalade()
    {
        Random rng = new Random();
        int proba = rng.Next(0, 101);
        for (int i = 0; i < MaladiesPotentielles.Count(); i++)
        {
            if (proba <= ProbaMaladies[i])
            {
                AttraperMaladie(MaladiesPotentielles[i]);
            }
        }
    }

    public override string ToString()
    {
        string message;
        message = $"Statuts {Espece} : Taille :{Taille}, Santé {Sante}\n Conditions : Humidité {NiveauHumidite}, Luminosité {NiveauLuminosite}, Température : {NiveauTemperature}";
        if (EstMaladeDe.Count() != 0)
        {
            message += $"- Maladies : {string.Join(", ", EstMaladeDe.Select(m => m.Nom))}";
        }
        if (CalculerScoreCondition() < 250)
        {
            message += "-- Mauvaises conditions - Perte de production --";
        }
        if ((CoorX == -1) && (CoorY == -1))
        {
            message = $"-- {Espece} Morte --";
        }
        return message;
    }
}
