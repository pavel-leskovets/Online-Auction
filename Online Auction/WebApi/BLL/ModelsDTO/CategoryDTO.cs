using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ModelsDTO
{
    /// <summary>
    /// Category data transfer object which contains information about the category.
    /// </summary>
    public class CategoryDTO
    {
        /// <summary>
        /// Id of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
