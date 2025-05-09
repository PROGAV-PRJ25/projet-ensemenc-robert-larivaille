public abstract class Aubergine : PlanteVivace
{
    public Aubergine(Potager pot) : base(pot)
    {
        this.SaisondeSemis = Printemps;
        this.SaisondeRecolte = Ete;
        this.Espacement = 1;
        this.Comestible = true;
        this.QuotaCroissance = 20;
        this.EsperanceDeVie = 40;
        this.NbRecolte = 3;
        this.Taille = 1;
        this.TailleMax = 4;
        this.TempsCroissance = 3;
        this.BesoinEau = 5;
        this.SeuilHumidite = 50;
        this.NiveauHumidite = 50;
        this.SeuilLuminosite = 80;
        this.NiveauLuminosite = 80;
        this.TemperatureCible = new List<int> { 20, 28 };
        this.NiveauTemperature; //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Mildiou };
        this.ProbaMaladies = new int[] { 30 };
        this.Sante = 100;
        this.QteProduite = 2;

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

        message = $"Statuts Aubergine : Taille :{Taille}, Santé {Sante}";

        return message;
    }
}