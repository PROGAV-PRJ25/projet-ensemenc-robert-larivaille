public abstract class PlanteAnnuelle : Plante
{
    public PlanteAnnuelle(Potager pot) : base(pot)
    {
        this.CoorX = CoorX;
        this.CoorY = CoorY;
        this.EsperanceDeVie = 12;

    }
}