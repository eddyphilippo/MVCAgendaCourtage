﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVCLaManuAgendaCourtage.Models
{
    [Table("customers")]
    public partial class Customers
    {
        public Customers()
        {
            Appointments = new HashSet<Appointments>();
        }

        [Key]
        [Column("idCustomer")]
        public int IdCustomer { get; set; }
        [Required]
        [Column("lastname")]
        [StringLength(50)]
        [Unicode(false)]
        public string Lastname { get; set; }
        [Required]
        [Column("firstname")]
        [StringLength(50)]
        [Unicode(false)]
        public string Firstname { get; set; }
        [Required]
        [Column("mail")]
        [StringLength(100)]
        [Unicode(false)]
        public string Mail { get; set; }
        [Required]
        [Column("phoneNumber")]
        [StringLength(10)]
        [Unicode(false)]
        public string PhoneNumber { get; set; }
        [Column("budget")]
        public int Budget { get; set; }

        [InverseProperty("IdCustomerNavigation")]
        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}