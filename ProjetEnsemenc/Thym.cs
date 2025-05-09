public abstract class Thym : PlanteVivace
{
    public Thym(Potager pot) : base(pot)
    {
        this.SaisondeSemis = Printemps;
        this.SaisondeRecolte = Ete;
        this.Espacement = 1;
        this.Comestible = true;
        this.QuotaCroissance = 30;
        this.EsperanceDeVie = 44;
        this.NbRecolte = 2;
        this.Taille = 1;
        this.TailleMax = 2;
        this.TempsCroissance = 3;
        this.BesoinEau = 5;
        this.SeuilHumidite = 50;
        this.NiveauHumidite = 50;
        this.SeuilLuminosite = 90;
        this.NiveauLuminosite = 90;
        this.TemperatureCible = new List<int> { 15, 25 };
        this.NiveauTemperature; //Insérer Température Potager
        this.MaladiesPotentielles = new List<Maladie> { Oidium };
        this.ProbaMaladies = new int[] { 15 };
        this.Sante = 100;
        this.QteProduite = 20;

        if (TerrainPlant == "Calcaire")
        {
            this.ScoreTerrain = 100;
        }
        else if (TerrainPlant == "Sable")
        {
            this.ScoreTerrain = 90;
        }
        else if (TerrainPlant == "Terre")
        {
            this.ScoreTerrain = 80;
        }
        else
            this.ScoreTerrain = 50;

        // Il restera à initialiser les coordonnées

    }

    public override string ToString()
    {
        string message = base.ToString();

        message = $"Statuts Thym : Taille :{Taille}, Santé {Sante}";

        return message;
    }
}