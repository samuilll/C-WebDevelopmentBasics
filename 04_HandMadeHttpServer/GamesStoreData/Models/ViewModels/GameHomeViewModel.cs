﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GamesStoreData.Models.ViewModels
{
   public class GameHomeViewModel
    {
        public string ThumbnailUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price  { get; set; }

        public decimal Size { get; set; }

    }
}
