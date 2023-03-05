using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookWriting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int index { get; set; }

        public int authorID { get; set; }
        [ForeignKey("authorID")]
        public Author author { get; set; }

        public int bookID { get; set; }
        [ForeignKey("bookID")]
        public Book book { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage ="Vui lòng nhập vai trò")]
        [Display(Name = "Vai Trò")]
        public string role { get; set; }
        
    }
}