using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories;

namespace Application.Groups
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; }

    }
}