public abstract class Olivier : PlanteVivace
{
    public Olivier() : base()
    {
        this.SaisondeSemis = Automne;
        this.TerrainPref = Terre;
        this.Espacement = 14; //7m entre chaque olivier
        this.Comestible = true;
        this.QuotaCroissance = 8;
        this.EsperanceDeVie = 200; //50 ans d'espérance de vie
        this.FrequenceRecolte = 12; // 1x par an
        this.Taille = 1;
        this.TailleMax = 5;
        this.TempsCroissance = 12; //1 an pour passer à la taille supérieure
        this.BesoinEau = 5; // 5 au départ puis quand taille 3 atteinte passe à 2
        this.SeuilHumidite = 40;
        this.NiveauHumidite = 40;
        this.SeuilLuminosite = 85;
        this.NiveauLuminosite = 85;
        this.TemperatureCible = new List<int> { 20, 30 };
        this.NiveauTemperature = //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Mouches }
        this.ProbaMaladies = new int[] { 40 };
        this.Sante = 100;
        this.QteProduite = 5000; //5000 olives par an

        if (TerrainPlant == TerrainPref)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Calcaire)
        {
            this.ScoreTerrain = 90;
        }
        else if (TerrainPlant == Sable)
        {
            this.ScoreTerrain = 80;
        }
        else
            this.ScoreTerrain = 40;

        // Il restera à initialiser les coordonnées

    }
}