using UnityEngine;
using System.Collections;

public class Item  {

       
        public int ID { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public int Energy { get; set; }
        public int Protein { get; set; }
        public int Thirst { get; set; }
        public string Description { get; set; }
        public bool Stackable { get; set; }
        public int Rarity { get; set; }
        public string Slug { get; set; }
        public Sprite Sprite { get; set; }
        public GameObject itemModel { get; set; }

        public Item(int id, string title, int type, int energy, int protein, int thirst, string description, bool stackable, int rarity, string slug)
        {
            this.ID = id;
            this.Title = title;
            this.Type = type;
            this.Energy = energy;
            this.Protein = protein;
            this.Thirst = thirst;
            this.Description = description;
            this.Stackable = stackable;
            this.Rarity = rarity;
            this.Slug = slug;
            this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
            this.itemModel = Resources.Load<GameObject>("Models/" + slug);
        }

        public Item()
        {
            this.ID = -1;
        }

    }
