public class Roquette : PlanteAnnuelle
{
    public Roquette(int coorX, int coorY, Potager pot, Terrain ter, Simulation simu) : base(coorX, coorY, pot, ter, simu)
    {
        this.Espece = "Roquette";
        this.SaisondeSemis = Saison.Ete;
        this.SaisondeRecolte = Saison.Automne;
        this.TerrainPlant = ter;
        this.Espacement = 0;
        this.Comestible = true;
        this.QuotaCroissance = 25;
        this.NbRecoltePossible = 3;
        this.Taille = 1;
        this.TailleMax = 3;
        this.TempsCroissance = 2;
        this.BesoinEau = 5;
        this.SeuilHumidite = 65;
        this.NiveauHumidite = 65;
        this.SeuilLuminosite = 70;
        this.NiveauLuminosite = 70;
        this.TemperatureCible = new List<int> { 10, 20 };
        this.MaladiesPotentielles = new List<Maladie> { new Mildiou("Mildiou") };
        this.ProbaMaladies = new List<int> { 30, 15 };
        this.Sante = 100;
        this.QteMaxProduite = 10;

        if (TerrainPlant == Terrain.Terre)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Terrain.Sable)
        {
            this.ScoreTerrain = 80;
        }
        else if (TerrainPlant == Terrain.Calcaire)
        {
            this.ScoreTerrain = 60;
        }
        else
            this.ScoreTerrain = 50;
    }
    public Roquette() : base()
    {
        this.SaisondeSemis = Saison.Ete;
        //Pour faire fonctionner la méthode AssocierGrainePlante
    }
}