public abstract class PlanteVivace : Plante
{
    public PlanteVivace(int coorX, int coorY, Potager pot, Terrain ter, Simulation simu) : base(coorX, coorY, pot, ter, simu)
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