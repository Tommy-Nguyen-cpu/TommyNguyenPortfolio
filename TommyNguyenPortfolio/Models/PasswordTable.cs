using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TommyNguyenPortfolio.Models
{
    public class PasswordTable
    {
        [Key]
        public int PasswordTableId { get; set; }


        [Required, PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public int ClientID { get; set; }

        public int PermissionLevel { get; set; }

    }
}
