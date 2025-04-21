public abstract class Artichaut : PlanteVivace
{
    public Artichaut() : base()
    {
        this.SaisondeSemis = Printemps;
        this.TerrainPref = Terre;
        this.Espacement = 3;
        this.Comestible = true;
        this.QuotaCroissance = 10;
        this.EsperanceDeVie = 55;
        this.FrequenceRecolte = 6;
        this.Taille = 1;
        this.TailleMax = 4;
        this.TempsCroissance = 3;
        this.BesoinEau = 6;
        this.SeuilHumidite = 60;
        this.NiveauHumidite = 60;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 15, 25 };
        this.NiveauTemperature = //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Mildiou, Pucerons }
        this.ProbaMaladies = new int[] { 15, 10 };
        this.Sante = 100;
        this.QteProduite = 5;

        if (TerrainPlant == TerrainPref)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Argile)
        {
            this.ScoreTerrain = 90;
        }
        else if (TerrainPlant == Sable)
        {
            this.ScoreTerrain = 80;
        }
        else
            this.ScoreTerrain = 50;

        // Il restera à initialiser les coordonnées

    }
}