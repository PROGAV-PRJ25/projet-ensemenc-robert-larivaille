public abstract class Remede
{
    public string Nom { get; set; }
    public Remede(string nom)
    {
        Nom = nom;
    }
    public abstract void Agir(Plante plante);
}