public abstract class PlanteAnnuelle : Plante
{
    public Saison SaisondeRecolte { get; set; }
    public PlanteAnnuelle() : base()
    {
        this.EsperanceDeVie = 12;
    }
}