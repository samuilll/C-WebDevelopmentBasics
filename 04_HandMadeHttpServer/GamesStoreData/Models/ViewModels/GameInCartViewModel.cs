﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GamesStoreData.Models.ViewModels
{
   public class GameInCartViewModel
    {
        public int Id { get; set; }

        public string Thumbnail { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
