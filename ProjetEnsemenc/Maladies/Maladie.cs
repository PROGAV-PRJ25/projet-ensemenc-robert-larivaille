public abstract class Maladie
{
    public string Nom { get; set; }
    protected int ProbaContamination { get; set; } //Valeur ajoutée à la proba de la plante d'attraper la maladie
    protected int Criticite { get; set; }

    public Maladie(string nom)
    {
        Nom = nom;
    }
    public void EffetMaladie(Plante plante)
    {
        plante.Sante -= Criticite;
    }
}