public abstract class PlanteVivace : Plante
{
    public PlanteVivace(int coorY, int coorX, Potager pot, Terrain ter, Simulation simu) : base(coorY, coorX, pot, ter, simu)
    {
        this.Pot = pot;
        this.CoorX = CoorX;
        this.CoorY = CoorY;
        this.TerrainPlant = ter;
    }
    public PlanteVivace() : base()
    {
        //Pour faire fonctionner la méthode AssocierGrainePlante
    }
}