using System;
using System.Collections.Generic;
using System.Text;

namespace GamesStoreData.Models.ViewModels
{
   public class GameViewModel
    {
        public int SequelNumber { get; set; }

        public string Title { get; set; }

        public decimal Size { get; set; }

        public decimal Price { get; set; }
    }
}
