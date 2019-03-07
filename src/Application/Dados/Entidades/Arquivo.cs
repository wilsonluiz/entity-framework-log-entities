using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dados.Entidades
{
    [Table("FILES", Schema = "HR")]
    public class Arquivo
    {
        [Column("FILE_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Column("FILE_DATA")]
        public DateTime DataCriacao { get; set; }

        [Column("FILE_DADOS")]
        public string Dados { get; set; }
    }
}
