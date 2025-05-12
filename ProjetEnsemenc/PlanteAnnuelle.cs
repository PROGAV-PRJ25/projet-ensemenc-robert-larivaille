public abstract class PlanteAnnuelle : Plante
{
    public PlanteAnnuelle(int coorX, int coorY, Potager pot) : base(coorX, coorY, pot)
    {
        this.Potager = pot;
        this.CoorX = CoorX;
        this.CoorY = CoorY;
        this.EsperanceDeVie = 12;

    }
}