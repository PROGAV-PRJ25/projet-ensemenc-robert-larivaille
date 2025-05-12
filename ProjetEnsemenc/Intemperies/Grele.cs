public class Grele : Intemperie
{
    public Grele(Potager pot) : base(pot)
    {
        this.Numero = 3;
        this.Duree = 4;
    }

    public override void EffetIntemperie()
    {
        int nombrePlantes = 0;
        foreach (Plante plante in Pot.ListePlantes)
        {
            if ((plante.Taille == 1) && (nombrePlantes < 2))
            {
                plante.EstMorte();
                nombrePlantes++;
            }
        }
    }
}