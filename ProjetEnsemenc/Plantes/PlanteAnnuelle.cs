public abstract class PlanteAnnuelle : Plante
{
    public PlanteAnnuelle(int coorX, int coorY, Potager pot, Terrain ter, Simulation simu) : base(coorX, coorY, pot, ter, simu)
    {
        this.Pot = pot;
        this.CoorX = CoorX;
        this.CoorY = CoorY;
        this.TerrainPlant = ter;
        this.EsperanceDeVie = 12;
    }
}