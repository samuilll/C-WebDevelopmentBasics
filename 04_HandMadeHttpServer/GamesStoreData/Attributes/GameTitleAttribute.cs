using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GamesStoreData.Attributes
{
   public class GameTitleAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string title = (string) value;


            return char.IsUpper(title[0]);
        }
    }
}
