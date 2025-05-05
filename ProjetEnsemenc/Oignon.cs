public abstract class Oignon : PlanteAnnuelle
{
    public Oignon(Potager pot) : base(pot)
    {
        this.SaisondeSemis = Automne;
        this.SaisondeRecolte = Ete;
        this.Espacement = 0;
        this.Comestible = true;
        this.QuotaCroissance = 20;
        this.NbRecolte = 1;
        this.Taille = 1;
        this.TailleMax = 2;
        this.TempsCroissance = 3;
        this.BesoinEau = 5;
        this.SeuilHumidite = 70;
        this.NiveauHumidite = 70;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 5, 38 };
        this.NiveauTemperature; //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Mildiou };
        this.ProbaMaladies = new int[] { 20 };
        this.Sante = 100;
        this.QteProduite = 1;

        if (TerrainPlant == "Terre")
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == "Argile")
        {
            this.ScoreTerrain = 75;
        }
        else
            this.ScoreTerrain = 0; // A corriger

        // Il restera à initialiser les coordonnées

    }

    public override string ToString()
    {
        string message = base.ToString();

        message = $"Statuts Oignon : Taille :{Taille}, Santé {Sante}";

        return message;
    }
}