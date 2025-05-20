
public abstract class ActionUrgente
{
    public object Sujet { get; set; }
    
    public ActionUrgente() { }

    public void ProposerAction(object Sujet, Potager Pot)
    {
        if (Sujet is Animaux)
        {
            Console.WriteLine("Vous pouvez : ");
            Console.WriteLine("1 - Faire du bruit (contre : Oiseaux) \n2 - Poser un épouvantail (Plus aucun oiseau) \n3 - Faire fuir le chat (contre : Chat) \n4 - Reboucher un trou (contre : Taupe) \n5- Adopter un chien (plus aucun rongeur)");
        }
        else if (Sujet is Intemperie)
        {
            Console.WriteLine("Vous pouvez : ");
            Console.WriteLine("6 - Mettre une bâche (contre : Grêle) \n7 - Installer une pompe (contre : Inondation) \n8 - Utiliser le tuyau d'arrosage (contre : Sécheresse) \n9 - installer l'arrosage automatique (contre : Sécheresse)");
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
            InstallerArosageAuto(Sujet, Pot);
        }
    }

    public void FaireBruit(object Sujet)
    {
        if (Sujet is Oiseau oiseau)
        {
            oiseau.Disparait();
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }

    public void PoserEpouvantail(object Sujet, ref bool PresenceEpouvantail)
    {
        if (Sujet is Oiseau oiseau)
        {
            oiseau.Disparait();
            PresenceEpouvantail = true;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void FaireFuirChat(object Sujet)
    {
        if (Sujet is Chat chat)
        {
            chat.Disparait();
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void ReboucherTrou(object Sujet)
    {
        if (Sujet is Taupe taupe)
        {
            taupe.Disparait();
            //Enlever les trous
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void AdopterChien(object Sujet, ref bool PresenceChien)
    {
        if (Sujet is Rongeur rongeur)
        {
            rongeur.Disparait();
            PresenceChien = true;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void PoserBache(object Sujet, ref bool Grele, ref bool Bache)
    {
        if ((Sujet is Grele)) /* Et Vérifier qu'on a une bache */
        {
            Grele = false;
            Bache = true; //A mettree dans Simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void InstallerPompe(object Sujet)
    {
        if ((Sujet is Inondation) /*&& (Vérifier qu'on a une pompe )*/)
        {
            bool inondation = false;
            bool pompe = true; //A mettree dans Simulation
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void Arroser(object Sujet, Potager pot)
    {
        if ((Sujet is Secheresse)/* && (Vérifier qu'on a un tuyau d'arrosage )*/)
        {
            pot.EffetArroserTuyau();
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void InstallerArosageAuto(object Sujet, Potager pot)
    {
        if ((Sujet is Secheresse) /*&& (Vérifier qu'on a un arrosage auto )*/)
        {
            bool Secheresse = false;
            pot.EffetArrosageAutomatique();
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
}

