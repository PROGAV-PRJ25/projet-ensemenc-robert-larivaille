public class Olivier : PlanteVivace
{

    public Olivier(int coorX, int coorY, Potager pot, Terrain ter, Simulation simu) : base(coorX, coorY, pot, ter, simu)
    {
        this.Espece = "Olivier";
        this.SaisondeSemis = Saison.Automne;
        this.SaisondeRecolte = Saison.Automne;
        this.TerrainPlant = ter;
        this.Espacement = 14;
        this.Comestible = true;
        this.QuotaCroissance = 8;
        this.EsperanceDeVie = 200;
        this.NbRecoltePossible = 1;
        this.Taille = 1;
        this.TailleMax = 5;
        this.TempsCroissance = 12;
        this.BesoinEau = 5;
        this.SeuilHumidite = 40;
        this.NiveauHumidite = 40;
        this.SeuilLuminosite = 85;
        this.NiveauLuminosite = 85;
        this.TemperatureCible = new List<int> { 20, 30 };
        this.MaladiesPotentielles = new List<Maladie> { new Mildiou("Mildiou") };
        this.ProbaMaladies = new List<int> { 40 };
        this.Sante = 100;
        this.QteMaxProduite = 5000;

        if (TerrainPlant == Terrain.Terre)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Terrain.Calcaire)
        {
            this.ScoreTerrain = 90;
        }
        else if (TerrainPlant == Terrain.Sable)
        {
            this.ScoreTerrain = 80;
        }
        else
            this.ScoreTerrain = 40;

    }
    public Olivier() : base()
    {
        this.SaisondeSemis = Saison.Automne;
        //Pour faire fonctionner la méthode AssocierGrainePlante
    }
}