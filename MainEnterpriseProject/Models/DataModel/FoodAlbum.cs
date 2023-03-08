﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class FoodAlbum
    {
        public int ID { get; set; }
        public string albumName { get; set; }
        public string albumImage { get; set; }
        public List<int> lstFoodIDs { get; set; }
    }
}