public abstract class Remede
{
    public string Nom { get; set; }
    public List<string> AgitSur { get; set; }
    public Remede(string nom)
    {
        Nom = nom;
        AgitSur = new List<string>();
    }
    public abstract void Agir(Maladie maladie, Plante plante);
}