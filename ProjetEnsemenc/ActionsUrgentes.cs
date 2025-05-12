using System.Globalization;
using System.Reflection.PortableExecutable;

public abstract class ActionUrgente
{
    public string Sujet { get; set; }
    public ActionUrgente(string sujet)
    {
        Sujet = sujet;
    }
    public void ProposerAction(string Sujet, Potager Pot)
    {
        if (Sujet == "animal")
        {
            Console.WriteLine("Vous pouvez : ");
            Console.WriteLine("-Faire du bruit (1) \n- Poser un épouvantail (2) \n- Faire fuir le chat (3) \n- Reboucher un trou (4) \n- Adopter un chien (5)");
        }
        else if (Sujet == "intempérie")
        {
            Console.WriteLine("Vous pouvez : ");
            Console.WriteLine("-Mettre une bâche (6) \n- Installer une pompe (7) \n- Utiliser le tuyau d'arrosage (8) \n- installer l'arrosage automatique (9)");
        }

        int choix = Convert.ToInt32(Console.ReadLine());
        if (choix == 1)
        {
            FaireBruit(Sujet);
        }
        else if (choix == 2)
        {
            PoserEpouvantail(Sujet);
        }
        else if (choix == 3)
        {
            FaireFuirChat(Sujet);
        }
        else if (choix == 4)
        {
            ReboucherTrou(Sujet);
        }
        else if (choix == 5)
        {
            AdopterChien(Sujet);
        }
        else if (choix == 6)
        {
            PoserBache(Sujet);
        }
        else if (choix == 7)
        {
            InstallerPompe(Sujet);
        }
        else if (choix == 8)
        {
            Arroser(Sujet, Pot);
        }
        else if (choix == 9)
        {
            InstallerArosageAutomatique(Sujet, Pot);
        }
    }

    public void FaireBruit(string Sujet)
    {
        if (Sujet is Oiseau)
        {
            Sujet.Disparait();
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }

    public void PoserEpouvantail(string Sujet)
    {
        if (Sujet is Oiseau)
        {
            Sujet.Disparait;
            bool epouvantail = true; //Mettre dans simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void FaireFuirChat(string Sujet)
    {
        if (Sujet is Chat)
        {
            Sujet.Disparait();
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void ReboucherTrou(string Sujet)
    {
        if (Sujet is Taupe)
        {
            Sujet.Disparait();
            //Enlever les trous
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void AdopterChien(string Sujet)
    {
        if (Sujet is Rongeur)
        {
            Sujet.Disparait();
            bool chien = true; //A mettree dans Simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void PoserBache(string Sujet)
    {
        if ((Sujet is Grele) && (/*Vérifier qu'on a une bache */))
        {
            Sujet.Disparait();
            bool bache = true; //A mettree dans Simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void InstallerPompe(string Sujet)
    {
        if ((Sujet is Inondation) && (/*Vérifier qu'on a une pompe */))
        {
            Sujet.Disparait();
            bool pompe = true; //A mettree dans Simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void Arroser(string Sujet, Potager Pot)
    {
        if ((Sujet is Secheresse) && (/*Vérifier qu'on a un tuyau d'arrosage */))
        {
            Sujet.Disparait();
            foreach (Plante plante in Pot)
            {
                plante.NiveauHumidite += 5;
            }
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void InstallerArosageAutomatique(string Sujet, Potager Pot)
    {
        if ((Sujet is Secheresse) && (/*Vérifier qu'on a un arrosage auto */))
        {
            Sujet.Disparait();
            foreach (Plante plante in Pot)
            {
                plante.NiveauHumidite = plante.SeuilHumidite;
            }
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
}

