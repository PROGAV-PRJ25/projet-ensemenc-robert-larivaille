public abstract class PlanteAnnuelle : Plante
{
    public PlanteAnnuelle(int coorY, int coorX, Potager pot, Terrain ter, Simulation simu) : base(coorY, coorX, pot, ter, simu)
    {
        this.Pot = pot;
        this.CoorX = CoorX;
        this.CoorY = CoorY;
        this.TerrainPlant = ter;
        this.EsperanceDeVie = 12;
    }
    public PlanteAnnuelle() : base()
    {
        //Pour faire fonctionner la méthode AssocierGrainePlante
    }
}