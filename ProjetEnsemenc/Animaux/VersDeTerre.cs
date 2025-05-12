public class VersDeTerre : AnimauxBons
{
    public VersDeTerre(Potager pot) : base(80, pot, 3000)
    {
        this.Predateurs.Add("Oiseau");
        this.Predateurs.Add("Taupe");
        this.Nom = "Escargot";
    }

    public override void Effet(Plante plante)
    {
        plante.AmelioreTerrain();
    }
}