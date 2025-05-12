public abstract class Poivron : PlanteAnnuelle
{
    public Poivron(int coorX, int coorY, Potager pot) : base(coorX, coorY, pot) // Selon caractéristiques du poivron rouge
    {
        this.Espece = "Poivron";
        this.SaisondeSemis = Hiver;
        this.SaisondeRecolte = Ete;
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
        this.MaladiesPotentielles = new List<Maladie> { Mildiou, Oidium };
        this.ProbaMaladies = new int[] { 35, 25 };
        this.Sante = 100;
        this.QteProduite = 6;

        if (TerrainPlant == "Terre")
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == "Sable")
        {
            this.ScoreTerrain = 90;
        }
        else if (TerrainPlant == "Calcaire")
        {
            this.ScoreTerrain = 60;
        }
        else
            this.ScoreTerrain = 20;

        // Il restera à initialiser les coordonnées

    }

    public override string ToString()
    {
        string message = base.ToString();

        message = $"Statuts Poivron : Taille :{Taille}, Santé {Sante}";

        return message;
    }
}