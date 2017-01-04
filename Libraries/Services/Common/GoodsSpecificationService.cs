using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class GoodsSpecificationService
    {
        private readonly DapperRepository _repository;
        private readonly IDbConnection _context;

        public GoodsSpecificationService(DapperRepository repository,
            IDbConnection context)
        {
            _repository = repository;
            _context = context;
        }
    }
}
