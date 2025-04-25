public class VersDeTerre : AnimauxBons
{
    private static int numero=1;
    public VersDeTerre ( Potager pot) : base(  80, pot , 3000)
    {
        this.Predateurs.Add("Oiseau");
        this.Predateurs.Add("Taupe");
        numero++;
        this.Nom="Escargot" + ToString(numero);
    }

    
}