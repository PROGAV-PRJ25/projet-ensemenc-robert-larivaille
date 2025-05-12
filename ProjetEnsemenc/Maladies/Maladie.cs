public abstract class Maladie
{
    public string Nom { get; set; }
    public int ProbaContamination { get; set; } //Valeur ajoutée à la proba de la plante d'attraper la maladie
    public int Criticite { get; set; }

    public Maladie(string nom)
    {
        Nom = nom;
    }
    public void EffetMaladie(Plante plante)
    {
        plante.Sante -= Criticite;
    }
}