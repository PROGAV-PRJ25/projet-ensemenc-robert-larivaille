Console.WriteLine("Déterminez la hauteur du potager");
int hauteur = -1;
string inputHauteur;
bool hauteurValide = false;
while (!hauteurValide)
{
    inputHauteur = Console.ReadLine()!;
    if (!int.TryParse(inputHauteur, out hauteur))
        Console.WriteLine("Veuillez entrer un nombre entier valide.");
    else if (hauteur > 12)
        Console.WriteLine("Vous ne pouvez pas entrer une hauteur supérieure à 12");
    else if (hauteur <= 0)
        Console.WriteLine("Vous ne pouvez pas entrer une hauteur inférieure 1");
    else
        hauteurValide = true;
}


Console.WriteLine("Déterminez la largeur du potager");
int largeur = -1;
string inputLargeur;
bool largeurValide = false;
while (!largeurValide)
{
    inputLargeur = Console.ReadLine()!;
    if (!int.TryParse(inputLargeur, out largeur))
        Console.WriteLine("Veuillez entrer un nombre entier valide.");
    else if (largeur > 12)
        Console.WriteLine("Vous ne pouvez pas entrer une hauteur supérieure à 12");
    else if (largeur <= 0)
        Console.WriteLine("Vous ne pouvez pas entrer une largeur inférieure 1");
    else
        largeurValide = true;
}


Simulation simu = new Simulation(hauteur, largeur);
simu.Simuler(simu.Pot, simu);