public class RemedeMildiou : Remede
{
    public RemedeMildiou() : base("traitement Mildiou")
    {
    }

    public override void Agir(Plante plante)
    {
        foreach (Maladie maladie in plante.EstMaladeDe)
        {
            if (maladie.Nom == "Mildiou")
            {
                plante.EstMaladeDe.Remove(maladie);
                plante.Sante = 100;
            }
        }
    }
}