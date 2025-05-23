public class RemedeFusariose : Remede
{
    public RemedeFusariose() : base("traitement Fusariose")
    {

    }

    public override void Agir(Plante plante)
    {
        foreach (Maladie maladie in plante.EstMaladeDe)
        {
            if (maladie.Nom == "Fusariose")
            {
                plante.EstMaladeDe.Remove(maladie);
                plante.Sante = 100;
            }
        }
    }
}