using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShopAutomatizator.Models
{
    public class PdfModel
    {
        [Key]
        public int PdfKey { get; set; }

        [DisplayName("Pdf name")]
        [Column(TypeName = "nvarchar(50)")]
        public string PdfName { get; set; }

        [NotMapped]
        [DisplayName("Upload file")]
        public IFormFile PdfFile { get; set; }

		internal void Update(PdfModel pdfModel)
		{
			PdfName = pdfModel.PdfName;
            if(pdfModel.PdfFile != null)
                PdfFile = pdfModel.PdfFile;
        }
	}
}
