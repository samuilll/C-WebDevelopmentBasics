using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.ByTheCakeApp.ViewModels
{
   public class ProfileViewModel
    {
        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int OrdersCount { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"<p>Name: {this.Name}</p>");
            sb.AppendLine($"<p>Registered on: {this.RegistrationDate.ToString("dd-MM-yyyy")}</p>");
            sb.AppendLine($"<p>Orders count:: {this.OrdersCount}</p>");

            return sb.ToString().TrimEnd('\n', '\r');
        }
    }
}
