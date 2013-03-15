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
    public class JPerson : JAncestor
    {
        public string id;
        public string displayName;
        public string title;
        public string avatarSmall;
    }
}
