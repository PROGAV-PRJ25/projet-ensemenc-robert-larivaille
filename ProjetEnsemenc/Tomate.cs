public abstract class Tomate : PlanteAnnuelle
{
    public Tomate() : base()
    {
        this.SaisondeSemis = Printemps;
        this.SaisondeRecolte = Ete;
        this.TerrainPref = Terre;
        this.Espacement = 1;
        this.Comestible = true;
        this.QuotaCroissance = 30;
        this.FrequenceRecolte = 1;
        this.Taille = 1;
        this.TailleMax = 3;
        this.TempsCroissance = 4;
        this.BesoinEau = 10;
        this.SeuilHumidite = 80;
        this.NiveauHumidite = 80;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 15, 30 };
        this.NiveauTemperature = //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Mildiou, Oidiou }
        this.ProbaMaladies = new int[] { 50, 20 };
        this.Sante = 100;
        this.QteProduite = 30;

        if (TerrainPlant == TerrainPref)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Sable)
        {
            this.ScoreTerrain = 70;
        }
        else if (TerrainPlant == Calcaire)
        {
            this.ScoreTerrain = 60;
        }
        else
            this.ScoreTerrain = 50;

        // Il restera à initialiser les coordonnées

    }
}