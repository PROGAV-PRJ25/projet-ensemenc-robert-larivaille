public abstract class PlanteVivace : Plante
{
    public PlanteVivace(int coorX, int coorY, Potager pot) : base(coorX, coorY, pot)
    {
        this.Potager = pot;
        this.CoorX = CoorX;
        this.CoorY = CoorY;
    }
}