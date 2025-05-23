public class RemedeMildiou : Remede
{
    public RemedeMildiou() : base("traitement Mildiou")
    {
    }

    public override void Agir(Plante plante)
    {
        //Création d'une liste temporaire pour stocker les maladies à retirer
        //C# ne supporte pas de modifier une liste dans un foreach
        List<Maladie> aRetirer = new List<Maladie>();
        foreach (Maladie maladie in plante.EstMaladeDe)
        {
            if (maladie.Nom == "Mildiou")
            {
                aRetirer.Add(maladie);
                plante.Sante = 100;
            }
        }
        foreach (Maladie maladie in aRetirer)
        {
            plante.EstMaladeDe.Remove(maladie);
        }
    }
}