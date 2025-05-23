public class VersDeTerre : AnimauxBons
{
    public VersDeTerre(Potager pot) : base(20, pot, 3000)
    {
        this.Predateurs.Add("Oiseau");
        this.Nom = "VersDeTerre";
    }

    public override void Effet(Plante plante)
    {
        plante.AmelioreTerrain();
    }
}