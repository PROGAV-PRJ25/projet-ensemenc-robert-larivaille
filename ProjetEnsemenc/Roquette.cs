public abstract class Roquette : PlanteAnnuelle
{
    public Roquette(Potager pot) : base(pot)
    {
        this.SaisondeSemis = Ete;
        this.SaisondeRecolte = Automne;
        this.Espacement = 0;
        this.Comestible = true;
        this.QuotaCroissance = 25;
        this.NbRecolte = 3;
        this.Taille = 1;
        this.TailleMax = 3;
        this.TempsCroissance = 2;
        this.BesoinEau = 5;
        this.SeuilHumidite = 65;
        this.NiveauHumidite = 65;
        this.SeuilLuminosite = 70;
        this.NiveauLuminosite = 70;
        this.TemperatureCible = new List<int> { 10, 20 };
        this.NiveauTemperature; //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Mildiou };
        this.ProbaMaladies = new int[] { 30, 15 };
        this.Sante = 100;
        this.QteProduite = 10;

        if (TerrainPlant == "Terre")
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == "Sable")
        {
            this.ScoreTerrain = 80;
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

        message = $"Statuts Roquette : Taille :{Taille}, Santé {Sante}";

        return message;
    }
}