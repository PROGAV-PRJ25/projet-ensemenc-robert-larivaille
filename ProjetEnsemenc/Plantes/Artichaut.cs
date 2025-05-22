public class Artichaut : PlanteVivace
{
    public Artichaut(int coorX, int coorY, Potager pot, Terrain ter, Simulation simu) : base(coorX, coorY, pot, ter, simu)
    {
        this.Espece = "Artichaut";
        this.SaisondeSemis = Saison.Printemps;
        this.SaisondeRecolte = Saison.Automne;
        this.Espacement = 3;
        this.TerrainPlant = ter;
        this.Comestible = true;
        this.QuotaCroissance = 10;
        this.EsperanceDeVie = 55;
        this.Taille = 1;
        this.TailleMax = 4;
        this.TempsCroissance = 3;
        this.BesoinEau = 6;
        this.SeuilHumidite = 60;
        this.NiveauHumidite = 60;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 15, 25 };
        this.MaladiesPotentielles = new List<Maladie> { new Mildiou("Mildiou") };
        this.ProbaMaladies = new List<int> { 15, 10 };
        this.Sante = 100;
        this.QteMaxProduite = 5;
        this.NbRecoltePossible = 2;

        if (TerrainPlant == Terrain.Terre)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Terrain.Argile)
        {
            this.ScoreTerrain = 90;
        }
        else if (TerrainPlant == Terrain.Sable)
        {
            this.ScoreTerrain = 80;
        }
        else
            this.ScoreTerrain = 50;
    }
    public Artichaut() : base()
    {
        this.SaisondeSemis = Saison.Printemps;
        //Pour faire fonctionner la méthode AssocierGrainePlante
    }

}