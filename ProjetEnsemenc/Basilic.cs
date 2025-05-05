public abstract class Basilic : PlanteAnnuelle
{
    public Basilic() : base()
    {
        this.SaisondeSemis = Printemps;
        this.SaisondeRecolte = Ete;
        this.TerrainPref = Terre;
        this.Espacement = 0;
        this.Comestible = true;
        this.QuotaCroissance = 18;
        this.FrequenceRecolte = 3;
        this.Taille = 1;
        this.TailleMax = 2;
        this.TempsCroissance = 3;
        this.BesoinEau = 5;
        this.SeuilHumidite = 60;
        this.NiveauHumidite = 60;
        this.SeuilLuminosite = 85;
        this.NiveauLuminosite = 85;
        this.TemperatureCible = new List<int> { 20, 25 };
        this.NiveauTemperature = //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Fusariose, Mildiou, Oidium };
        this.ProbaMaladies = new int[] { 35, 10, 20 };
        this.Sante = 100;
        this.QteProduite = 20;

        if (TerrainPlant == TerrainPref)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Sable)
        {
            this.ScoreTerrain = 85;
        }
        else if (TerrainPlant == Calcaire)
        {
            this.ScoreTerrain = 75;
        }
        else
            this.ScoreTerrain = 20;

        // Il restera à initialiser les coordonnées

    }
}