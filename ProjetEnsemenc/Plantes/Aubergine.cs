public class Aubergine : PlanteVivace
{
    public Aubergine(int coorX, int coorY, Potager pot, Terrain ter) : base(coorX, coorY, pot, ter)
    {
        this.Espece = "Aubergine";
        this.SaisondeSemis = Saison.Printemps;
        this.SaisondeRecolte = Saison.Ete;
        this.TerrainPlant = ter;
        this.Espacement = 1;
        this.Comestible = true;
        this.QuotaCroissance = 20;
        this.EsperanceDeVie = 40;
        this.NbRecoltePossible = 3;
        this.Taille = 1;
        this.TailleMax = 4;
        this.TempsCroissance = 3;
        this.BesoinEau = 5;
        this.SeuilHumidite = 50;
        this.NiveauHumidite = 50;
        this.SeuilLuminosite = 80;
        this.NiveauLuminosite = 80;
        this.TemperatureCible = new List<int> { 20, 28 };
        this.MaladiesPotentielles = new List<Maladie> { new Mildiou("Mildiou") };
        this.ProbaMaladies = new List<int> { 30 };
        this.Sante = 100;
        this.QteProduite = 2;

        if (TerrainPlant == Terrain.Terre)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Terrain.Sable)
        {
            this.ScoreTerrain = 70;
        }
        else if (TerrainPlant == Terrain.Calcaire)
        {
            this.ScoreTerrain = 60;
        }
        else
            this.ScoreTerrain = 50;

        // Il restera à initialiser les coordonnées

    }

}