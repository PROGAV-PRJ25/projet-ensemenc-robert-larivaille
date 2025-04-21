public abstract class PlanteBisannuelle : Plante
{
    public Saison SaisondeRecolte { get; set; }
    public PlanteBisannuelle() : base()
    {
        this.EsperanceDeVie = 12;
        this.FrequenceRecolte = 6;
    }
}