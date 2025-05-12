public abstract class Basilic : PlanteAnnuelle
{
    public Basilic(int coorX, int coorY, Potager pot) : base(coorX, coorY, pot)
    {
        this.Espece = "Basilic";
        this.SaisondeSemis = Printemps;
        this.SaisondeRecolte = Ete;
        this.Espacement = 0;
        this.Comestible = true;
        this.QuotaCroissance = 18;
        this.NbRecolte = 3;
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
        this.ProbaMaladies = new int[] { 35, 10, 20 };
        this.Sante = 100;
        this.QteProduite = 20;

        if (TerrainPlant == "Terre")
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == "Sable")
        {
            this.ScoreTerrain = 85;
        }
        else if (TerrainPlant == "Calcaire")
        {
            this.ScoreTerrain = 75;
        }
        else
            this.ScoreTerrain = 20;

        // Il restera à initialiser les coordonnées

    }
    public override string ToString()
    {
        string message = base.ToString();

        message = $"Statuts Basilic : Taille :{Taille}, Santé {Sante}";

        return message;
    }
}