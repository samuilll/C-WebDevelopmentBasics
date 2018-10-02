using System.ComponentModel.DataAnnotations;

namespace GamesStoreData.Models
{
   public class OrderGame
    {
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
