using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InGame.Business.Concrete.Enum;

namespace InGame.Business.Concrete.DTO.Concrete
{
    public class ServiceResult
    {
        public ServiceResult(ServiceResultType serviceResultType) { ServiceResultType = serviceResultType; }

        public string Message { get; set; }
        public ServiceResultType ServiceResultType { get; set; }
        public int ExceptionCode { get; set; }
        public Exception Exception { get; set; }
        public object Data { get; set; }
    }
}
