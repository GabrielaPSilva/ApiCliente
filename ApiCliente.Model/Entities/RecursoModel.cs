using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Model.Entities
{
    public class RecursoModel<T>
    {
        public T Data { get; set; }
        public List<Link> Links { get; set; }

        public RecursoModel(T data, List<Link> links)
        {
            Data = data;
            Links = links;
        }
    }
}
