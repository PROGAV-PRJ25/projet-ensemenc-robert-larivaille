public abstract class Plante
{
    public Saison SaisondeSemis { get; set; }
    public Terrain TerrainPref { get; set; }
    public Terrain TerrainPlant { get; set; }
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

    public int QteProduite { get; set; }
    public int FrequenceRecolte { get; set; }
    public int ScoreCondition { get; set; }
    public int[] ScoreTerrain { get; set; }
    public List<Animaux> AnimauxPresents { get; set; }
    public List<Achats> ObjetsPresents { get; set; }

    public int CoorX { get; set; }
    public int CoorY { get; set; }

    public Plante()
    {

    }

}