using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Data.Entities
{
    public class Contract : IEntity
    {
        public int Id { get; set; }

        public string GuidId { get; set; }

        public User Tenant { get; set; }

        //BeginReading
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? Start { get; set; }


        [Display(Name = "Monthly Price")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double MonthlyPrice { get; set; }

        public byte Document_Pdf { get; set; }// byte[]

        public Property Property { get; set; }


        [Display(Name = "Contract issued")]
        public bool ContractIssued { get; set; }
    }
}
