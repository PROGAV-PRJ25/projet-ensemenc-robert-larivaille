public class Tomate : PlanteAnnuelle
{
    public Tomate(int coorY, int coorX, Potager pot, Terrain ter, Simulation simu) : base(coorY, coorX, pot, ter, simu)
    {
        this.Espece = "Tomate";
        this.SaisondeSemis = Saison.Printemps;
        this.SaisondeRecolte = Saison.Ete;
        this.TerrainPlant = ter;
        this.Espacement = 1;
        this.Comestible = true;
        this.QuotaCroissance = 30;
        this.NbRecoltePossible = 3;
        this.Taille = 1;
        this.TailleMax = 3;
        this.TempsCroissance = 4;
        this.BesoinEau = 10;
        this.SeuilHumidite = 80;
        this.NiveauHumidite = 80;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 15, 30 };
        this.MaladiesPotentielles = new List<Maladie>
        {
            new Mildiou("Mildiou"),
            new Oidium("Oidium")
        };
        this.ProbaMaladies = new List<int> { 50, 20 };
        this.Sante = 100;
        this.QteMaxProduite = 30;

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
    }
    public Tomate() : base()
    {
        this.SaisondeSemis = Saison.Printemps;
        //Pour faire fonctionner la méthode AssocierGrainePlante
    }
}