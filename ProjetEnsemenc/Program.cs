Console.WriteLine("Déterminez la hauteur du potager");
int hauteur;
while (!int.TryParse(Console.ReadLine(), out hauteur))
{
    Console.WriteLine("Veuillez entrer un nombre entier valide.");
}

Console.WriteLine("Déterminez la largeur du potager");
int largeur;
while (!int.TryParse(Console.ReadLine(), out largeur))
{
    Console.WriteLine("Veuillez entrer un nombre entier valide.");
}


Simulation simu = new Simulation(hauteur, largeur);
simu.Simuler(simu.Pot, simu);