public class Thym : PlanteVivace
{
    public Thym(int coorX, int coorY, Potager pot, Terrain ter, Simulation simu) : base(coorX, coorY, pot, ter, simu)
    {
        this.Espece = "Thym";
        this.SaisondeSemis = Saison.Printemps;
        this.SaisondeRecolte = Saison.Ete;
        this.TerrainPlant = ter;
        this.Espacement = 1;
        this.Comestible = true;
        this.QuotaCroissance = 30;
        this.EsperanceDeVie = 44;
        this.NbRecoltePossible = 2;
        this.Taille = 1;
        this.TailleMax = 2;
        this.TempsCroissance = 3;
        this.BesoinEau = 5;
        this.SeuilHumidite = 50;
        this.NiveauHumidite = 50;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 15, 25 };
        this.MaladiesPotentielles = new List<Maladie> { new Oidium("Oidium") };
        this.ProbaMaladies = new List<int> { 15 };
        this.Sante = 100;
        this.QteMaxProduite = 20;

        if (TerrainPlant == Terrain.Calcaire)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Terrain.Sable)
        {
            this.ScoreTerrain = 90;
        }
        else if (TerrainPlant == Terrain.Terre)
        {
            this.ScoreTerrain = 80;
        }
        else
            this.ScoreTerrain = 50;

    }
    public Thym() : base()
    {
        this.SaisondeSemis = Saison.Printemps;
        //Pour faire fonctionner la méthode AssocierGrainePlante
    }
}