public abstract class Plante
{
    public Potager Pot;
    public Saison SaisondeSemis { get; set; }
    public Saison SaisondeRecolte { get; set; }
    public int Espacement { get; set; }
    public bool Comestible { get; set; }
    public int QuotaCroissance { get; set; }
    public int Taille { get; set; } // à 1 par défaut
    public int TailleMax { get; set; }
    public int TempsCroissance { get; set; }
    public int BesoinEau { get; set; }
    public int NiveauHumidite { get; set; } //=seuil Humidité à la planta°
    public int SeuilHumidite { get; set; }
    public int NiveauLuminosite { get; set; } //=seuil Luminosité à la planta°
    public int SeuilLuminosite { get; set; }
    public List<int> TemperatureCible { get; set; }
    public int NiveauTemperature { get; set; }  // Celle du potager par défaut 
    public Maladie[] MaladiesPotentielles { get; set; }
    public int[] ProbaMaladies { get; set; }
    public int Sante { get; set; }  // à 100 par défaut
    public int EsperanceDeVie { get; set; } //En mois
    public List<Maladie> estMaladeDe { get; set; }

    public int QteProduite { get; set; } //Qté récupérée par récolte
    public int NbRecolte { get; set; } //Compris entre 1 et 3 -> Nb fois où l'on peut récolter dans la saison
    public int ScoreCondition { get; set; }
    public int ScoreTerrain { get; set; }

    // Faire en sorte que les objets et animaux sachent sur quelle plante ils sont plutôt
    // public List<Animaux> AnimauxPresents { get; set; }
    // public List<Achats> ObjetsPresents { get; set; }

    public int coorX;
    public int coorY;
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

    public Plante(Potager pot)
    {
        this.Pot = pot;
    }

    // Pour la méthode est Mangé -> utiliser if (instance is Classe)
    public void EstDetruit(Animaux animal)
    {
        if (animal is AnimauxDestructeurs)
        {
            CoorX = -1;
            CoorY = -1;
        }
    }

    public void EstMange(Animaux animal)
    {
        if (animal is AnimauxMangeurs)
        {
            Sante -= 5;
        }
    }

    public void EstMorte()
    {
        CoorX = -1;
        CoorY = -1;
    }

    public void EstMalade(Maladie maladie)
    {
        if (maladie is Maladie)
        {
            Sante -= 5; //Ajouter criticité ?
        }
    }
    public int CalculerScoreCondition()
    {
        int scoreEau;
        int scoreTemp;
        int scoreLum;

        // Gestion du respect des conditions d'humidité
        if (NiveauHumidite = SeuilHumidite)
        {
            scoreEau = 100;
        }
        else
        {
            int differenceEau = Math.Abs(NiveauHumidite - SeuilHumidite);
            scoreEau = Math.Max(0, 100 - (differenceEau * 2)); // Réduit le score de 2 points par unité d'écart
        }

        // Gestion du respect des conditions de luminosité
        if (NiveauLuminosite = SeuilLuminosite)
        {
            scoreLum = 100;
        }
        else
        {
            int differenceLum = Math.Abs(NiveauLuminosite - SeuilLuminosite);
            scoreLum = Math.Max(0, 100 - (differenceLum * 2)); // Réduit le score de 2 points par unité d'écart
        }

        // Gestion du respect des conditions de température
        int tempCible = (TemperatureCible(1) + TemperatureCible(0)) / 2; //On prend le milieu de la zone de température comme référentiel
        if (NiveauTemperature = tempCible)
        {
            scoreTemp = 100;
        }
        else
        {
            int differenceTemp = Math.Abs(NiveauTemperature - tempCible);
            scoreTemp = Math.Max(0, 100 - (differenceTemp * 2)); // Réduit le score de 2 points par unité d'écart
        }
        ScoreCondition = ScoreTerrain + scoreEau + scoreTemp + scoreLum;
        return ScoreCondition;
    }

    public void ImpactConditions()
    {
        if (CalculerScoreCondition() < 200)
        {
            EstMorte();
        }
        else if ((CalculerScoreCondition() >= 200) && (CalculerScoreCondition() < 250))
        {
            QteProduite /= 2;
        }
        else if (CalculerScoreCondition() >= 350)
            QteProduite *= 2;
    }

    public override string ToString()
    {
        string message;
        message = $"Statuts {Plante} : Taille :{Taille}, Santé {Sante}";
        if (CalculerScoreCondition() < 250)
        {
            message += "-- Mauvaises conditions - Perte de production --";
        }
        if ((CoorX = -1) && (CoorY = -1))
        {
            message = "-- Plante Morte --";
        }
        return message;
    }
}