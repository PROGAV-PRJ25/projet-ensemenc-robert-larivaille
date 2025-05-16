public class Poivron : PlanteAnnuelle
{
    public Poivron(int coorX, int coorY, Potager pot, Terrain ter) : base(coorX, coorY, pot, ter) // Selon caractéristiques du poivron rouge
    {
        this.Espece = "Poivron";
        this.SaisondeSemis = Saison.Hiver;
        this.SaisondeRecolte = Saison.Ete;
        this.TerrainPlant = ter;
        this.Espacement = 1;
        this.Comestible = true;
        this.QuotaCroissance = 15;
        this.NbRecolte = 3;
        this.Taille = 1;
        this.TailleMax = 3;
        this.TempsCroissance = 2;
        this.BesoinEau = 10;
        this.SeuilHumidite = 80;
        this.NiveauHumidite = 80;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 20, 28 };
        this.MaladiesPotentielles = new List<Maladie>
        {
            new Mildiou("Mildiou"),
            new Oidium("Oidium")
        };
        this.ProbaMaladies = new List<int> { 35, 25 };
        this.Sante = 100;
        this.QteProduite = 6;

        if (TerrainPlant == Terrain.Terre)
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == Terrain.Sable)
        {
            this.ScoreTerrain = 90;
        }
        else if (TerrainPlant == Terrain.Calcaire)
        {
            this.ScoreTerrain = 60;
        }
        else
            this.ScoreTerrain = 20;

        // Il restera à initialiser les coordonnées

    }
}