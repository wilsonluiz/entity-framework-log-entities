using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dados.Entidades
{
    [Description("")]
    [Table("LOCATIONS", Schema = "HR")]
    public class Location
    {
        [Key]
        [Column("LOCATION_ID")]
        [Description("")]
        public decimal LocationId { get; set; }

        [Column("STREET_ADDRESS")]
        [Description("")]
        public string StreetAddress { get; set; }

        [Column("POSTAL_CODE")]
        [Description("")]
        public string PostalCode { get; set; }

        [Column("CITY")] [Description("")] public string City { get; set; }

        [Column("STATE_PROVINCE")]
        [Description("")]
        public string StateProvince { get; set; }

        [Column("COUNTRY_ID")]
        [Description("")]
        public string CountryId { get; set; }
    }
}