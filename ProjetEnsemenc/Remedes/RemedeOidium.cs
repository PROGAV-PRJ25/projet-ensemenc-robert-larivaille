public class RemedeOidium : Remede
{
    public RemedeOidium() : base("traitement Oidium")
    {
    }

    public override void Agir (Plante plante)
    {
        foreach (Maladie maladie in plante.EstMaladeDe)
        {
            if (maladie.Nom == "Oidium")
            {
                plante.EstMaladeDe.Remove(maladie);
                plante.Sante = 100;
            }
        }
    }
}