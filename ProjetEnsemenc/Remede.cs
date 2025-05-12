public class Remede
{
    public string Nom { get; set; }
    public List<string> AgitSur { get; set; }
    public Remede(string nom)
    {
        Nom = nom;
        AgitSur = new List<string>();
    }
    public void Agir(Maladie maladie, Plante plante)
    {

    }
}