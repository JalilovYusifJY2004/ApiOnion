using ApiOnion104.Application.Abstractions.Repositories;
using ApiOnion104.Domain.Entities;
using ApiOnion104.Persistence.Contexts;
using ApiOnion104.Persistence.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnion104.Persistence.Implementations.Repositories
{
    public class ColorRepository:Repository<Color>,IColorRepositoy
    {
        public ColorRepository(AppDbContext context):base(context) { }  
   
    }
}
