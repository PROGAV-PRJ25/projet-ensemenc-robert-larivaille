public abstract class Intemperie
{
    public Potager Pot { get; set; }
    protected int Numero { get; set; }
    public int Duree { get; set; }

    public Intemperie(Potager pot)
    {
        Pot = pot;
    }
    public abstract void EffetIntemperie();
}