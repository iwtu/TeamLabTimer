/*
 * Copyright © 2012 Adrián Lachata
 * 
 * CIIT! means Code It If Necessary. I use it when a piece of code have good reason,
 * but it doesn't seem necassary to me.
 * 
 */

using System;

namespace TeamLab.API.Entities
{
    public class JFoundedProjets : JAncestor
    {
        public class Item
        {
            public int id;
            public int entityType;
            public string title;
            public string created;
        }

        public Item[] items;
        public JProjectOwner projectOwner;
    }
}
