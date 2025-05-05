public abstract class Tomate : PlanteAnnuelle
{
    public Tomate(Potager pot) : base(pot)
    {
        this.SaisondeSemis = Printemps;
        this.SaisondeRecolte = Ete;
        this.Espacement = 1;
        this.Comestible = true;
        this.QuotaCroissance = 30;
        this.NbRecolte = 3;
        this.Taille = 1;
        this.TailleMax = 3;
        this.TempsCroissance = 4;
        this.BesoinEau = 10;
        this.SeuilHumidite = 80;
        this.NiveauHumidite = 80;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 15, 30 };
        this.NiveauTemperature; //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Mildiou, Oidium };
        this.ProbaMaladies = new int[] { 50, 20 };
        this.Sante = 100;
        this.QteProduite = 30;

        if (TerrainPlant == "Terre")
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == "Sable")
        {
            this.ScoreTerrain = 70;
        }
        else if (TerrainPlant == "Calcaire")
        {
            this.ScoreTerrain = 60;
        }
        else
            this.ScoreTerrain = 50;

        // Il restera à initialiser les coordonnées

    }

    public override string ToString()
    {
        string message = base.ToString();

        message = $"Statuts Tomate : Taille :{Taille}, Santé {Sante}";

        return message;
    }
}