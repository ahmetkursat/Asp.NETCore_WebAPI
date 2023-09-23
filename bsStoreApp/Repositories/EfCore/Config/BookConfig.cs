using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EfCore.Config
{
        public class BookConfig : IEntityTypeConfiguration<Book>
        {
            public void Configure(EntityTypeBuilder<Book> builder)
            {
                builder.HasData(
                    new Book { Id = 1, Title = "Hacivat karagöz", Price = 75 },
                    new Book { Id = 2, Title = "mesnevi", Price = 175 },
                    new Book { Id = 3, Title = "devlet", Price = 375 }

                );
            }
        }
}
