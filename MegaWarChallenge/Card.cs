using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaWarChallenge
{
    public class Card
    {
        public string file { get; set; }
        public string relPath
        {
            get
            {
                return file.Substring(file.LastIndexOf("\\") - 5);
            }
            set
            {

            }
        }
        public string suit { get; set; }
        public int value { get; set; }
        public string name { get; set; }
    }
}