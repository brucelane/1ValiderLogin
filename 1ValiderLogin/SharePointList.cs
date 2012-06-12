using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.WebPartPages.Communication;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;

namespace _1ValiderLogin
{
    class SharePointList
    {
        private string listTitle; // titre
        private string listDescription; // description
        private SPListTemplateType listTemplateType; // type de template de la liste
        private List<champSPlist> CollChamps = new List<champSPlist>(); // liste des objet champs

        private int nbInfoCreer; // nombre de ligne creer dans la liste

        // Constructeur de la l'objet SharePointList
        public SharePointList(string Title, string Description, SPListTemplateType TemplateType)
        {
            listTitle = Title;
            listDescription = Description;
            listTemplateType = TemplateType;
            nbInfoCreer = 0;
        }

        // Methode de remplissage de la liste d'objet champ de la liste
        public void addCollChamps(champSPlist champ)
        {
            CollChamps.Add(champ);
        }

        // Methode qui crée la SharepointList dans SharePoint
        public void creerListInSharePoint(SPListCollection listCollection)
        {
            SPWeb webSP = SPContext.Current.Web; // recuperation context
            webSP.AllowUnsafeUpdates = true; // autorisation de modification des liste SharePoint
            SPList list;
            listCollection.Add(listTitle, listDescription, listTemplateType); // methode de creation de liste dans sharepoint
            foreach (champSPlist Champ in CollChamps) // pour chaque objet champs dans la liste
            {
                list = listCollection[listTitle]; // recuperation de la liste a modifier
                list.Fields.AddFieldAsXml(@"<Field Type='Text' DisplayName='" + Champ.getTitle() + "'/>", true, SPAddFieldOptions.Default); // creation du champ
                foreach (string info in Champ.getCollInfo()) // pour chaque information dans la liste d'information de l'objet champ actif
                {
                    SPListItem LigneInfo;
                    LigneInfo = listCollection[listTitle].AddItem(); // on rajoute une ligne d'info
                    LigneInfo["Titre"] = nbInfoCreer;
                    LigneInfo[Champ.getTitle()] = info;
                    LigneInfo.Update();
                    nbInfoCreer++; //indique l'index a donner a l'info et donne au finale le nombre d'info creer
                }
            }
        }
    }


    class champSPlist
    {
        private string title; // titre  du champ
        private List<string> CollInfo = new List<string>(); // liste d'information a creer dans se champ

        public champSPlist(string titleChamp) // constructeur
        {
            title = titleChamp;
        }

        public string getTitle() // methode de recuperation du titre
        {
            return title;
        }

        public List<string> getCollInfo() // methode de recupeation de la liste d'info
        {
            return CollInfo;
        }

        public void addCollInfo(string info) // methode d'ajour d'une info dans al liste
        {
            CollInfo.Add(info);
        }
    }
}
