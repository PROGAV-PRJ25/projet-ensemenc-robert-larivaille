public abstract class Oignon : PlanteBisannuelle
{
    public Oignon() : base()
    {
        this.SaisondeSemis = Automne;
        this.TerrainPref = Terre;
        this.Espacement = 0;
        this.Comestible = true;
        this.QuotaCroissance = 20;
        this.Taille = 1;
        this.TailleMax = 2;
        this.TempsCroissance = 3;
        this.BesoinEau = 5;
        this.SeuilHumidite = 70;
        this.NiveauHumidite = 70;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 5, 38 };
        this.NiveauTemperature = //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Mildiou };
        this.ProbaMaladies = new int[] { 20 };
        this.Sante = 100;
        this.QteProduite = 1;

        if (TerrainPlant == TerrainPref)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Argile)
        {
            this.ScoreTerrain = 75;
        }
        else
            this.ScoreTerrain = 0;

        // Il restera à initialiser les coordonnées

    }
}