public class Basilic : PlanteAnnuelle
{
    public Basilic(int coorY, int coorX, Potager pot, Terrain ter, Simulation simu) : base(coorY, coorX, pot, ter, simu)
    {
        this.Espece = "Basilic";
        this.SaisondeSemis = Saison.Printemps;
        this.SaisondeRecolte = Saison.Ete;
        this.TerrainPlant = ter;
        this.Espacement = 0;
        this.Comestible = true;
        this.QuotaCroissance = 18;
        this.NbRecoltePossible = 3;
        this.Taille = 1;
        this.TailleMax = 2;
        this.TempsCroissance = 3;
        this.BesoinEau = 5;
        this.SeuilHumidite = 60;
        this.NiveauHumidite = 60;
        this.SeuilLuminosite = 85;
        this.NiveauLuminosite = 85;
        this.TemperatureCible = new List<int> { 20, 25 };
        this.MaladiesPotentielles = new List<Maladie>
        {
            new Fusariose("Fusariose"),
            new Mildiou("Mildiou"),
            new Oidium("Oidium") };
        this.ProbaMaladies = new List<int> { 35, 10, 20 };
        this.Sante = 100;
        this.QteMaxProduite = 20;

        if (TerrainPlant == Terrain.Terre)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Terrain.Sable)
        {
            this.ScoreTerrain = 85;
        }
        else if (TerrainPlant == Terrain.Calcaire)
        {
            this.ScoreTerrain = 75;
        }
        else
            this.ScoreTerrain = 20;

    }
    public Basilic() : base()
    {
        this.SaisondeSemis = Saison.Printemps;
        //Pour faire fonctionner la méthode AssocierGrainePlante
    }
}