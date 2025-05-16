public abstract class PlanteVivace : Plante
{
    public PlanteVivace(int coorX, int coorY, Potager pot, Terrain ter) : base(coorX, coorY, pot, ter)
    {
        this.Pot = pot;
        this.CoorX = CoorX;
        this.CoorY = CoorY;
        this.TerrainPlant = ter;
    }
}